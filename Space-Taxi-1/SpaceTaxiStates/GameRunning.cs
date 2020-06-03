using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.State;
using DIKUArcade.Timers;
using SpaceTaxi_1.Entities;
using SpaceTaxi_1.LevelLoading;
using SpaceTaxi_1.Utilities;


namespace SpaceTaxi_1.SpaceTaxiStates {
    public class GameRunning : IGameState {
        private static GameRunning instance;

        private static Stopwatch gameTimer;
        private List<Customer> CustomerList;
        private List<Customer> CustomersPickedUp;
        private Entity backGroundImage;
        private LevelCreator levelCreator;
        private Level level;
        private Player player;
        private PointDisplay points;
        private static string map;
        
        private int CusBye;

        public GameRunning() {
            InitializeGameState();
        }

        public static GameRunning GetInstance() {
            return GameRunning.instance ?? (GameRunning.instance = new GameRunning());
        }

        public static GameRunning NewInstance(string map) {
            GameRunning.map = map;
            return GameRunning.instance = new GameRunning();
        }
        
        public void InitializeGameState() {
            GameRunning.gameTimer = new Stopwatch();
            GameRunning.gameTimer.Start();
            
            
            backGroundImage = new Entity(new StationaryShape(new Vec2F(0.0f, 0.0f),
         new Vec2F(1.0f, 1.0f)), 
                new Image(Path.Combine("Assets", "Images", "SpaceBackground.png"))
            );
            levelCreator = new LevelCreator();
            level = levelCreator.CreateLevel(GameRunning.map);
            player = new Player();            
            player.SpawnPos(levelCreator.playerpos);
            CustomerList = new List<Customer>();
            CustomersPickedUp = new List<Customer>();
            foreach (var customer in levelCreator.Customer) {                
                Customer customer1 = new Customer(customer);                                
                CustomerList.Add(customer1);                
            }
            points = new PointDisplay();
            
        }

        

        public void UpdateGameLogic() {            
            player.Move();
            GameCollisions();
            foreach (var cust in CustomerList) {
                SpawnCustomer(cust);
            }
            CustomerTimeUp(CustomerList);            
            StopRenderDropOff();
        }

        public void RenderState() {
            backGroundImage.RenderEntity();
            level.RenderLevelObjects();
            player.RenderPlayer();
            foreach (var customer in CustomerList) {
                customer.RenderCustomer(customer.IsRendered);
            }
            foreach (var customer in CustomersPickedUp) {
                customer.RenderCustomer(customer.IsRendered);
            }
            points.RenderPoints();
        }
        
        /// <summary>
        /// checks for all Collisions that can occur
        /// and handles the appropriately 
        /// </summary>
        public void GameCollisions() {
            Collision.ObstacleCollision(player, levelCreator.Obstacles);              

            if (Collision.ExitCollision(player, levelCreator.Exits)) {
                if (GameRunning.map == "short-n-sweet.txt") {
                    
                    GameRunning.NewInstance("the-beach.txt");
                    InitializeGameState();
                } else {                    
                    GameRunning.NewInstance("short-n-sweet.txt");
                    InitializeGameState();
                }
            }

            var platcol = Collision.PlatformCollision(player, levelCreator.Platforms);
            if (platcol.Item1) {
                var v = platcol.Item2;
                player.PlatformTouch = true;
                DropCustomerOff(CustomersPickedUp, v);
                
            }


            foreach (Customer customer in CustomerList) {
                if (Collision.CustomerCollision(player, customer)) {
                    customer.IsRendered = false;
                                       
                    CustomersPickedUp.Add(customer);                                        
                }
            }            
        }
        /// <summary>
        /// Ends game if player is to slow to drop off customer
        /// </summary>
        /// <param name="cuslst"></param>
        
        public void CustomerTimeUp(List<Customer> cuslst) {
            var timeSpan = GameRunning.gameTimer.Elapsed.Seconds;
            foreach (Customer customer in cuslst) {                
                if (timeSpan >= customer.dropOffTime + customer.SpawnTime && !customer.IsDroppedOff) {                       
                    EventBus.GetBus().RegisterEvent(GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.GameStateEvent, "", "CHANGE_STATE",
                        "MAIN_MENU", ""));
                }                
            }
        }
        /// <summary>
        /// handles customer drop off, set customer pos to drop off platform and set IsRender 
        /// = true, and sets StopRenderTime = 2 secs, adds points for the display
        /// </summary>
        /// <param name="cusLst"></param>
        /// <param name="c"></param>

        public void DropCustomerOff(List<Customer> cusLst, char c) {      
            var timeSpan = GameRunning.gameTimer.Elapsed.Seconds;
            if (cusLst.Count>0) {                               
                foreach (Customer customer in cusLst) {                     
                    foreach (var platform in levelCreator.Platforms) {                        
                        if (platform.PlatformType == customer.DropOffPlatform 
                            && c == customer.DropOffPlatform) {                            
                            float newY = platform.Shape.Position.Y + platform.Shape.Extent.Y;
                            Vec2F pos = new Vec2F(platform.Shape.Position.X, newY);
                            customer.SpawnPosition(pos);
                            customer.IsRendered = true;
                            customer.IsDroppedOff = true;
                            points.SuccessfulDrop(customer.dropOffPoints);                            
                            customer.StopRenderTime = timeSpan + 2;
                            break;
                            
                        }
                    }                    
                }                
            }
        }
        
        /// <summary>
        /// Stops rendering of dropped off customer, after they have been displayed
        /// again for a second
        /// </summary>
        public void StopRenderDropOff() {
            var timeSpan = GameRunning.gameTimer.Elapsed.Seconds;
            if (CustomersPickedUp.Count > 0) {
                foreach (var customer in CustomersPickedUp) {
                    if (timeSpan >= customer.StopRenderTime && 1 < customer.StopRenderTime) {
                        customer.IsRendered = false;
                    }
                }                 
            }                                       
        }
        /// <summary>
        /// Spawns customer at the given spawn platform, when the stopwatch gameTimer has
        /// reached the number of seconds passed, the given customers spawn time is, if it has not
        /// been picked up all ready 
        /// </summary>
        /// <param name="customer"></param>
        public void SpawnCustomer(Customer customer) {
            var timeSpan = GameRunning.gameTimer.Elapsed.Seconds;
            
            foreach (var platform in levelCreator.Platforms) {
                if (!(customer.IsDroppedOff) && platform.PlatformType == customer.StartPlatform) {                        
                    float newY = platform.Shape.Position.Y + platform.Shape.Extent.Y;
                    Vec2F pos = new Vec2F(platform.Shape.Position.X, newY);                                                
                    customer.SpawnPosition(pos);
                    break;
                }                    
            }
                   
            if (timeSpan == customer.SpawnTime && !customer.IsDroppedOff) {                                    
                customer.IsRendered = true;
            }                                                
        }
        
        

        public void HandleKeyEvent(string keyValue, string keyAction) {
            
        }
        
        public void GameLoop() {
            
        }
    }
}
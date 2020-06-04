
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using SpaceTaxi_1.Enums;
using SpaceTaxi_1.Utilities;

namespace SpaceTaxi_1.Entities {
    public class Player : IGameEventProcessor<object> {
        public DynamicShape Shape;

        public bool MoveLeft;
        public bool MoveRight;
        public bool MoveUp;
        public bool PlatformTouch;
        
        private IBaseImage thrusterOffImageLeft;
        private IBaseImage thrusterOffImageRight;
        
        private IBaseImage thrusterOnImageBackLeft; 
        private IBaseImage thrusterOnImageBackRight; 
        private IBaseImage thrusterOnImageBottomBackLeft; 
        private IBaseImage thrusterOnImageBottomBackRight; 
        private IBaseImage thrusterOnImageBottomLeft; 
        private IBaseImage thrusterOnImageBottomRight; 

        private float upThruster = 0.00025F;
        private float sideThruster = 0.00015F;
        private Orientation orientation;

        public Player() {
            Shape = new DynamicShape(new Vec2F(), new Vec2F(0.06F, 0.06F));
                        
            
            thrusterOffImageLeft = new Image(Utils.GetImageFilePath("Taxi_Thrust_None.png"));
            thrusterOffImageRight = new Image(Utils.GetImageFilePath("Taxi_Thrust_None_Right.png"));
           
            thrusterOnImageBackLeft = new ImageStride(1000,
                ImageStride.CreateStrides(2,
                    Utils.GetImageFilePath("Taxi_Thrust_Back.png")));
            thrusterOnImageBackRight = new ImageStride(1000,
                ImageStride.CreateStrides(2,
                    Utils.GetImageFilePath("Taxi_Thrust_Back_Right.png")));

            thrusterOnImageBottomLeft = new ImageStride(1000,
                ImageStride.CreateStrides(2,
                    Utils.GetImageFilePath("Taxi_Thrust_Bottom.png")));
            thrusterOnImageBottomRight = new ImageStride(1000,
                ImageStride.CreateStrides(2,
                    Utils.GetImageFilePath("Taxi_Thrust_Bottom_Right.png")));

            thrusterOnImageBottomBackLeft = new ImageStride(1000,
                ImageStride.CreateStrides(2,
                    Utils.GetImageFilePath("Taxi_Thrust_Bottom_Back.png")));
            thrusterOnImageBottomBackRight = new ImageStride(1000,
                ImageStride.CreateStrides(2,
                    Utils.GetImageFilePath("Taxi_Thrust_Bottom_Back_Right.png")));
            
            Entity = new Entity(Shape, thrusterOffImageLeft);            
            EventBus.GetBus().Subscribe(GameEventType.PlayerEvent,this);
            
            
                        
        }
        public Entity Entity { get; set; }


        
        public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent) {
            if (eventType == GameEventType.PlayerEvent) {
                switch (gameEvent.Message) {
                case "BOOSTER_UPWARDS":
                    MoveUp = true;
                    break;
                case "BOOSTER_TO_LEFT":
                    MoveLeft = true;
                    orientation = Orientation.Left;
                    break;
                case "BOOSTER_TO_RIGHT":
                    MoveRight = true;
                    orientation = Orientation.Right;
                    break;
                case "STOP_ACCELERATE_UP":
                    MoveUp = false;
                    break;
                case "STOP_ACCELERATE_LEFT":
                    MoveLeft = false;
                    break;
                case "STOP_ACCELERATE_RIGHT":
                    MoveRight = false;
                    break;
                }
            }            
        }
        /// <summary>
        /// Handles all player movement, gravity on/off, side and up thrusters.
        /// </summary>
        public void Move() {            
            var en = Entity.Shape.AsDynamicShape();            
            en.Direction.Y -= 0.0000981F;
            
            if (MoveUp && MoveRight) {
                en.Direction.Y += upThruster;
                en.Direction.X += sideThruster;
            } else if (MoveUp && MoveLeft) {
                en.Direction.Y += upThruster;
                en.Direction.X -= sideThruster;
            } else if(MoveUp) {
                en.Direction.Y += upThruster;
            } else if(MoveLeft) {                
                en.Direction.X -= sideThruster;
            } else if (MoveRight) {
                en.Direction.X += sideThruster;
            }

            if (!MoveUp && PlatformTouch) {                
                en.Direction.Y = 0.0F;
                en.Direction.X = 0.0F;
            } else {
                PlatformTouch = false;
                Entity.Shape.Move();
            }
                                                                               
        }
        
        

        public void SpawnPos(Vec2F pos) {
            Shape.Position = pos;
        }
        /// <summary>
        /// Matches the players active thrusters and orientation with corresponding images
        /// </summary>
        public void PlayerImage() {
            if (MoveUp && MoveRight) {
                Entity = new Entity(Shape, thrusterOnImageBottomBackRight);
            } else if (MoveUp && MoveLeft) {
                Entity = new Entity(Shape, thrusterOnImageBottomBackLeft);
            } else if(MoveUp) {
                if (orientation == Orientation.Right) {
                    Entity = new Entity(Shape, thrusterOnImageBottomRight);    
                } else {
                    Entity = new Entity(Shape, thrusterOnImageBottomLeft);                    
                }                
            } else if(MoveLeft) {                
                Entity = new Entity(Shape, thrusterOnImageBackLeft);
            } else if (MoveRight) {
                Entity = new Entity(Shape, thrusterOnImageBackRight);
            } else {
                if (orientation == Orientation.Right) {
                    Entity = new Entity(Shape, thrusterOffImageRight);
                } else {
                    Entity = new Entity(Shape, thrusterOffImageLeft);
                }
            }
        }        
        

        public void RenderPlayer() {
            PlayerImage();
            Entity.RenderEntity();
        }
        
    }
}
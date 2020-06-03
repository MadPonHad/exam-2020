using System.Drawing;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.State;
using SpaceTaxi_1.Utilities;
using Image = DIKUArcade.Graphics.Image;

namespace SpaceTaxi_1.SpaceTaxiStates {
    public class MainMenu : IGameState {
        private static MainMenu instance;

        private Entity backGroundImage;
        private Text[] menuButtons;
        private int activeMenuButton;
        private const int maxMenuButtons = 3;

        public MainMenu() {
            InitializeGameState();
        }

        public void RenderState() {
            backGroundImage.RenderEntity();

            foreach (var button in menuButtons) {
                button.RenderText();
                
            }
        }

        public static MainMenu GetInstance() {
            return MainMenu.instance ?? (MainMenu.instance = new MainMenu());
        }

        

        public void InitializeGameState() {
            backGroundImage =
                new Entity(new StationaryShape(new Vec2F(0.0f, 0.0f), new Vec2F(1.0f, 1.0f))
                    , new Image(Path.Combine("Assets", "Images", "SpaceBackground.png")));

            menuButtons = new Text[MainMenu.maxMenuButtons];
            menuButtons = new[] {
                new Text("New Game", new Vec2F(0.4f, 0.3f), new Vec2F(0.3f, 0.35f)),
                new Text("Choose Level", new Vec2F(0.4f, 0.2f), new Vec2F(0.3f, 0.35f)),
                new Text("Quit", new Vec2F(0.4f, 0.1f), new Vec2F(0.3f, 0.35f))
                
            };            
            menuButtons[0].SetColor(Color.DarkGray);            
            menuButtons[1].SetColor(Color.DarkGray);
            menuButtons[2].SetColor(Color.DarkGray);
            menuButtons[activeMenuButton].SetColor(Color.White);
        }
        
        public void HandleKeyEvent(string keyValue, string keyAction) {
            switch (keyAction) {
            case "KEY_PRESS":
                switch (keyValue) {
                case "KEY_UP":                    
                    if (activeMenuButton > 0) {
                        menuButtons[activeMenuButton].SetColor(Color.DarkGray);
                        activeMenuButton--;
                        menuButtons[activeMenuButton].SetColor(Color.White);                        
                    } else if (activeMenuButton == 0) {
                        menuButtons[activeMenuButton].SetColor(Color.DarkGray);
                        activeMenuButton = 2;
                        menuButtons[activeMenuButton].SetColor(Color.White);
                    }

                    break;

                case "KEY_DOWN":                    
                    if (activeMenuButton < MainMenu.maxMenuButtons - 1) {
                        menuButtons[activeMenuButton].SetColor(Color.DarkGray);
                        activeMenuButton++;
                        menuButtons[activeMenuButton].SetColor(Color.White);                        
                    } else if (activeMenuButton == MainMenu.maxMenuButtons - 1) {
                        menuButtons[activeMenuButton].SetColor(Color.DarkGray);
                        activeMenuButton = 0;
                        menuButtons[activeMenuButton].SetColor(Color.White);
                    }

                    break;

                case "KEY_ENTER":
                    
                    if (activeMenuButton == 0) {
                        GameRunning.NewInstance("short-n-sweet.txt");
                        EventBus.GetBus().RegisterEvent(
                            GameEventFactory<object>.CreateGameEventForAllProcessors(
                                GameEventType.GameStateEvent, this, "CHANGE_STATE",
                                "GAME_RUNNING", ""));

                        
                    } else if (activeMenuButton == 1) {
                        EventBus.GetBus().RegisterEvent(
                            GameEventFactory<object>.CreateGameEventForAllProcessors(
                                GameEventType.GameStateEvent, this, "CHANGE_STATE",
                                "CHOOSE_LEVEL", ""));
                        
                    } else if (activeMenuButton == 2) {
                        EventBus.GetBus().RegisterEvent(
                            GameEventFactory<object>.CreateGameEventForAllProcessors(
                                GameEventType.WindowEvent, this, "CLOSE_WINDOW",
                                "", ""));
                    }

                    break;
                }

                break;
            }
        }

        public void UpdateGameLogic() {
            
        }                
        
        public void GameLoop() {
            
        }
    }
    
    
}
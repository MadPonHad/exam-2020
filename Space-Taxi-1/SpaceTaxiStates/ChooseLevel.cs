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
    public class ChooseLevel : IGameState {
        private static ChooseLevel instance;
        private int activeMenuButton;
        private int maxMenuButtons = 3;

        private Entity backGroundImage;
        private Text[] menuButtons;
        
        public ChooseLevel() {            
            InitializeGameState();
        }

        public static ChooseLevel GetInstance() {
            return ChooseLevel.instance ?? (ChooseLevel.instance = new ChooseLevel());
        }
        

        public void InitializeGameState() {
            backGroundImage =
                new Entity(new StationaryShape(new Vec2F(0.0f, 0.0f),
                        new Vec2F(1.0f, 1.0f)),
                    new Image(Path.Combine("Assets", "Images", "spaceBackGround.png")));

            menuButtons = new Text[maxMenuButtons];
            menuButtons = new[] {
                new Text("1: Short -N- Sweet", new Vec2F(0.30f, 0.4f),
                    new Vec2F(0.3f, 0.35f)),
                new Text("2: The Beach", new Vec2F(0.30f, 0.3f),
                    new Vec2F(0.3f, 0.35f)),
                new Text("Return", new Vec2F(0.30f, 0.2f),
                    new Vec2F(0.3f, 0.35f))
            };

            
            menuButtons[0].SetColor(Color.DarkGray);            
            menuButtons[1].SetColor(Color.DarkGray);            
            menuButtons[2].SetColor(Color.DarkGray);
            menuButtons[activeMenuButton].SetColor(Color.White);
        }



        public void RenderState() {
            backGroundImage.RenderEntity();

            foreach (var button in menuButtons) {
                button.RenderText();
                
            }
            
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
                    if (activeMenuButton < maxMenuButtons - 1) {
                        menuButtons[activeMenuButton].SetColor(Color.DarkGray);
                        activeMenuButton++;
                        menuButtons[activeMenuButton].SetColor(Color.White);                        
                    } else if (activeMenuButton == maxMenuButtons - 1) {
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
                        GameRunning.NewInstance("the-beach.txt");
                        EventBus.GetBus().RegisterEvent(
                            GameEventFactory<object>.CreateGameEventForAllProcessors(
                                GameEventType.GameStateEvent, this, "CHANGE_STATE",
                                "GAME_RUNNING", ""));                        
                        
                    } else if (activeMenuButton == 2) {
                        EventBus.GetBus().RegisterEvent(
                            GameEventFactory<object>.CreateGameEventForAllProcessors(
                                GameEventType.GameStateEvent, this, "CHANGE_STATE",
                                "MAIN_MENU", ""));
                    }
                    break;
                }
                break;
            }
        }
        
        public void GameLoop() {            
        }
        
        public void UpdateGameLogic() {            
        }
    }
}
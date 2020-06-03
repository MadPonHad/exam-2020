using System.Drawing;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.State;
using SpaceTaxi_1.Utilities;

namespace SpaceTaxi_1.SpaceTaxiStates {
    public class GamePaused : IGameState{
        private static GamePaused instance;
        private int maxMenuButtons = 2;
        private int activeMenuButton;

        private Text[] menuButtons;

        public GamePaused() {
            InitializeGameState();
        }
         
        public static GamePaused GetInstance() {
            return GamePaused.instance ?? (GamePaused.instance = new GamePaused());
        }

        public void InitializeGameState() {
            menuButtons = new Text[maxMenuButtons];
            menuButtons = new[] {
                new Text("Continue", new Vec2F(0.40f, 0.3f),
                    new Vec2F(0.3f, 0.35f)),
                new Text("Main Menu", new Vec2F(0.40f, 0.22f),
                    new Vec2F(0.3f, 0.35f))
            };

            menuButtons[0].SetFontSize(50);
            menuButtons[0].SetColor(Color.DarkGray);
            menuButtons[1].SetFontSize(50);
            menuButtons[1].SetColor(Color.DarkGray);
            menuButtons[activeMenuButton].SetColor(Color.White);

            menuButtons[activeMenuButton].SetColor(new Vec3F(0.0f, 1.0f, 0.0f));        
        }
        

        public void RenderState() {
            GameRunning.GetInstance().RenderState();

            foreach (var button in menuButtons) {
                button.RenderText();
            }
        }

        public void HandleKeyEvent(string keyValue, string keyAction) {
            if (keyAction == "KEY_PRESS") {
                switch (keyValue) {
                case "KEY_UP":
                    if (activeMenuButton > 0) {
                        menuButtons[activeMenuButton].SetColor(Color.DarkGray);
                        activeMenuButton--;
                        menuButtons[activeMenuButton].SetColor(Color.White);
                    }

                    break;
                case "KEY_DOWN":
                    if (activeMenuButton < maxMenuButtons - 1) {
                        menuButtons[activeMenuButton].SetColor(Color.DarkGray);
                        activeMenuButton++;
                        menuButtons[activeMenuButton].SetColor(Color.White);
                    }

                    break;
                case "KEY_ENTER":
                    if (activeMenuButton == 1) {
                        EventBus.GetBus().RegisterEvent(
                            GameEventFactory<object>.CreateGameEventForAllProcessors(
                                GameEventType.GameStateEvent, this, "CHANGE_STATE",
                                "MAIN_MENU", ""));
                    } else if (activeMenuButton == 0) {
                        EventBus.GetBus().RegisterEvent(
                            GameEventFactory<object>.CreateGameEventForAllProcessors(
                                GameEventType.GameStateEvent, this, "CHANGE_STATE",
                                "GAME_RUNNING", ""));
                    }

                    break;
                }
            }
        }
        
        public void UpdateGameLogic() {
            
        }
        
        public void GameLoop() {
            
        }
    }
}
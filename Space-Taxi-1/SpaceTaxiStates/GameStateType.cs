using System;

namespace SpaceTaxi_1.SpaceTaxiStates {
    public enum GameStateType {
        GameRunning,
        GamePaused,
        MainMenu,
        ChooseLevel            
    }
    
    public class StateTransformer {

        public static GameStateType TransformStringToState(string state) {
            switch (state) {
            case "GAME_RUNNING":
                return GameStateType.GameRunning;
            case "GAME_PAUSED":
                return GameStateType.GamePaused;
            case "MAIN_MENU":
                return GameStateType.MainMenu;
            case "CHOOSE_LEVEL":
                return GameStateType.ChooseLevel;
            default:
                throw new ArgumentException("TransformStringToState Error");
            }
            
        }

        public static string TransformStateToString(GameStateType state) {
            switch (state) {
            case GameStateType.GameRunning:
                return "GAME_RUNNING";
            case GameStateType.GamePaused:
                return "GAME_PAUSED";
            case GameStateType.MainMenu:
                return "MAIN_MENU";
            case GameStateType.ChooseLevel:
                return "CHOOSE_LEVEL";
            default:
                throw new ArgumentException("TransformStateToString Error");
            }
        }
    }
}
using DIKUArcade.EventBus;
using DIKUArcade.State;
using SpaceTaxi_1.Utilities;


namespace SpaceTaxi_1.SpaceTaxiStates {
    public class StateMachine : IGameEventProcessor<object> {
        
        public IGameState ActiveState { get; private set; }

        public StateMachine() {
            EventBus.GetBus().Subscribe(GameEventType.GameStateEvent,this);
            EventBus.GetBus().Subscribe(GameEventType.InputEvent,this);

            ActiveState = MainMenu.GetInstance();
        }
        public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent) {
            if (eventType == GameEventType.GameStateEvent) {
                switch (gameEvent.Message) {
                case "CHANGE_STATE":
                    var state =
                        StateTransformer.TransformStringToState(gameEvent.Parameter1);
                    SwitchState(state);
                    break;
                }
            }
            if (eventType == GameEventType.InputEvent) {
                ActiveState.HandleKeyEvent(gameEvent.Message,
                    gameEvent.Parameter1);
            }
        }
        
        public void SwitchState(GameStateType stateType) {
            switch (stateType) {
            case GameStateType.GameRunning:
                ActiveState = GameRunning.GetInstance();
                break;
            case GameStateType.GamePaused:
                ActiveState = GamePaused.GetInstance();
                break;
            case GameStateType.MainMenu:
                ActiveState = MainMenu.GetInstance();
                break;
            case GameStateType.ChooseLevel:
                ActiveState = ChooseLevel.GetInstance();
                break;
            }
        }
    }
}
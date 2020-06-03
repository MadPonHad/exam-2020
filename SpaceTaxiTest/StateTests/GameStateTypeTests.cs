
using NUnit.Framework;
using SpaceTaxi_1.SpaceTaxiStates;

namespace SpaceTaxiTest.StateTests {
    public class GameStateTypeTests {
        
        //Tests all options in the StringToState Method
        [TestCase]
        public void StringToState() {
            Assert.True(StateTransformer.TransformStringToState("CHOOSE_LEVEL").
                Equals(GameStateType.ChooseLevel));
            Assert.True(StateTransformer.TransformStringToState("MAIN_MENU").
                Equals(GameStateType.MainMenu));
            Assert.True(StateTransformer.TransformStringToState("GAME_PAUSED").
                Equals(GameStateType.GamePaused));
            Assert.True(StateTransformer.TransformStringToState("GAME_RUNNING").
                Equals(GameStateType.GameRunning));
            
        }
        
        
        //Tests all options in the StateToString Method
        [TestCase]
        public void StateToStringTests() {
            Assert.True(StateTransformer.TransformStateToString(GameStateType.ChooseLevel).
                Equals("CHOOSE_LEVEL"));
            Assert.True(StateTransformer.TransformStateToString(GameStateType.MainMenu).
                Equals("MAIN_MENU"));
            Assert.True(StateTransformer.TransformStateToString(GameStateType.GamePaused).
                Equals("GAME_PAUSED"));
            Assert.True(StateTransformer.TransformStateToString(GameStateType.GameRunning).
                Equals("GAME_RUNNING"));
        }

    }
}
using System.Collections.Generic;
using DIKUArcade;
using DIKUArcade.EventBus;
using NUnit.Framework;
using SpaceTaxi_1.SpaceTaxiStates;
using SpaceTaxi_1.Utilities;

namespace SpaceTaxiTest.StateTests {
    public class StateMachineTests {
        private StateMachine stateMachine;

        [SetUp]
        public void Init() {
            Window.CreateOpenGLContext();
            
            EventBus.GetBus().InitializeEventBus(new List<GameEventType>(){GameEventType.InputEvent,
                GameEventType.GameStateEvent, GameEventType.PlayerEvent});
            
            stateMachine = new StateMachine();
            
            EventBus.GetBus().Subscribe(GameEventType.GameStateEvent, stateMachine);
            EventBus.GetBus().Subscribe(GameEventType.InputEvent, stateMachine);
            EventBus.GetBus().Subscribe(GameEventType.PlayerEvent, stateMachine);
            
            
        }
        
        // Checks that the initial active state is Main Menu
        [TestCase]
        public void InitialState() {
            Assert.True(stateMachine.ActiveState == MainMenu.GetInstance());            
        }
        
        //Tests that the SwitchState method actually changes the active state, and does so for all
        //states (and both maps in GameRunning
        
        [TestCase]
        public void SwitchingState() {
            stateMachine.SwitchState(GameStateType.GamePaused);
            Assert.True(stateMachine.ActiveState == GamePaused.GetInstance());                        
            
            stateMachine.SwitchState(GameStateType.ChooseLevel);
            Assert.True(stateMachine.ActiveState == ChooseLevel.GetInstance());
            
            stateMachine.SwitchState(GameStateType.MainMenu);
            Assert.True(stateMachine.ActiveState == MainMenu.GetInstance());
            
            GameRunning.NewInstance("the-beach.txt");
            stateMachine.SwitchState(GameStateType.GameRunning);
            Assert.True(stateMachine.ActiveState == GameRunning.GetInstance());
            
            stateMachine.SwitchState(GameStateType.MainMenu);
            Assert.True(stateMachine.ActiveState == MainMenu.GetInstance());
            
            GameRunning.NewInstance("short-n-sweet.txt");
            stateMachine.SwitchState(GameStateType.GameRunning);
            Assert.True(stateMachine.ActiveState == GameRunning.GetInstance());
                        
        }                
    }
}
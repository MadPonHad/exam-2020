using DIKUArcade;

using NUnit.Framework;
using SpaceTaxi_1.LevelLoading;

namespace SpaceTaxiTest.LevelLoadingTests {
    public class LevelCreatorTest {
        private LevelCreator levelCreator;        
        [SetUp]
        public void Init() {
            Window.CreateOpenGLContext();                        
        }
        //Checks that the right number of Entites are created by the levelCreator
        //levels fields are private, and therefor it is tested the right data is passed
        //on to the level objects for both levels.
        [TestCase]
        public void LevelSnS() {
            levelCreator = new LevelCreator();
            levelCreator.CreateLevel("short-n-sweet.txt");            
            Assert.True(levelCreator.Obstacles.CountEntities() == 173);
            Assert.True(levelCreator.Platforms.Count == 13);
            Assert.True(levelCreator.Exits.CountEntities() == 6);
            Assert.True(levelCreator.playerpos.ToString() == "Vec2F(0,7749998,0,8261199)");
            
        }
        [TestCase]
        public void LevelTb() {
            levelCreator = new LevelCreator();
            levelCreator.CreateLevel("the-beach.txt");            
            Assert.True(levelCreator.Obstacles.CountEntities() == 253);
            Assert.True(levelCreator.Platforms.Count == 27);
            Assert.True(levelCreator.Exits.CountEntities() == 6);
            Assert.True(levelCreator.playerpos.ToString() ==  "Vec2F(0,3750001,0,3479497)");
            
        }
        
    }
}
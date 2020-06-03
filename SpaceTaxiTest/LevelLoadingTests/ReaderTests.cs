using System.Security.Principal;
using DIKUArcade;
using NUnit.Framework;
using SpaceTaxi_1.LevelLoading;


namespace SpaceTaxiTest.LevelLoadingTests {
    public class ReaderTests {
        private Reader reader;

        [SetUp]

        public void Init() {
            Window.CreateOpenGLContext();                        
        }
        //Checks reader gets the correct number of map chars, platform chars, obstacle chars,
        // and Customers in level Short-n-Sweet
        [TestCase]
        public void SnS() {
            reader = new Reader();
            reader.ReadFile("short-n-sweet.txt");            
            Assert.True(reader.MapData.Count == 920);
            Assert.True(reader.MetaData.Count == 1);
            Assert.True(reader.LegendData.Count == 22);
            Assert.True(reader.CustomerData.Count == 1);
        }
        
        //Checks reader gets the correct number of map chars, platform chars, obstacle chars,
        // and Customers in level The-Beach
       [TestCase]
        public void TB() {
            reader = new Reader();
            reader.ReadFile("the-beach.txt");
            Assert.True(reader.MapData.Count == 920);
            Assert.True(reader.MetaData.Count == 3);
            Assert.True(reader.LegendData.Count == 29);
            Assert.True(reader.CustomerData.Count == 2);
        }
        
    }
}
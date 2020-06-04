using System.Collections.Generic;
using DIKUArcade.Entities;
using SpaceTaxi_1.Entities;



namespace SpaceTaxi_1.LevelLoading {
    public class Level {
        // Add fields as needed
        private EntityContainer obstacles;
        private List<Platform> platforms;
        private EntityContainer exits;        

        public Level(EntityContainer ob, List<Platform> plat, EntityContainer exit) {
            obstacles = new EntityContainer();
            platforms = new List<Platform>();
            exits = new EntityContainer();            
            obstacles = ob;
            platforms = plat;
            exits = exit;           
        }

        public void RenderLevelObjects() {
            // all rendering here
            obstacles.RenderEntities();
            foreach (var plat in platforms) {
                plat.RenderEntity();                
            }            
            exits.RenderEntities();
            
            
            
        }
    }
}

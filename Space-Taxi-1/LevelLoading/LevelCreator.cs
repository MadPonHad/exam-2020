
using System.Collections.Generic;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using SpaceTaxi_1.Entities;
using SpaceTaxi_1.Utilities;

namespace SpaceTaxi_1.LevelLoading {
    public class LevelCreator {
        // add fields as you see fit
        private Reader reader;
        public EntityContainer Obstacles { get; }
        
        public List<Platform> Platforms { get; }
        
        public EntityContainer Exits { get; }

        public List<string[]> Customer;

        public Vec2F playerpos;

        public LevelCreator() {
            reader = new Reader();
            Obstacles = new EntityContainer();
            Platforms = new List<Platform>();
            Exits = new EntityContainer();
            playerpos = new Vec2F();
            Customer = new List<string[]>();
        }
        
        public Level CreateLevel(string levelname) {           
            Level level = new Level(Obstacles, Platforms,Exits);
            Customer = reader.CustomerData;                        
            reader.ReadFile(levelname);
            MapCreator(reader.MapData);
                                                                        
            return level;
        }
        
        /// <summary>
        /// Takes the list of chars the reader has read and add the map entities
        /// to the corresponding entity containers, goes top-left to bottom-right
        /// </summary>
        /// <param name="map"></param>
        public void MapCreator(List<char> map) {
            const float width = 0.025F;
            const float height = 0.04347F;
            const float xMin = 0;
            const float yMin = 0;
            const float xMax = 1 - width;
            const float yMax = 1 - height;
            
            
            Vec2F imageExtent = new Vec2F(width, height); 
            
            var x = xMin;
            var y = yMax;

            var index = 0;

            while (y > yMin) {                
                while (x < xMax) {
                    // Increment index variable when coordinate is empty
                    if (map[index].ToString() == " ") {
                        index += 1;
                        
                    } else {                                                                                                      
                        if (reader.LegendData.ContainsKey(map[index]) && !reader.MetaData.ContainsKey(map[index])) {
                            var shape = new StationaryShape(new Vec2F(x, y), imageExtent);
                            var file = 
                                Path.Combine(Utils.GetImageFilePath(reader.LegendData[map[index]]));
                            Obstacles.AddStationaryEntity(shape, new Image(file));
                        }

                        if (reader.MetaData.ContainsKey(map[index])) {
                            var shape = new StationaryShape(new Vec2F(x, y), imageExtent);
                            var file =
                                Path.Combine((Utils.GetImageFilePath(reader.MetaData[map[index]])));                            
                            Platforms.Add(new Platform(shape, new Image(file), map[index]));
                        }

                        if (map[index] == '^') {
                            var shape = new StationaryShape(new Vec2F(x, y), imageExtent);
                            var file =
                                Path.Combine(Utils.GetImageFilePath("aspargus-passage.png"));
                            Exits.AddStationaryEntity(shape, new Image(file));
                        }
                        
                        if (map[index] == '>') {
                            playerpos = new Vec2F(x, y);
                        }                                                   
                        index += 1;                        
                    }                   
                    x += width;                    
                }
                x = 0;
                y -= height;                
            }            
        }
        
    }
}

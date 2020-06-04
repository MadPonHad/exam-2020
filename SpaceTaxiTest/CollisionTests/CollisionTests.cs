using System;
using System.Collections.Generic;
using System.IO;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using NUnit.Framework;
using SpaceTaxi_1;
using SpaceTaxi_1.Entities;
using SpaceTaxi_1.LevelLoading;
using SpaceTaxi_1.Utilities;

namespace SpaceTaxiTest.CollisionTests {
    public class CollisionTests {
        
        private Player player;
        private Customer customer;
        private EntityContainer entityContainer;
        private List<Platform> Platforms;
        private LevelCreator levelCreator;

        [SetUp]
        public void Init() {
            Window.CreateOpenGLContext();
        }
        
        //Test of both obstacle and Exit collisions, methods are identical
        //except for return values
        [TestCase]
        public void ExitObstacleCollisionTest() {
            
            var shape = new StationaryShape(new Vec2F(0.0F,0.5F),
                new Vec2F(1F,0.5F));
            var file = Path.Combine(Utils.GetImageFilePath("obstacle.png"));
            player = new Player();
            player.SpawnPos(new Vec2F(0.5F, 0.4F));
            entityContainer = new EntityContainer();
            entityContainer.AddStationaryEntity(shape, new Image(file));
            bool col = false;
            while (player.Shape.Position.Y < 0.55F && col == false) {                
                player.MoveUp = true;
                player.Move();                
                col = Collision.ExitCollision(player, entityContainer);
                Console.WriteLine(col);
            }
            Assert.True(col);
        }
        
        [TestCase]
        public void PlatformCollisionTest() {
            
            var shape = new StationaryShape(new Vec2F(0.0F,0.0F),
                new Vec2F(1F,0.5F));
            var file = Path.Combine(Utils.GetImageFilePath("obstacle.png"));
            player = new Player();
            player.SpawnPos(new Vec2F(0.5F, 0.6F));
            Platforms = new List<Platform>();
            Platforms.Add(new Platform(shape, new Image(file), 'i'));
            bool col = false;
            char returnVal2 = 'e';
            while (player.Shape.Position.Y > 0.45F && col == false) {                                
                player.Move();                
                col = Collision.PlatformCollision(player, Platforms).Item1;
                returnVal2 = Collision.PlatformCollision(player, Platforms).Item2;
                Console.WriteLine(col);
            }
            Assert.True(col);
            Assert.True(returnVal2 =='i');
        }

        [TestCase]
        public void CustomerCollision() {                        
            player = new Player();
            player.SpawnPos(new Vec2F(0.5F, 0.6F));
            levelCreator = new LevelCreator();            
            levelCreator.CreateLevel("the-beach.txt");
            customer = new Customer(levelCreator.Customer[0]);
            customer.SpawnPosition(new Vec2F(0.5F,0.5F));
            bool col = false;
            
            while (player.Shape.Position.Y > 0.45F && col == false) {                                
                player.Move();
                col = Collision.CustomerCollision(player,customer);                
                Console.WriteLine(col);
            }
            Assert.True(col);
            
        }
    }
}

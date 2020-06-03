using System;
using DIKUArcade;
using DIKUArcade.Math;
using NUnit.Framework;
using SpaceTaxi_1.Entities;
using SpaceTaxi_1.Enums;
using SpaceTaxi_1.SpaceTaxiStates;

namespace SpaceTaxiTest.EntitiesTests {
    public class PlayerTests {
        private Player player;
        private Orientation orientation;
        
        [SetUp]
        public void Init() {
            Window.CreateOpenGLContext();            
        }
        // Testing the SpawnPos method that sets the players position
        [TestCase]
        public void Position() {
            player = new Player();
            player.SpawnPos(new Vec2F(0.5F,0.5F));            
            Assert.True(player.Shape.Position.ToString() == "Vec2F(0,5,0,5)");
        }
        //Testing the player movement (Move method), test names show which movement
        [TestCase]
        public void Gravity() {
            player = new Player();
            player.SpawnPos(new Vec2F(0.5F,0.5F));            
            player.Move();
            Assert.True(player.Shape.Position.Y<0.5F);            
        }
        
        [TestCase]
        public void MoveUp() {
            player = new Player();
            player.SpawnPos(new Vec2F(0.5F,0.5F));
            player.MoveUp = true;
            player.Move();
            Assert.True(player.Shape.Position.Y>0.5F);            
        }
        
        [TestCase]
        public void MoveRight() {
            player = new Player();
            player.SpawnPos(new Vec2F(0.5F,0.5F));            
            player.MoveRight = true;
            player.Move();
            Assert.True(player.Shape.Position.X > 0.5F);            
        }
        
        [TestCase]
        public void MoveLeft() {
            player = new Player();
            player.SpawnPos(new Vec2F(0.5F,0.5F));            
            player.MoveLeft = true;
            player.Move();
            Assert.True(player.Shape.Position.X < 0.5F);            
        }
        
        [TestCase]
        public void MoveUpRight() {
            player = new Player();
            player.SpawnPos(new Vec2F(0.5F,0.5F));
            player.MoveUp = true;
            player.MoveRight = true;
            player.Move();
            Assert.True(player.Shape.Position.Y>0.5F && player.Shape.Position.X > 0.5F);            
        }
        
        [TestCase]
        public void MoveUpLeft() {
            player = new Player();
            player.SpawnPos(new Vec2F(0.5F,0.5F));
            player.MoveUp = true;
            player.MoveLeft = true;
            player.Move();
            Assert.True(player.Shape.Position.Y>0.5F && player.Shape.Position.X < 0.5F);            
        }

        
    }
}
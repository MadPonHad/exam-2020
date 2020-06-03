
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using SpaceTaxi_1.LevelLoading;
using SpaceTaxi_1.Utilities;

namespace SpaceTaxi_1.Entities {
    public class Customer {
        public string name;
        public int SpawnTime;
        public char StartPlatform;
        public char DropOffPlatform;
        public int dropOffTime;
        public int dropOffPoints;
        public StationaryShape Shape;
        public Image Image;
        public Entity Entity;

        public bool IsRendered;
        public int StopRenderTime;
        public bool IsDroppedOff;

       /// <summary>
       /// Constructor takes a string[] and gives data to fields 
       /// </summary>
       /// <param name="lst"></param>
        public Customer(string[] lst) {
            name = lst[1];
            SpawnTime = int.Parse(lst[2]);
            StartPlatform = char.Parse(lst[3]);
            if (lst[4].Length > 1) {
                var dop = lst[4].Split('^')[1];
                DropOffPlatform = char.Parse(dop);
            } else {
                DropOffPlatform = char.Parse(lst[4]);
            }             
            dropOffTime = int.Parse(lst[5]);
            dropOffPoints = int.Parse(lst[6]);
            
            Shape = new StationaryShape(new Vec2F(), new Vec2F(0.05F,0.05F));
            Image = new Image(Utils.GetImageFilePath("CustomerStandRight.png"));
            Entity = new Entity(Shape, Image);

            IsRendered = false;
            IsDroppedOff = false;
            StopRenderTime = 0;

        }

        public void SpawnPosition(Vec2F vec2F) {
            Shape.Position = vec2F;
        }

        public void RenderCustomer(bool spawned) {            
            if (IsRendered) {
                Entity.RenderEntity();
            }
        }
    




    }
}
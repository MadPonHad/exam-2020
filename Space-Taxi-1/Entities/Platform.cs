using System.Security.Cryptography.X509Certificates;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace SpaceTaxi_1.Entities {
    public class Platform : Entity{
        
        public char PlatformType;
        
        public Platform(StationaryShape shape, Image img, char plat) : base(shape, img){
            PlatformType = plat;
        }
    }
}
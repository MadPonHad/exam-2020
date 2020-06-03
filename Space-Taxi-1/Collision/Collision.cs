
using System.Collections.Generic;

using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Physics;
using SpaceTaxi_1.Entities;
using SpaceTaxi_1.Utilities;

namespace SpaceTaxi_1 {
    public class Collision {        
        
        
        /// <summary>
        /// All methods in this class checks for collisions between entities using the
        /// CollisionDetetion from DIKUArcade Platform 
        /// when there is a "proper" platform collision, ends game otherwise
        /// </summary>
        public static void ObstacleCollision(Player player, EntityContainer entityContainer ) {
            
            foreach (Entity element in entityContainer) {
                var collisionData = CollisionDetection.Aabb(player.Shape, element.Shape);
                if (collisionData.Collision) {
                    EventBus.GetBus().RegisterEvent(
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.GameStateEvent, "", "CHANGE_STATE",
                            "MAIN_MENU", ""));                    
                }                             
            }            
        }

        public static bool ExitCollision(Player player, EntityContainer entityContainer) {
            foreach (Entity element in entityContainer) {
                var collisionData = CollisionDetection.Aabb(player.Shape, element.Shape);
                if (collisionData.Collision) {
                    return true;
                }
                
            }

            return false;
        }
        
        public static (bool, char) PlatformCollision(Player player, List<Platform> entityContainer) {
            foreach (Platform element in entityContainer) {
                var collisionData = CollisionDetection.Aabb(player.Shape, element.Shape);                
                if (collisionData.Collision) {                                                                                  
                    if (collisionData.CollisionDir == CollisionDirection.CollisionDirDown) {                        
                        return (true, element.PlatformType);
                    } else {
                        EventBus.GetBus().RegisterEvent(GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.GameStateEvent, "", "CHANGE_STATE",
                            "MAIN_MENU", ""));
                    }   
                    
                }
            }

            return (false, '.');
        }

        public static bool CustomerCollision(Player player, Customer customer) {            
            var collisionData = CollisionDetection.Aabb(player.Shape, customer.Shape);
            if (collisionData.Collision) {
                return true;                    
            }            
            return false;
        }
        
    }
}
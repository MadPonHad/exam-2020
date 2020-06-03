using System.Drawing;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace SpaceTaxi_1.Entities {
    public class PointDisplay {
        private int points;
        private Text pointsDisplay;

        public PointDisplay() {
            points = 0;
            pointsDisplay = new Text(points.ToString(),new Vec2F(0.8F,0.8F), 
                new Vec2F(0.2F, 0.2F));
        }

        public void SuccessfulDrop(int drop) {
            points += drop;

        }

        public void RenderPoints() {
            pointsDisplay.SetText("Points: " + points);
            pointsDisplay.SetColor(Color.LimeGreen);
            pointsDisplay.RenderText();
        }
    }
}
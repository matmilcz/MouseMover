using System;
using System.Windows.Forms;
using System.Drawing;

namespace MouseMover
{
    public class MouseDefaultRouter : IMouseRouter
    {
        // f(x) = mx + b
        private float m = 0;
        private float b = 0;

        private Point destinationPosition = Cursor.Position;

        private void SetDestinationPosition()
        {
            Random random = new Random();
            int destX = random.Next(1, Screen.PrimaryScreen.Bounds.Width - 1);
            int destY = random.Next(1, Screen.PrimaryScreen.Bounds.Height - 1);

            destinationPosition = new Point(destX, destY);
        }

        public void SetRoute()
        {
            SetDestinationPosition();

            Point currentPosition = Cursor.Position;

            float dx = destinationPosition.X - currentPosition.X;
            if (dx == 0.0f)
            {
                dx = 1.0f; // if dx == 0, then m is whatever, I do not care
            }
            m = (destinationPosition.Y - currentPosition.Y) / dx;
            b = currentPosition.Y - (m * currentPosition.X);
        }

        public void RouteToNextPoint(int routeStep)
        {
            while (Math.Abs(destinationPosition.X - Cursor.Position.X) <= routeStep)
            {
                SetRoute();
            }

            int nextX = destinationPosition.X > Cursor.Position.X ? Cursor.Position.X + routeStep : Cursor.Position.X - routeStep;
            Cursor.Position = new Point(nextX, (int)((nextX * m) + b));
        }
    }
}

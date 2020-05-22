using System;
using System.Windows.Forms;
using System.Drawing;

namespace MouseMover
{
    public class MouseDefaultRouter : IMouseRouter
    {
        // f(x) = mx + b
        private double m = 0;
        private double b = 0;

        private Point endPosition = new Point();
        private PointF currPosition = new PointF();

        private void SetPositions()
        {
            Random random = new Random();
            int destX = random.Next(1, Screen.PrimaryScreen.Bounds.Width - 1);
            int destY = random.Next(1, Screen.PrimaryScreen.Bounds.Height - 1);

            endPosition = new Point(destX, destY);
            currPosition = new PointF(Cursor.Position.X, Cursor.Position.Y);
        }

        public void SetRoute()
        {
            SetPositions();

            double dx = endPosition.X - currPosition.X;
            if (dx == 0.0f)
            {
                dx = 1.0f; // if dx == 0, then m is whatever, I do not care
            }
            m = (endPosition.Y - currPosition.Y) / dx;
            b = currPosition.Y - (m * currPosition.X);
        }

        public void RouteToNextPoint(int routeStep)
        {
            double distanceX = Math.Abs(currPosition.X - (double)endPosition.X);
            double distanceY = Math.Abs(currPosition.Y - (double)endPosition.Y);
            double distance = Math.Sqrt(Math.Pow(distanceX, 2) + Math.Pow(distanceY, 2));

            while (distance <= routeStep)
            {
                SetRoute();

                distanceX = Math.Abs(currPosition.X - (double)endPosition.X);
                distanceY = Math.Abs(currPosition.Y - (double)endPosition.Y);
                distance = Math.Sqrt(Math.Pow(distanceX, 2) + Math.Pow(distanceY, 2));
            }

            double routeStepX = distanceX / (distance / routeStep);

            RouteToNextPoint(routeStepX);
        }

        private void RouteToNextPoint(double routeStepX)
        {
            if ((double)endPosition.X > currPosition.X)
            {
                currPosition.X += (float)routeStepX;
            }
            else
            {
                currPosition.X -= (float)routeStepX;
            }
            currPosition.Y = (float)((m * currPosition.X) + b);

            Cursor.Position = new Point((int)currPosition.X, (int)currPosition.Y);
        }
    }
}

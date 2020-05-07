using System;
using System.Windows.Forms;
using System.Drawing;

namespace MouseMover
{
    class MouseRouter
    {
        private Point[] route;
        private uint routeIdx = 0;

        public Point GetCurrPoint()
        {
            if (routeIdx == 0)
            {
                return new Point(Cursor.Position.X, Cursor.Position.Y);
            }
            else
            {
                return route[routeIdx];
            }
        }

        public Point GetNextPoint(uint routeStep)
        {
            if (routeIdx == 0)
            {
                SetRoute();
            }

            if (routeIdx + routeStep < route.Length)
            {
                routeIdx += routeStep;
            }
            else
            {
                SetRoute();
                while (!(routeStep < route.Length))
                {
                    SetRoute();
                }
                routeIdx = routeStep;
            }

            return route[routeIdx];
        }

        public void SetRoute()
        {
            Random random = new Random();
            int destX = random.Next(1, Screen.PrimaryScreen.Bounds.Width - 1);
            int destY = random.Next(1, Screen.PrimaryScreen.Bounds.Height - 1);

            Point currentPosition = new Point(Cursor.Position.X, Cursor.Position.Y);
            Point destinationPosition = new Point(destX, destY);

            //
            // f(x) = mx + b
            // route[point] = m * point + b
            //
            float dx = destinationPosition.X - currentPosition.X;
            if (dx == 0.0f)
            {
                dx = 1.0f; // if dx == 0, then m is whatever, I do not care
            }
            float m = (destinationPosition.Y - currentPosition.Y) / dx;
            float b = currentPosition.Y - (m * currentPosition.X);

            route = new Point[Math.Abs(destinationPosition.X - currentPosition.X)];

            int currX = currentPosition.X;
            for (int point = 0; point < route.Length; ++point)
            {
                route[point] = new Point(currX, (int)((m * currX) + b));
                if (destinationPosition.X > currentPosition.X)
                {
                    ++currX;
                }
                else
                {
                    --currX;
                }
            }
        }
    }
}

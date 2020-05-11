using System;
using System.Windows.Forms;
using System.Drawing;

namespace MouseMover
{
    public enum ERouteType
    {
        Random
        // TODO: Add more shapes
    }

    class MouseRouter
    {
        private Point[] route;
        private uint routeIdx = 0;

        public void SetRoute(ERouteType eRouteType)
        {
            switch (eRouteType)
            {
                case ERouteType.Random:
                    SetRandomRoute(0);
                    break;
                default:
                    SetRandomRoute(0);
                    break;
            }
        }

        public Point GetNextPoint(uint routeStep)
        {
            if (routeIdx + routeStep < route.Length)
            {
                routeIdx += routeStep;
            }
            else
            {
                SetRandomRoute(routeStep, routeStep);
            }

            return route[routeIdx];
        }

        private void SetRandomRoute(uint minLength, uint newIdx)
        {
            SetRandomRoute(newIdx);
            while (!(minLength < route.Length))
            {
                SetRandomRoute(newIdx);
            }
        }

        private void SetRandomRoute(uint newIdx)
        {
            routeIdx = newIdx;

            Random random = new Random();
            int destX = random.Next(1, Screen.PrimaryScreen.Bounds.Width - 1);
            int destY = random.Next(1, Screen.PrimaryScreen.Bounds.Height - 1);

            Point currentPosition = Cursor.Position;
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

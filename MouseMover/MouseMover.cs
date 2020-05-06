using System;
using System.Windows.Forms;
using System.Drawing;

namespace MouseMover
{
    class MouseMover
    {
        private readonly Timer moveTimer = new Timer();

        private readonly int shortInterval = 16; // 60 ticks per sec
        private readonly int longInterval = 10000; // 10 sec

        private Point[] route;
        private int routeIdx = 0;
        private readonly int routeStep = 7;

        public bool Enabled { get; private set; }

        public MouseMover()
        {
            moveTimer.Tick += new EventHandler(DoTimerRoutine);
            moveTimer.Interval = shortInterval;
        }

        public void Start()
        {
            Enabled = true;
            routeIdx = 0;
            moveTimer.Start();
        }

        public void Stop()
        {
            Enabled = false;
            moveTimer.Stop();
        }

        private void DoTimerRoutine(object sender, EventArgs e)
        {
            moveTimer.Stop();

            Point previousPosition = new Point(Cursor.Position.X, Cursor.Position.Y);

            if (routeIdx == 0)
            {
                SetRoute();
            }
            
            if (routeIdx + routeStep < route.Length)
            {
                moveTimer.Interval = shortInterval;
                Cursor.Position = route[routeIdx];
                routeIdx += routeStep;
            }
            else
            {
                routeIdx = 0;
            }

            if (routeIdx - (2 * routeStep) > 0 && previousPosition != route[routeIdx - (2 * routeStep)]) // why does it work?
            {
                moveTimer.Interval = longInterval;
                routeIdx = 0;
            }

            moveTimer.Start();
        }

        private void SetRoute()
        {
            Random random = new Random();
            int destX = random.Next(1, Screen.PrimaryScreen.Bounds.Width - 1);
            int destY = random.Next(1, Screen.PrimaryScreen.Bounds.Height - 1);

            Point currentPosition = new Point(Cursor.Position.X, Cursor.Position.Y);
            Point destinationPosition = new Point(destX, destY);

            route = new Point[Math.Abs(destinationPosition.X - currentPosition.X)];

            /**
            * f(x) = mx + b
            * route[point] = m * point + b
            **/
            float dx = destinationPosition.X - currentPosition.X;
            if (dx == 0.0f)
            {
                dx = 1.0f; // if dx == 0, then m is whatever, I do not care
            }
            float m = (destinationPosition.Y - currentPosition.Y) / dx;
            float b = currentPosition.Y - (m * currentPosition.X);

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

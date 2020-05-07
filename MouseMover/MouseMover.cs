using System;
using System.Windows.Forms;
using System.Drawing;

namespace MouseMover
{
    class MouseMover
    {
        private readonly Timer movementTimer = new Timer() {
            Interval = shortInterval
        };
        private readonly ScreenAwaker screenAwaker = new ScreenAwaker();

        private const int shortInterval = 16; // 60 ticks per sec
        private const int longInterval = 10000; // 10 sec

        private Point[] route;
        private int routeIdx = 0;
        private readonly int routeStep = 7;

        private bool _enabled = false;
        public bool Enabled
        {
            get => _enabled;
            set
            {
                _enabled = value;
                if (_enabled == true)
                {
                    Start();
                }
                else
                {
                    Stop();
                }
            } 
        }

        public MouseMover()
        {
            movementTimer.Tick += new EventHandler(DoTimerRoutine);
        }

        private void Start()
        {
            screenAwaker.Enabled = true;
            routeIdx = 0;
            movementTimer.Enabled = true;
        }

        private void Stop()
        {
            movementTimer.Enabled = false;
            movementTimer.Interval = shortInterval;
            screenAwaker.Enabled = false;
        }

        private void DoTimerRoutine(object sender, EventArgs e)
        {
            movementTimer.Stop();

            Point previousPosition = new Point(Cursor.Position.X, Cursor.Position.Y);

            if (routeIdx == 0)
            {
                SetRoute();
            }
            
            if (routeIdx + routeStep < route.Length)
            {
                movementTimer.Interval = shortInterval;
                Cursor.Position = route[routeIdx];
                routeIdx += routeStep;
            }
            else
            {
                routeIdx = 0;
            }

            if (routeIdx - (2 * routeStep) > 0 && previousPosition != route[routeIdx - (2 * routeStep)]) // why does it work?
            {
                movementTimer.Interval = longInterval;
                routeIdx = 0;
            }

            movementTimer.Start();
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

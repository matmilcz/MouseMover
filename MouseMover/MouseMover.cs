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
        private readonly MouseRouter mouseRouter = new MouseRouter();
        private Point prevPosition = new Point();

        private const int shortInterval = 16; // 60 ticks per sec
        private const int longInterval = 10000;

        private const uint routeStep = 7;

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
            prevPosition = Cursor.Position;
            screenAwaker.Enabled = true;
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

            Point currPosition = Cursor.Position;

            if (currPosition == prevPosition)
            {
                Cursor.Position = mouseRouter.GetNextPoint(routeStep);
                movementTimer.Interval = shortInterval;
                prevPosition = Cursor.Position;
            }
            else
            {
                mouseRouter.SetRoute();
                movementTimer.Interval = longInterval;
                prevPosition = currPosition;
            }

            movementTimer.Start();
        }
    }
}

using System;
using System.Windows.Forms;
using System.Drawing;

namespace MouseMover
{
    class MouseMover
    {
        private const int shortInterval = 16; // 60 ticks per sec
        private const int longInterval = 2000;
        private const uint routeStep = 7;

        private readonly Timer shortTimer = new Timer()
        {
            Interval = shortInterval
        };
        private readonly Timer longTimer = new Timer()
        {
            Interval = longInterval
        };
        private readonly ScreenAwaker screenAwaker = new ScreenAwaker();
        private readonly MouseRouter mouseRouter = new MouseRouter();
        private Point prevPosition = new Point();

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
            shortTimer.Tick += new EventHandler(ShortTimerTick);
            longTimer.Tick += new EventHandler(LongTimerTick);
        }

        private void Start()
        {
            mouseRouter.SetRoute(ERouteType.Random);
            prevPosition = Cursor.Position;

            shortTimer.Enabled = true;
            longTimer.Enabled = false;
            screenAwaker.Enabled = true;
        }

        private void Stop()
        {
            shortTimer.Enabled = false;
            longTimer.Enabled = false;
            screenAwaker.Enabled = false;
        }

        private void ShortTimerTick(object sender, EventArgs e)
        {
            shortTimer.Enabled = false;

            if (longTimer.Enabled == false)
            {
                if (Cursor.Position == prevPosition)
                {
                    Cursor.Position = mouseRouter.GetNextPoint(routeStep);
                }
                else
                {
                    longTimer.Enabled = true;
                }

                prevPosition = Cursor.Position;
            }

            shortTimer.Enabled = true;
        }

        private void LongTimerTick(object sender, EventArgs e)
        {
            if (Cursor.Position == prevPosition)
            {
                mouseRouter.SetRoute(ERouteType.Random);
                longTimer.Enabled = false;
            }
            else
            {
                prevPosition = Cursor.Position;
            }
        }
    }
}

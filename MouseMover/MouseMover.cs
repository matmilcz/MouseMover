using System;
using System.Windows.Forms;
using System.Drawing;

namespace MouseMover
{
    class MouseMover
    {
        private readonly CatMover catMover;

        private const int SHORT_INTERVAL = 16; // 60 ticks per sec
        private const int LONG_INTERVAL = 10000;
        private const byte MAX_BRIGHTNESS = 64; // TODO: implement setting the previous brightness value
        private const byte MIN_BRIGHTNESS = 0;

        private readonly Timer shortTimer = new Timer()
        {
            Interval = SHORT_INTERVAL
        };
        private readonly Timer longTimer = new Timer()
        {
            Interval = LONG_INTERVAL
        };
        private readonly DisplaySnoozeTimeCtrl screenAwaker = new DisplaySnoozeTimeCtrl();
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

        public MouseMover(CatMover _catMover)
        {
            catMover = _catMover;
            shortTimer.Tick += new EventHandler(ShortTimerTick);
            longTimer.Tick += new EventHandler(LongTimerTick);
        }

        private void Start()
        {
            DisplayBrightnessCtrl.SetDisplayBrightness(MIN_BRIGHTNESS);

            mouseRouter.SetRoute(ERouteType.Random);
            prevPosition = Cursor.Position;

            shortTimer.Enabled = true;
            longTimer.Enabled = false;
            screenAwaker.Enabled = true;
        }

        private void Stop()
        {
            DisplayBrightnessCtrl.SetDisplayBrightness(MAX_BRIGHTNESS);

            shortTimer.Enabled = false;
            longTimer.Enabled = false;
            screenAwaker.Enabled = false;
        }

        private void ShortTimerTick(object sender, EventArgs e)
        {
            shortTimer.Enabled = false;

            if (longTimer.Enabled == false)
            {
                if (Math.Abs(Cursor.Position.X - prevPosition.X) <= MouseRouter.ROUTE_STEP &&
                    Math.Abs(Cursor.Position.Y - prevPosition.Y) <= MouseRouter.ROUTE_STEP)
                {
                    mouseRouter.RouteToNextPoint();
                }
                else
                {
                    DisplayBrightnessCtrl.SetDisplayBrightness(MAX_BRIGHTNESS);
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
                catMover.MoveToPoint(Cursor.Position);

                mouseRouter.SetRoute(ERouteType.Random);

                DisplayBrightnessCtrl.SetDisplayBrightness(MIN_BRIGHTNESS);

                longTimer.Enabled = false;
            }
            else
            {
                prevPosition = Cursor.Position;
            }
        }
    }
}

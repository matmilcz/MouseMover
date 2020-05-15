using System;
using System.Drawing;
using System.Windows.Forms;

namespace MouseMover
{
    class CatMover
    {
        private readonly CatForm catForm;

        private const int SHORT_INTERVAL = 16; // 60 ticks per sec
        private const int TRESH_X = 35;
        private const int TRESH_Y = 35;
        private const int VELOCITY = 14;

        private readonly Timer shortTimer = new Timer()
        {
            Interval = SHORT_INTERVAL
        };

        public CatMover(CatForm _catForm)
        {
            catForm = _catForm;
            catForm.Visible = false;
            shortTimer.Tick += new EventHandler(ShortTimerTick);
        }

        public Point GetPosition()
        {
            Point position = new Point(catForm.Location.X + TRESH_X, catForm.Location.Y + TRESH_Y);
            return position;
        }

        private void SetPosition(Point position)
        {
            catForm.Location = new Point(position.X - TRESH_X, position.Y - TRESH_Y);
        }

        public void MoveToPoint(Point position)
        {
            Point startPos = new Point(Screen.PrimaryScreen.Bounds.Width, Cursor.Position.Y);
            catForm.Visible = true;
            SetPosition(startPos);
            shortTimer.Enabled = true;
        }

        private void ShortTimerTick(object sender, EventArgs e)
        {
            shortTimer.Enabled = false;

            SetPosition(new Point(GetPosition().X - VELOCITY, GetPosition().Y));

            if(GetPosition().X > 0)
            {
                shortTimer.Enabled = true;
            }
            else 
            {
                catForm.Visible = false;
            }
            
        }
    }
}

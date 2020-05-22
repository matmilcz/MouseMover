using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseMover.ScreenControll
{
    public class ScreenCtrl
    {
        public bool ScreenAwakerEnabled
        {
            get => ScreenSnoozeTimeCtrl.Enabled;
            set => ScreenSnoozeTimeCtrl.Enabled = value;
        }

        public void SetBrightness(byte brightness)
        {
            ScreenBrightnessCtrl.SetDisplayBrightness(brightness);
        }
    }
}

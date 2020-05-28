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

        public void SetBrightness(int brightness)
        {
            ScreenBrightnessCtrl.SetDisplayBrightness(brightness);
        }

        public void ResetBrightness()
        {
            ScreenBrightnessCtrl.ResetDisplayBrightness();
        }
    }
}

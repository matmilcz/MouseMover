using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Management;

namespace MouseMover
{
    class ScreenBrightnessCtrl
    {
        public static void SetDisplayBrightness(byte brightness)
        {
            ManagementScope scope = new ManagementScope("root\\WMI");
            SelectQuery query = new SelectQuery("WmiMonitorBrightnessMethods");
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query))
            {
                using (ManagementObjectCollection objectCollection = searcher.Get())
                {
                    foreach (ManagementObject mObj in objectCollection)
                    {
                        mObj.InvokeMethod("WmiSetBrightness",
                            new Object[] { UInt32.MaxValue, brightness });
                        break;
                    }
                }
            }
        }

        public static void SetDisplayBrightness()
        {
            // TODO
        }
    }
}

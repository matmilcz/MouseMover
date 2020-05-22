using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Management;

namespace MouseMover.ScreenControll
{
    public static class ScreenBrightnessCtrl
    {
        private static readonly ManagementScope scope = new ManagementScope("root\\WMI");
        private static readonly SelectQuery query = new SelectQuery("WmiMonitorBrightnessMethods");
        private static readonly ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
        private static readonly ManagementObjectCollection objectCollection = searcher.Get();
        private static readonly ManagementObjectCollection.ManagementObjectEnumerator objectCollectionEnumerator = objectCollection.GetEnumerator();

        static ScreenBrightnessCtrl()
        {
            _ = objectCollectionEnumerator.MoveNext();
        }

        public static void SetDisplayBrightness(byte brightness)
        {
            foreach (ManagementObject mObj in objectCollection)
            {
                _ = mObj.InvokeMethod("WmiSetBrightness", new object[] { uint.MaxValue, brightness });
                break;
            }
        }

        public static int GetDisplayBrightness()
        {
            foreach (ManagementObject mObj in objectCollection)
            {
                var val = mObj.GetPropertyValue("CurrentBrightness").ToString();
                return int.Parse(val);
            }
            return -1;
        }
    }
}

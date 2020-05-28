using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Management;

namespace MouseMover.ScreenControll
{
    public static class ScreenBrightnessCtrl
    {
        private static readonly ManagementObject _brightnessInstance;
        private static ManagementBaseObject _brightnessClass;

        private static int prevBrightness;

        static ScreenBrightnessCtrl()
        {
            SetBrightnesssAPI();

            string instanceName = (string)_brightnessClass["InstanceName"];
            _brightnessInstance = new ManagementObject("root\\WMI",
                                                       "WmiMonitorBrightnessMethods.InstanceName='" + instanceName + "'",
                                                       null);

            prevBrightness = GetDisplayBrightness();
        }

        private static void SetBrightnesssAPI()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\WMI",
                                                                             "SELECT * FROM WmiMonitorBrightness");

            ManagementObjectCollection results = searcher.Get();
            ManagementObjectCollection.ManagementObjectEnumerator resultEnum = results.GetEnumerator();
            _ = resultEnum.MoveNext();
            _brightnessClass = resultEnum.Current;
        }

        public static void SetDisplayBrightness(int brightness)
        {
            prevBrightness = GetDisplayBrightness();

            if (brightness < 0)
            {
                brightness = 0;
            }

            if (brightness > 100)
            {
                brightness = 100;
            }

            var inParams = _brightnessInstance.GetMethodParameters("WmiSetBrightness");
            inParams["Brightness"] = brightness;
            inParams["Timeout"] = 0;
            _ = _brightnessInstance.InvokeMethod("WmiSetBrightness", inParams, null);
        }

        public static int GetDisplayBrightness()
        {
            SetBrightnesssAPI();

            object value = _brightnessClass.GetPropertyValue("CurrentBrightness");
            string valueString = value.ToString();
            return int.Parse(valueString);
        }

        public static void ResetDisplayBrightness()
        {
            ManagementBaseObject inParams = _brightnessInstance.GetMethodParameters("WmiSetBrightness");
            inParams["Brightness"] = prevBrightness;
            inParams["Timeout"] = 0;
            _ = _brightnessInstance.InvokeMethod("WmiSetBrightness", inParams, null);
        }
    }
}

using System;
using System.Runtime.InteropServices;
//using Microsoft.Win32;

namespace MouseMover
{
    class ScreenAwaker
    {
        [FlagsAttribute()]
        public enum EExecutionState : uint
        {
            //ES_AWAYMODE_REQUIRED = 0x40,
            Continuous = 0x80000000u,
            DisplayRequired = 0x2,
            //SystemRequired = 0x1
            // Legacy flag, should not be used.
            // ES_USER_PRESENT = 0x00000004
        }

        // Enables an application to inform the system that it is in use, 
        // thereby preventing the system from entering sleep or turning off the display 
        // while the application is running.
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern EExecutionState SetThreadExecutionState(EExecutionState esFlags);

        private bool _enabled = false;
        public bool Enabled
        {
            get => _enabled;
            set
            {
                _enabled = value;
                if (_enabled == true)
                {
                    _ = SetThreadExecutionState(EExecutionState.DisplayRequired | EExecutionState.Continuous);
                }
                else
                {
                    _ = SetThreadExecutionState(EExecutionState.Continuous);
                }
            }
        }
    }
}

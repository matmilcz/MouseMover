using System;
using System.Runtime.InteropServices;

namespace MouseMover
{
    class ScreenSnoozeTimeCtrl
    {
        [FlagsAttribute()]
        public enum EExecutionState : uint
        {
            Continuous = 0x80000000u,
            DisplayRequired = 0x2,
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

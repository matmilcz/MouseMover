using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MouseMover.AppUpdater
{
    static public class Updater
    {
        private static readonly string scriptName = "updateMouse.bat";

        private static readonly string[] script = {
            "timeout 2",
            "powershell -Command \"Invoke-WebRequest https://github.com/matmilcz/MouseMover/archive/master.zip -OutFile master.zip\"",
            "powershell -Command \"Expand-Archive -Force master.zip .\"",

            "cd MouseMover-master",
            "move \"Release\\MouseMover.exe\" \"..\\MouseMover.exe\"",
            "cd ..",

            "del master.zip",
            "rd /s /q MouseMover-master",

            "start \"\" MouseMover.exe"
        };

        public static void Update() // TODO: it downloads whole repo now...
        {
            CreateScript();
#if !DEBUG
            string strCmdText;
            strCmdText = "/C " + scriptName;
            System.Diagnostics.Process.Start("CMD.exe", strCmdText);

            Application.Exit();
#endif
        }

        private static void CreateScript()
        {
            System.IO.File.WriteAllLines(scriptName, script);
        }

        public static void DeleteScript()
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo
            {
                WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                FileName = "cmd.exe",
                Arguments = "/C del " + scriptName
            };
            process.StartInfo = startInfo;
            process.Start();
        }
    }
}

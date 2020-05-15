using System;
using System.Windows.Forms;

namespace MouseMover
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            CatForm catForm = new CatForm
            {
                Visible = false
            };

            _ = new MainForm(catForm);

            Application.Run();
        }
    }
}

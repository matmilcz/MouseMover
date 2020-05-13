using System;
using System.Windows.Forms;

namespace MouseMover
{
    public partial class MainForm : Form
    {
        private readonly MouseMover mouseMover = new MouseMover();
        public MainForm()
        {
            InitializeComponent();
            InitializeSystemTrayContextMenu();
        }

        private void InitializeSystemTrayContextMenu()
        {
            notifyIcon.ContextMenuStrip = new ContextMenuStrip();
            _ = notifyIcon.ContextMenuStrip.Items.Add("Start", null, NotifyIcon_ContextMenu_Start_Stop);
            _ = notifyIcon.ContextMenuStrip.Items.Add("About", null, NotifyIcon_ContextMenu_About);
            _ = notifyIcon.ContextMenuStrip.Items.Add("Exit", null, NotifyIcon_ContextMenu_Exit);
        }

        private void NotifyIcon_ContextMenu_Start_Stop(object sender, EventArgs e)
        {
            if (mouseMover.Enabled == true)
            {
                notifyIcon.ContextMenuStrip.Items[0].Text = "Start";
                mouseMover.Enabled = false;
            }
            else
            {
                notifyIcon.ContextMenuStrip.Items[0].Text = "Stop";
                mouseMover.Enabled = true;
            }
        }

        private void NotifyIcon_ContextMenu_About(object sender, EventArgs e)
        {
#if DEBUG
            const string debug = "DEBUG version\n\n";
#else
            const string debug = "";
#endif
            const string thanks = "Special thanks to my beta tester from NS.";
            const string attribution = "Icons made by https://www.flaticon.com/authors/freepik from https://www.flaticon.com.";
            _ = MessageBox.Show(debug + thanks + "\n\n" + attribution);
        }

        private void NotifyIcon_ContextMenu_Exit(object sender, EventArgs e)
        {
            notifyIcon.Visible = false;
            Application.Exit();
        }

        private void NotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            // TODO: for future release
#if DEBUG
            notifyIcon.Visible = false;
            Show();
            WindowState = FormWindowState.Normal;
#endif
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                notifyIcon.Visible = true;
                Hide();
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            notifyIcon.Visible = false;
            Application.Exit();
        }
    }
}

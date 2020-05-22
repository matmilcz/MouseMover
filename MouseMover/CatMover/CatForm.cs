using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MouseMover
{
    public partial class CatForm : Form
    {
        public CatForm()
        {
            InitializeComponent();
            MakeFormTransparent();
            ShowInTaskbar = false;
        }

        private void MakeFormTransparent()
        {
            BackColor = Color.Lime;
            TransparencyKey = Color.Lime;
            FormBorderStyle = FormBorderStyle.None;
        }
    }
}

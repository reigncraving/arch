using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arch
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void AboutBut1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.BackgroundImage = Arch.Properties.Resources.textE_small;
        }
    }
}

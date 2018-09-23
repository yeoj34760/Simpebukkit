using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simplebukkit
{
    public partial class Setting : Form
    {
        public Setting()
        {
            InitializeComponent();
            numericUpDown1.Value = Properties.Settings.Default.RAM;
            if (Properties.Settings.Default.server_clean == false)
            {
                checkBox1.Checked = false;
            }
            else
            {
                checkBox1.Checked = true;
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.RAM = numericUpDown1.Value;
            if (checkBox1.Checked == false)
            {
                Properties.Settings.Default.server_clean = false;
            }
            else
            {
                Properties.Settings.Default.server_clean = true;
            }
            this.Close();
        }
    }
}

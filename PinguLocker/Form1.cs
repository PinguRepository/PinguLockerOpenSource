using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace PinguLocker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false)
            {
                button2.Enabled = false;
            }
            else
            {
                button2.Enabled = true;
                button2.ForeColor = Color.Red;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button2.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LockWindows();
        }

        public void LockWindows()
        {
            this.Hide();
            var NewForm = new WindowsLocked();
            NewForm.ShowDialog();
            ProcessStartInfo noexplorer = new ProcessStartInfo();
            noexplorer.FileName = "cmd.exe";
            noexplorer.WindowStyle = ProcessWindowStyle.Hidden;
            noexplorer.Arguments = @"/k taskkill /f /im explorer.exe";
            Process.Start(noexplorer);
            this.Close();
        }
    }
}

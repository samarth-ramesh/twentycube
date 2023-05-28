using Microsoft.Toolkit.Uwp.Notifications;
using System.Windows.Forms;
using Windows.Devices.Geolocation;
using Windows.Media.Capture;

namespace twentycube
{
    public partial class Form1 : Form
    {
        static System.Windows.Forms.Timer workTimer = new System.Windows.Forms.Timer();
        static System.Windows.Forms.Timer restTimer = new System.Windows.Forms.Timer();
        static System.Windows.Forms.Timer ticker = new System.Windows.Forms.Timer();
        static bool state = false;
        static int current_timer_location = 0;


        private static void WorkTimerEventHandler(Object myObject,
                                           EventArgs myEventArgs)
        {
            new Microsoft.Toolkit.Uwp.Notifications.ToastContentBuilder().AddText("Take a break!").Show();
            workTimer.Enabled = false;
            restTimer.Interval = 20 * 1000;
            restTimer.Enabled = true;
            current_timer_location = 20 * 1000;
        }

        private void TickEventHandler(Object myObject,
                                           EventArgs myEventArgs)
        {
            current_timer_location -= 1000;
            TimeSpan ts = TimeSpan.FromMilliseconds(current_timer_location);
            label1.Text = ts.ToString();
        }

        private static void RestTimerEventHandler(Object myObject,
                                           EventArgs myEventArgs)
        {
            new Microsoft.Toolkit.Uwp.Notifications.ToastContentBuilder().AddText("Back to work!").Show();
            workTimer.Enabled = true;
            restTimer.Enabled = false;
            current_timer_location = 60 * 20 * 1000;
        }


        public Form1()
        {
            InitializeComponent();
            workTimer.Tick += new EventHandler(WorkTimerEventHandler);
            restTimer.Tick += new EventHandler(RestTimerEventHandler);
            ticker.Tick += new EventHandler(TickEventHandler);
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            ShowInTaskbar = false;
            notifyIcon1.Visible = true;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (!state)
            {
                state = !state;
                workTimer.Interval = 60 * 20 * 1000;
                workTimer.Start();
                current_timer_location += 20 * 60 * 1000;
                button1.Text = "Stop Timer";
                ticker.Interval = 1000;
                ticker.Start();
                ticker.Enabled = true;

            }
            else
            {
                state = !state;
                workTimer.Enabled = false;
                restTimer.Enabled = false;
                button1.Text = "Start Timer";
                current_timer_location = 20 * 6000;
                ticker.Enabled = false;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void notifyIcon1_MouseDoubleClick_1(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
            System.Windows.Forms.Application.Exit();
        }
    }
};
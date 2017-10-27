using System;
using System.Drawing;
using System.Windows.Forms;

namespace PingTester
{
    public partial class Form1 : Form
    {
        private readonly Timer _timer;
        private readonly PingService _pingService;

        public UrlControl StaticUrl01 { get; set; }
        public UrlControl StaticUrl02 { get; set; }
        public UrlControl StaticUrl03 { get; set; }

        public UrlControl CustomUrl01 { get; set; }

        public Form1()
        {
            _timer = new Timer();
            _timer.Interval = 250;
            _timer.Tick += OnTimerTick;
            _pingService = new PingService();
            InitializeComponent();

            StaticUrl01 = new UrlControl("8.8.8.8", "Google DNS", 500, 20);
            StaticUrl02 = new UrlControl("8.8.8.8", "Google DNS", 500, 20);
            StaticUrl03 = new UrlControl("8.8.8.8", "Google DNS", 500, 20);

            checkBox4.Enabled = false;
            textBox1.Enabled = false;

            checkBox1.Text = StaticUrl01.DisplayName;
            checkBox2.Text = StaticUrl02.DisplayName;
            checkBox3.Text = StaticUrl03.DisplayName;

            label1.Text = String.Empty;
            label2.Text = String.Empty;
            label3.Text = String.Empty;

            _timer.Start();
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            _pingService.IssueNewPingIfActive(StaticUrl01);
            _pingService.IssueNewPingIfActive(StaticUrl02);
            _pingService.IssueNewPingIfActive(StaticUrl03);
            //_pingService.IssueNewPingIfActive(CustomUrl01);

            if (StaticUrl01.IsActive) SetPingLabelText(label1, _pingService.GetAveragePing(StaticUrl01));
            if (StaticUrl02.IsActive) SetPingLabelText(label2, _pingService.GetAveragePing(StaticUrl02));
            if (StaticUrl03.IsActive) SetPingLabelText(label3, _pingService.GetAveragePing(StaticUrl03));
            //if (CustomUrl01.IsActive) label4.Text = _pingService.GetAveragePing(CustomUrl01).ToString();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            StaticUrl01.IsActive = !StaticUrl01.IsActive;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            StaticUrl02.IsActive = !StaticUrl02.IsActive;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            StaticUrl03.IsActive = !StaticUrl03.IsActive;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            CustomUrl01.IsActive = !CustomUrl01.IsActive;
        }

        private void SetPingLabelText(Label label, long ping)
        {
            label.Text = ping.ToString();

            if (ping < 30) label.BackColor = Color.LightGreen;
            else if (ping < 80) label.BackColor = Color.LightYellow;
            else if (ping < 150) label.BackColor = Color.IndianRed;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PingTester
{
    public partial class Form1 : Form
    {
        private const Int32 PingInterval = 500;

        private readonly Timer _timer;
        private readonly PingService _pingService;

        private List<UrlControl> _availableControls;

        public UrlControl StaticUrl01 { get; set; }
        public UrlControl StaticUrl02 { get; set; }
        public UrlControl StaticUrl03 { get; set; }

        public Form1()
        {
            _pingService = new PingService();
            _availableControls = new List<UrlControl>();
            
            _timer = new Timer();
            _timer.Interval = PingInterval;
            _timer.Tick += OnTimerTick;

            InitializeComponent();
            InitControls();

            label1.Text = String.Empty;
            label2.Text = String.Empty;
            label3.Text = String.Empty;

            Text = "PingTester 1.0.0";

            _timer.Start();
        }

        private void InitControls()
        {
            _availableControls.Add(new UrlControl("8.8.8.8", "Google DNS", 500, 20));
            _availableControls.Add(new UrlControl("104.160.141.3", "League Of Legends EUW", 500, 20));
            _availableControls.Add(new UrlControl("185.60.114.159", "Overwatch EUW 1", 500, 20));
            _availableControls.Add(new UrlControl("146.66.152.1", "CS:GO EU West, Luxembourg", 500, 20));
            _availableControls.Add(new UrlControl("146.66.155.1", "CS:GO EU East Vienna", 500, 20));
            _availableControls.Add(new UrlControl("146.66.156.1", "CS:GO SW Stockholm", 500, 20));
            _availableControls.Add(new UrlControl("185.60.112.157", "Starcraft II Europe 1", 500, 20));
            _availableControls.Add(new UrlControl("185.60.112.158", "Starcraft II Europe 2", 500, 20));
            _availableControls.Add(new UrlControl("185.60.114.159", "Starcraft II Europe 3", 500, 20));

            _availableControls = _availableControls.OrderBy(c => c.DisplayName).ToList();

            comboBox1.Items.AddRange(_availableControls.ToArray());
            comboBox2.Items.AddRange(_availableControls.ToArray());
            comboBox3.Items.AddRange(_availableControls.ToArray());
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            _pingService.IssueNewPingIfActive(StaticUrl01);
            _pingService.IssueNewPingIfActive(StaticUrl02);
            _pingService.IssueNewPingIfActive(StaticUrl03);

            if (StaticUrl01 != null && StaticUrl01.IsActive) SetPingLabelText(label1, _pingService.GetAveragePing(StaticUrl01));
            if (StaticUrl02 != null && StaticUrl02.IsActive) SetPingLabelText(label2, _pingService.GetAveragePing(StaticUrl02));
            if (StaticUrl03 != null && StaticUrl03.IsActive) SetPingLabelText(label3, _pingService.GetAveragePing(StaticUrl03));

            SetPingWindowTitle();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (StaticUrl01 == null) return;

            var deactivation = !((CheckBox)sender).Checked;
            StaticUrl01.IsActive = !deactivation;

            if(deactivation)
            {
                label1.Text = String.Empty;
                label1.BackColor = Color.Empty;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (StaticUrl02 == null) return;

            var deactivation = !((CheckBox)sender).Checked;
            StaticUrl02.IsActive = !deactivation;

            if (deactivation)
            {
                label2.Text = String.Empty;
                label2.BackColor = Color.Empty;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (StaticUrl03 == null) return;

            var deactivation = !((CheckBox)sender).Checked;
            StaticUrl03.IsActive = !deactivation;

            if (deactivation)
            {
                label3.Text = String.Empty;
                label3.BackColor = Color.Empty;
            }
        }        

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var listBox = sender as ComboBox;
            if (listBox == null) return;

            StaticUrl01 = _availableControls.FirstOrDefault(x => x.DisplayName == ((UrlControl)listBox.SelectedItem).DisplayName);
            StaticUrl01.IsActive = checkBox1.Checked;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var listBox = sender as ComboBox;
            if (listBox == null) return;

            StaticUrl02 = _availableControls.FirstOrDefault(x => x.DisplayName == ((UrlControl)listBox.SelectedItem).DisplayName);
            StaticUrl02.IsActive = checkBox2.Checked;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            var listBox = sender as ComboBox;
            if (listBox == null) return;

            StaticUrl03 = _availableControls.FirstOrDefault(x => x.DisplayName == ((UrlControl)listBox.SelectedItem).DisplayName);
            StaticUrl03.IsActive = checkBox3.Checked;
        }

        private void SetPingLabelText(Label label, long ping)
        {
            label.Text = ping.ToString();

            if(ping < 0)
            {
                label.Text = "N/A";
                label.BackColor = Color.IndianRed;
            }
            else if (ping < 20) label.BackColor = Color.Green;
            else if (ping < 50) label.BackColor = Color.LightGreen;
            else if (ping < 100) label.BackColor = Color.Yellow;
            else if (ping < 150) label.BackColor = Color.Orange;
            else label.BackColor = Color.IndianRed;
        }

        private void SetPingWindowTitle()
        {
            if (StaticUrl01 != null && StaticUrl01.IsActive)
            {
                var ping = _pingService.GetAveragePing(StaticUrl01);
                var text = ping < 0 ? "N/A" : ping.ToString();
                Text = String.Format($"{StaticUrl01.DisplayName} - {text}");
            }
            else if (StaticUrl02 != null && StaticUrl02.IsActive)
            {
                var ping = _pingService.GetAveragePing(StaticUrl02);
                var text = ping < 0 ? "N/A" : ping.ToString();
                Text = String.Format($"{StaticUrl02.DisplayName} - {text}");
            }
            else if (StaticUrl03 != null && StaticUrl03.IsActive)
            {
                var ping = _pingService.GetAveragePing(StaticUrl03);
                var text = ping < 0 ? "N/A" : ping.ToString();
                Text = String.Format($"{StaticUrl03.DisplayName} - {text}");
            }
        }
    }
}

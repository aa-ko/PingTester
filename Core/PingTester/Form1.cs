using System;
using System.Drawing;
using System.Windows.Forms;

namespace PingTester
{
    public partial class Form1 : Form
    {
        private readonly Timer _timer;
        private readonly PingService _pingService;

        private EventHandler TimerTick;

        public String CurrentUrl { get; set; }

        private bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                if (value)
                {
                    btnActive.BackColor = Color.Lime;
                    _isActive = true;
                }
                else
                {
                    btnActive.BackColor = Color.Red;
                    _isActive = false;
                }
            }
        }

        public Form1()
        {
            _timer = new Timer();
            _timer.Interval = 250;
            TimerTick += OnTimerTick;

            _pingService = new PingService();

            InitializeComponent();
            _timer.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IsActive = !IsActive;
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            _pingService.IssueNewPing(CurrentUrl);
        }
    }
}

using System;
using System.Drawing;
using System.Windows.Forms;
using StudentManagementSystem.Helpers;

namespace StudentManagementSystem.Forms.Controls
{
    public class ToastForm : Form
    {
        private System.Windows.Forms.Timer _timer;
        private int _lifeSpan = 0;
        private const int MAX_LIFE = 100; // Ticks
        private bool _isClosing = false;
        private Label lblMessage;
        private Panel pnlColorStrip;

        public enum ToastType { Success, Error, Info, Warning }

        public ToastForm(string message, ToastType type)
        {
            InitializeComponent();
            SetupToast(message, type);
        }

        private void InitializeComponent()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.Manual;
            this.Size = new Size(350, 60);
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.BackColor = ModernTheme.SurfaceColor;
            
            pnlColorStrip = new Panel { Dock = DockStyle.Left, Width = 10 };
            
            lblMessage = new Label 
            { 
                Dock = DockStyle.Fill, 
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = ModernTheme.TextColor,
                Font = new Font("Segoe UI", 10),
                Padding = new Padding(10, 0, 0, 0)
            };

            this.Controls.Add(lblMessage);
            this.Controls.Add(pnlColorStrip);

            _timer = new System.Windows.Forms.Timer();
            _timer.Interval = 20;
            _timer.Tick += Timer_Tick;
        }

        private void SetupToast(string message, ToastType type)
        {
            lblMessage.Text = message;
            
            switch (type)
            {
                case ToastType.Success:
                    pnlColorStrip.BackColor = ModernTheme.SecondaryColor;
                    break;
                case ToastType.Error:
                    pnlColorStrip.BackColor = ModernTheme.ErrorColor;
                    break;
                case ToastType.Warning:
                    pnlColorStrip.BackColor = Color.Orange;
                    break;
                case ToastType.Info:
                default:
                    pnlColorStrip.BackColor = ModernTheme.PrimaryColor;
                    break;
            }
        }

        public static void Show(string message, ToastType type)
        {
            // Run on UI thread to ensure it doesn't block if called from background
            var toast = new ToastForm(message, type);
            toast.Show();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            // Position at bottom right
            var screen = Screen.PrimaryScreen.WorkingArea;
            this.Location = new Point(screen.Right - this.Width - 20, screen.Bottom - this.Height - 20);
            
            // Simple slide up animation or fade in could go here
            this.Opacity = 0;
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (_isClosing)
            {
                this.Opacity -= 0.05;
                if (this.Opacity <= 0)
                {
                    _timer.Stop();
                    this.Close();
                }
            }
            else
            {
                if (this.Opacity < 1)
                    this.Opacity += 0.1;
                
                _lifeSpan++;
                if (_lifeSpan > MAX_LIFE)
                {
                    _isClosing = true;
                }
            }
        }
    }
}

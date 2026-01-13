using System;
using System.Drawing;
using System.Windows.Forms;
using StudentManagementSystem.Services;
using StudentManagementSystem.Helpers;
using StudentManagementSystem.Forms.Controls;

namespace StudentManagementSystem.Forms
{
    public partial class RegisterForm : Form
    {
        private readonly AuthenticationService _authService;
        private TextBox txtFullName;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private TextBox txtConfirmPassword;
        private Panel pnlFullName;
        private Panel pnlUsername;
        private Panel pnlPassword;
        private Panel pnlConfirmPassword;
        private ModernButton btnRegister;
        private ModernButton btnCancel;
        
        // Custom Title Bar
        private Panel pnlTitleBar;
        private Button btnClose;

        // Dragging
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        public RegisterForm(AuthenticationService authService)
        {
            _authService = authService;
            InitializeComponents();
            ApplyModernEvents();
        }

        private void InitializeComponents()
        {
            this.Text = "Register New Admin";
            this.Size = new Size(500, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = ModernTheme.SurfaceColor;

            // Custom Title Bar
            pnlTitleBar = new Panel
            {
                Dock = DockStyle.Top,
                Height = 30,
                BackColor = ModernTheme.BackColor
            };
            pnlTitleBar.MouseDown += TitleBar_MouseDown;
            pnlTitleBar.MouseMove += TitleBar_MouseMove;
            pnlTitleBar.MouseUp += TitleBar_MouseUp;

            btnClose = new Button
            {
                Text = "✕",
                Dock = DockStyle.Right,
                Width = 40,
                FlatStyle = FlatStyle.Flat,
                ForeColor = ModernTheme.TextColor,
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatAppearance.MouseOverBackColor = ModernTheme.ErrorColor;
            btnClose.Click += (s, e) => this.DialogResult = DialogResult.Cancel;
            pnlTitleBar.Controls.Add(btnClose);
            this.Controls.Add(pnlTitleBar);

            var lblTitle = new Label
            {
                Text = "Create New Account",
                Location = new Point(0, 50),
                Size = new Size(500, 40),
                Font = ModernTheme.HeaderFont,
                ForeColor = ModernTheme.PrimaryColor,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Full Name
            var lblFullName = new Label 
            { 
                Text = "DESCRIPTION / FULL NAME",
                Location = new Point(50, 100),
                AutoSize = true,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = ModernTheme.SubTextColor
            };

            pnlFullName = CreateInputPanel(50, 125, out txtFullName);

            // Username
            var lblUsername = new Label
            {
                Text = "USERNAME",
                Location = new Point(50, 175),
                AutoSize = true,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = ModernTheme.SubTextColor
            };

            pnlUsername = CreateInputPanel(50, 200, out txtUsername);

            // Password
            var lblPassword = new Label
            {
                Text = "PASSWORD",
                Location = new Point(50, 250),
                AutoSize = true,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = ModernTheme.SubTextColor
            };

            pnlPassword = CreateInputPanel(50, 275, out txtPassword, true);

            // Confirm Password
            var lblConfirmPassword = new Label
            {
                Text = "CONFIRM PASSWORD",
                Location = new Point(50, 325),
                AutoSize = true,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = ModernTheme.SubTextColor
            };

            pnlConfirmPassword = CreateInputPanel(50, 350, out txtConfirmPassword, true);

            // Buttons
            btnRegister = new ModernButton
            {
                Text = "REGISTER",
                Location = new Point(50, 420),
                Size = new Size(400, 45),
                BackColor = ModernTheme.PrimaryColor,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                BorderRadius = 20,
                Cursor = Cursors.Hand
            };
            btnRegister.Click += BtnRegister_Click;

            btnCancel = new ModernButton
            {
                Text = "Cancel",
                Location = new Point(50, 480),
                Size = new Size(400, 45),
                BackColor = ModernTheme.SurfaceColor,
                ForeColor = ModernTheme.SubTextColor,
                Font = new Font("Segoe UI", 10),
                BorderRadius = 20,
                Cursor = Cursors.Hand
            };
            btnCancel.MouseEnter += (s, e) => btnCancel.ForeColor = Color.White;
            btnCancel.MouseLeave += (s, e) => btnCancel.ForeColor = ModernTheme.SubTextColor;
            btnCancel.Click += (s, e) => this.DialogResult = DialogResult.Cancel;

            this.Controls.AddRange(new Control[]
            {
                lblTitle,
                lblFullName, pnlFullName,
                lblUsername, pnlUsername,
                lblPassword, pnlPassword,
                lblConfirmPassword, pnlConfirmPassword,
                btnRegister, btnCancel
            });
        }

        private Panel CreateInputPanel(int x, int y, out TextBox textBox, bool isPassword = false)
        {
            Panel panel = new Panel
            {
                Location = new Point(x, y),
                Size = new Size(400, 40),
                BackColor = ModernTheme.InputBackColor
            };

            textBox = new TextBox
            {
                Location = new Point(10, 10),
                Size = new Size(380, 20),
                Font = ModernTheme.BodyFont,
                BackColor = ModernTheme.InputBackColor,
                ForeColor = ModernTheme.InputForeColor,
                BorderStyle = BorderStyle.None,
                UseSystemPasswordChar = isPassword
            };
            
            panel.Controls.Add(textBox);
            return panel;
        }

        private void ApplyModernEvents()
        {
            SetupHoverEffect(pnlFullName, txtFullName);
            SetupHoverEffect(pnlUsername, txtUsername);
            SetupHoverEffect(pnlPassword, txtPassword);
            SetupHoverEffect(pnlConfirmPassword, txtConfirmPassword);
        }

        private void SetupHoverEffect(Panel panel, TextBox textBox)
        {
            panel.Paint += (s, e) => DrawBorder(e.Graphics, panel.ClientRectangle, textBox.Focused ? ModernTheme.PrimaryColor : Color.Transparent);
            textBox.Enter += (s, e) => panel.Invalidate();
            textBox.Leave += (s, e) => panel.Invalidate();
        }

        private void DrawBorder(Graphics g, Rectangle rect, Color color)
        {
            if (color == Color.Transparent) return;
            using (Pen pen = new Pen(color, 2))
            {
                g.DrawRectangle(pen, 0, 0, rect.Width - 1, rect.Height - 1);
            }
        }

        // Draggable Form Logic
        private void TitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void TitleBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void TitleBar_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text) ||
                string.IsNullOrWhiteSpace(txtUsername.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("All fields are required!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Passwords do not match!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Register using O(1) hash table insertion
            bool success = _authService.Register(
                txtFullName.Text.Trim(),
                txtUsername.Text.Trim(),
                txtPassword.Text);

            if (success)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                ToastForm.Show("Username already exists", ToastForm.ToastType.Error);
            }
        }
    }
}

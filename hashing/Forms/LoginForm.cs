using System;
using System.Drawing;
using System.Windows.Forms;
using StudentManagementSystem.Services;
using StudentManagementSystem.Helpers;
using StudentManagementSystem.Forms.Controls;

namespace StudentManagementSystem.Forms
{
    /// <summary>
    /// Login Form - Demonstrates O(1) authentication using Hash Table
    /// </summary>
    public partial class LoginForm : Form
    {
        private readonly AuthenticationService _authService;
        private ModernButton btnLogin;
        private ModernButton btnRegister;
        private Label lblTitle;
        private Label lblUsername;
        private Label lblPassword;
        private Label lblComplexity;
        private Panel panelMain;
        private Panel pnlUsername;
        private Panel pnlPassword;
        private TextBox txtUsername;
        private TextBox txtPassword;
        
        // Custom Title Bar parts
        private Panel pnlTitleBar;
        private Button btnClose;
        private Button btnMinimize;
        
        // Dragging
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        public LoginForm(AuthenticationService authService)
        {
            _authService = authService;
            InitializeComponents();
            ApplyModernEvents();
        }

        private void InitializeComponents()
        {
            // Form settings
            this.Text = "Student Management System - Login";
            this.Size = new Size(950, 600); // Larger, more modern size
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None; // Remove default border
            this.BackColor = ModernTheme.BackColor;
            this.Icon = null; 

            // Custom Title Bar
            pnlTitleBar = new Panel
            {
                Dock = DockStyle.Top,
                Height = 30,
                BackColor = Color.Transparent 
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
            btnClose.Click += (s, e) => Application.Exit();

            btnMinimize = new Button
            {
                Text = "—",
                Dock = DockStyle.Right,
                Width = 40,
                FlatStyle = FlatStyle.Flat,
                ForeColor = ModernTheme.TextColor,
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand
            };
            btnMinimize.FlatAppearance.BorderSize = 0;
            btnMinimize.FlatAppearance.MouseOverBackColor = ModernTheme.SurfaceColor;
            btnMinimize.Click += (s, e) => this.WindowState = FormWindowState.Minimized;

            pnlTitleBar.Controls.Add(btnMinimize);
            pnlTitleBar.Controls.Add(btnClose);
            this.Controls.Add(pnlTitleBar);

            // Main Panel (Center Card)
            panelMain = new Panel
            {
                Size = new Size(400, 480),
                BackColor = ModernTheme.SurfaceColor,
                Location = new Point((this.Width - 400) / 2, (this.Height - 480) / 2)
            };

            // Title Label
            lblTitle = new Label
            {
                Text = "Student Management System",
                Location = new Point(0, 40),
                Size = new Size(400, 40),
                Font = ModernTheme.HeaderFont,
                ForeColor = ModernTheme.PrimaryColor,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Subtitle
            lblComplexity = new Label
            {
                Text = "Sign in to continue",
                Location = new Point(0, 80),
                Size = new Size(400, 20),
                Font = ModernTheme.BodyFont,
                ForeColor = ModernTheme.SubTextColor,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Username Section
            lblUsername = new Label
            {
                Text = "USERNAME",
                Location = new Point(50, 130),
                Size = new Size(300, 20),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = ModernTheme.SubTextColor
            };

            pnlUsername = new Panel
            {
                Location = new Point(50, 155),
                Size = new Size(300, 40),
                BackColor = ModernTheme.InputBackColor
            };
            
            txtUsername = new TextBox
            {
                Location = new Point(10, 10),
                Size = new Size(280, 20), 
                Font = ModernTheme.BodyFont,
                BackColor = ModernTheme.InputBackColor,
                ForeColor = ModernTheme.InputForeColor,
                BorderStyle = BorderStyle.None
            };
            pnlUsername.Controls.Add(txtUsername);

            // Password Section
            lblPassword = new Label
            {
                Text = "PASSWORD",
                Location = new Point(50, 210),
                Size = new Size(300, 20),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = ModernTheme.SubTextColor
            };

            pnlPassword = new Panel
            {
                Location = new Point(50, 235),
                Size = new Size(300, 40),
                BackColor = ModernTheme.InputBackColor
            };

            txtPassword = new TextBox
            {
                Location = new Point(10, 10),
                Size = new Size(280, 20),
                Font = ModernTheme.BodyFont,
                BackColor = ModernTheme.InputBackColor,
                ForeColor = ModernTheme.InputForeColor,
                BorderStyle = BorderStyle.None,
                PasswordChar = '●'
            };
            pnlPassword.Controls.Add(txtPassword);

            // Login Button
            btnLogin = new ModernButton
            {
                Text = "LOGIN",
                Location = new Point(50, 310),
                Size = new Size(300, 45),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                BackColor = ModernTheme.PrimaryColor,
                ForeColor = Color.White,
                BorderRadius = 20,
                Cursor = Cursors.Hand
            };
            btnLogin.Click += BtnLogin_Click;

            // Register Button
            btnRegister = new ModernButton
            {
                Text = "Create Account",
                Location = new Point(50, 370),
                Size = new Size(300, 45),
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                BackColor = ModernTheme.SurfaceColor,
                ForeColor = ModernTheme.SubTextColor,
                BorderRadius = 20,
                Cursor = Cursors.Hand
            };
            btnRegister.MouseEnter += (s, e) => btnRegister.ForeColor = ModernTheme.PrimaryColor;
            btnRegister.MouseLeave += (s, e) => btnRegister.ForeColor = ModernTheme.SubTextColor;
            btnRegister.Click += BtnRegister_Click;

            // Add controls to panel
            panelMain.Controls.Add(lblTitle);
            panelMain.Controls.Add(lblComplexity);
            panelMain.Controls.Add(lblUsername);
            panelMain.Controls.Add(pnlUsername);
            panelMain.Controls.Add(lblPassword);
            panelMain.Controls.Add(pnlPassword);
            panelMain.Controls.Add(btnLogin);
            panelMain.Controls.Add(btnRegister);

            // Add panel to form
            this.Controls.Add(panelMain);

            // Set Enter key to login
            this.AcceptButton = btnLogin;
        }

        private void ApplyModernEvents()
        {
            // Hover effects for inputs
            pnlUsername.Paint += (s, e) => DrawBorder(e.Graphics, pnlUsername.ClientRectangle, txtUsername.Focused ? ModernTheme.PrimaryColor : Color.Transparent);
            txtUsername.Enter += (s, e) => pnlUsername.Invalidate();
            txtUsername.Leave += (s, e) => pnlUsername.Invalidate();

            pnlPassword.Paint += (s, e) => DrawBorder(e.Graphics, pnlPassword.ClientRectangle, txtPassword.Focused ? ModernTheme.PrimaryColor : Color.Transparent);
            txtPassword.Enter += (s, e) => pnlPassword.Invalidate();
            txtPassword.Leave += (s, e) => pnlPassword.Invalidate();
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

        /// <summary>
        /// Login button click - Demonstrates O(1) hash table lookup
        /// KEY POINT FOR EXAM: This is where O(1) authentication happens!
        /// </summary>
        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.",
                    "Validation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // Show loading
            btnLogin.Enabled = false;
            btnLogin.Text = "Authenticating...";
            Application.DoEvents();

            // CRITICAL: O(1) authentication using hash table!
            // No loop through all users - direct hash lookup!
            var admin = _authService.Login(username, password);

            if (admin != null)
            {
                // Show success message
                MessageBox.Show(
                    $"Welcome, {admin.FullName}!",
                    "Login Successful",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // Open main dashboard
                this.Hide();
                var dashboard = new MainDashboardForm(_authService, admin);
                dashboard.FormClosed += (s, args) => this.Close();
                dashboard.Show();
            }
            else
            {
                MessageBox.Show(
                    "Invalid username or password.\n\n" +
                    "Default credentials:\n" +
                    "Username: admin\n" +
                    "Password: admin123",
                    "Login Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                btnLogin.Enabled = true;
                btnLogin.Text = "LOGIN";
                txtPassword.Clear();
                txtPassword.Focus();
            }
        }

        /// <summary>
        /// Register button - Open registration form
        /// </summary>
        private void BtnRegister_Click(object sender, EventArgs e)
        {
            var registerForm = new RegisterForm(_authService);
            if (registerForm.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show(
                    "Registration successful!\n" +
                    "You can now login with your new credentials.",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }
    }
}

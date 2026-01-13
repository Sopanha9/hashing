using System;
using System.Drawing;
using System.Windows.Forms;
using StudentManagementSystem.Models;
using StudentManagementSystem.Services;
using StudentManagementSystem.Helpers;
using StudentManagementSystem.Forms.Controls;

namespace StudentManagementSystem.Forms
{
    public partial class MainDashboardForm : Form
    {
        private readonly AuthenticationService _authService;
        private readonly SystemAdmin _currentAdmin;
        private Label lblWelcome;
        private ModernButton btnManageStudents;
        private ModernButton btnManageTeachers;
        private ModernButton btnManageCourses;
        private ModernButton btnViewReports;
        private ModernButton btnLogout;
        private Panel panelInfo;
        
        // Custom Title Bar
        private Panel pnlTitleBar;
        private Button btnClose;
        private Button btnMinimize;
        
        // Dragging
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        public MainDashboardForm(AuthenticationService authService, SystemAdmin admin)
        {
            _authService = authService;
            _currentAdmin = admin;
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            // Form settings
            this.Text = "Student Management System - Dashboard";
            this.Size = new Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = ModernTheme.BackColor;

            // Custom Title Bar
            pnlTitleBar = new Panel
            {
                Dock = DockStyle.Top,
                Height = 30,
                BackColor = ModernTheme.SurfaceColor
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
            btnClose.Click += (s, e) => this.Close();

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

            // Welcome Label
            lblWelcome = new Label
            {
                Text = $"Welcome, {_currentAdmin.FullName}!",
                Location = new Point(50, 50),
                Size = new Size(700, 40),
                Font = ModernTheme.TitleFont,
                ForeColor = ModernTheme.TextColor
            };

            // Info Panel
            panelInfo = new Panel
            {
                Location = new Point(50, 100),
                Size = new Size(900, 100),
                BackColor = ModernTheme.SurfaceColor,
            };

            var lblInfoTitle = new Label
            {
                Text = "Student Management System",
                Location = new Point(20, 20),
                Size = new Size(860, 30),
                Font = ModernTheme.HeaderFont,
                ForeColor = ModernTheme.PrimaryColor
            };

            var lblComplexity = new Label
            {
                Text = "Manage students, courses, and academic records efficiently using advanced Data Structures.",
                Location = new Point(20, 55),
                Size = new Size(860, 25),
                Font = ModernTheme.BodyFont,
                ForeColor = ModernTheme.SubTextColor
            };

            panelInfo.Controls.Add(lblInfoTitle);
            panelInfo.Controls.Add(lblComplexity);

            // Manage Students Button
            btnManageStudents = CreateDashboardButton("Manage Students", ModernTheme.PrimaryColor, 50, 230);
            btnManageStudents.Click += BtnManageStudents_Click;

            // Manage Teachers Button
            btnManageTeachers = CreateDashboardButton("Manage Teachers", ModernTheme.SecondaryColor, 280, 230);
            btnManageTeachers.Click += BtnManageTeachers_Click;

            // Manage Courses Button
            btnManageCourses = CreateDashboardButton("Manage Courses", Color.FromArgb(155, 89, 182), 510, 230);
            btnManageCourses.Click += BtnManageCourses_Click;

            // View Reports Button
            btnViewReports = CreateDashboardButton("View Reports", Color.FromArgb(230, 126, 34), 740, 230);
            btnViewReports.Click += BtnViewReports_Click;

            // Logout Button
            btnLogout = new ModernButton
            {
                Text = "Logout",
                Location = new Point(850, 630),
                Size = new Size(100, 35),
                Font = new Font("Segoe UI", 10),
                BackColor = ModernTheme.ErrorColor,
                ForeColor = Color.White,
                BorderRadius = 10,
                Cursor = Cursors.Hand
            };
            btnLogout.Click += (s, e) => this.Close();

            // Add all controls
            this.Controls.AddRange(new Control[]
            {
                lblWelcome, panelInfo, btnManageStudents, btnManageTeachers, 
                btnManageCourses, btnViewReports, btnLogout
            });
        }

        private ModernButton CreateDashboardButton(string text, Color color, int x, int y)
        {
            return new ModernButton
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(200, 150),
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                BackColor = color,
                ForeColor = Color.White,
                BorderRadius = 20,
                Cursor = Cursors.Hand
            };
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

        private void BtnManageStudents_Click(object sender, EventArgs e)
        {
            var studentForm = new StudentManagementForm();
            studentForm.Show();
        }

        private void BtnManageTeachers_Click(object sender, EventArgs e)
        {
            ToastForm.Show("Teacher Management module coming soon!", ToastForm.ToastType.Info);
        }

        private void BtnManageCourses_Click(object sender, EventArgs e)
        {
            ToastForm.Show("Course Management module coming soon!", ToastForm.ToastType.Info);
        }

        private void BtnViewReports_Click(object sender, EventArgs e)
        {
            ToastForm.Show("Reports module coming soon!", ToastForm.ToastType.Info);
        }
    }
}

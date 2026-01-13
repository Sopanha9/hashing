using System;
using System.Windows.Forms;
using StudentManagementSystem.Forms;
using StudentManagementSystem.Services;
using StudentManagementSystem.Models;
using System.Collections.Generic;

namespace StudentManagementSystem
{
    static class Program
    {
        /// <summary>
        /// Main entry point for the Windows Forms application
        /// Demonstrates Custom Hash Table implementation for O(1) operations
        /// Built for Data Structures Final Exam
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Enable visual styles for modern UI
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Initialize services
            // Create AuthenticationService with hash table for O(1) login
            var authService = new AuthenticationService();

            // Load sample admin data
            LoadSampleAdmins(authService);

            // Show login form
            Application.Run(new LoginForm(authService));
        }

        /// <summary>
        /// Load sample admin accounts for testing
        /// This demonstrates loading data into hash table for O(1) access
        /// </summary>
        private static void LoadSampleAdmins(AuthenticationService authService)
        {
            // Register default admin accounts
            // Note: In production, these would come from a database
            authService.Register("System Administrator", "admin", "admin123");
            authService.Register("John Doe", "john", "admin");
            authService.Register("Jane Smith", "jane", "1234");

            // Show startup message
            MessageBox.Show(
                "Welcome to Student Management System!\n\n" +
                "Default Login:\n" +
                "Username: admin\n" +
                "Password: admin123",
                "Student Management System",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
    }
}

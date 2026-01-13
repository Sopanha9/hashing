using System;

namespace StudentManagementSystem.Models
{
    public class SystemAdmin
    {
        public int AdminID { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

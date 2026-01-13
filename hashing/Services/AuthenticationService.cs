using System;
using System.Security.Cryptography;
using System.Text;
using StudentManagementSystem.DataStructures;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Services
{
    // Authentication Service using Custom Hash Table
    public class AuthenticationService
    {
        // Hash Table to store users by Username as key
        private readonly MyHashTable<string, SystemAdmin> _adminHashTable;

        public AuthenticationService()
        {
            _adminHashTable = new MyHashTable<string, SystemAdmin>(32);
        }

        // Register a new admin user
        public bool Register(string fullName, string username, string password)
        {
            // Check if username already exists
            if (_adminHashTable.ContainsKey(username))
            {
                return false; // Username already taken
            }

            // Hash the password using SHA-256
            string hashedPassword = HashPassword(password);

            // Create new admin
            var newAdmin = new SystemAdmin
            {
                FullName = fullName,
                Username = username,
                PasswordHash = hashedPassword,
                CreatedAt = DateTime.Now
            };

            // Insert into hash table
            _adminHashTable.Insert(username, newAdmin);

            return true;
        }

        // Login authentication
        public SystemAdmin Login(string username, string password)
        {
            try
            {
                // Search for user in hash table
                SystemAdmin admin = _adminHashTable.Search(username);

                // Verify password
                string hashedPassword = HashPassword(password);
                if (admin.PasswordHash == hashedPassword)
                {
                    return admin; // Login successful
                }

                return null; // Invalid password
            }
            catch (Exception)
            {
                return null; // User not found
            }
        }

        // Load existing admins from database into hash table
        public void LoadAdminsIntoHashTable(List<SystemAdmin> admins)
        {
            foreach (var admin in admins)
            {
                // Insert each admin into hash table
                _adminHashTable.Insert(admin.Username, admin);
            }
        }

        // Get admin by username
        public SystemAdmin GetAdminByUsername(string username)
        {
            try
            {
                return _adminHashTable.Search(username);
            }
            catch
            {
                return null;
            }
        }

        // Check if username exists
        public bool UsernameExists(string username)
        {
            return _adminHashTable.ContainsKey(username);
        }

        // Get all admins
        public List<SystemAdmin> GetAllAdmins()
        {
            return _adminHashTable.GetAllValues();
        }

        // Update admin information
        public bool UpdateAdmin(SystemAdmin admin)
        {
            try
            {
                _adminHashTable.Insert(admin.Username, admin); // Insert updates if key exists
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Delete admin
        public bool DeleteAdmin(string username)
        {
            return _adminHashTable.Delete(username);
        }

        // Hash password using SHA-256
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        // Get hash table statistics
        public string GetHashTableStats()
        {
            return $"Total Admins in Hash Table: {_adminHashTable.Size}\n" +
                   $"Lookup Time Complexity: O(1)\n" +
                   $"Compared to List Search: O(n)\n" +
                   $"Performance Gain: {_adminHashTable.Size}x faster for worst case!";
        }
    }
}

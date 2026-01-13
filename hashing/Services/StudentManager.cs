using System;
using System.Collections.Generic;
using System.Linq;
using StudentManagementSystem.DataStructures;
using StudentManagementSystem.Models;
using StudentManagementSystem.Data;

namespace StudentManagementSystem.Services
{
    // Student Manager using Custom Hash Table AND SQL Database
    public class StudentManager
    {
        // Hash Table for O(1) lookup
        private readonly MyHashTable<int, Student> _studentHashTable;
        
        // Database Context for persistence
        private readonly ApplicationDbContext _context;

        public StudentManager()
        {
            _studentHashTable = new MyHashTable<int, Student>(100);
            _context = new ApplicationDbContext();
        }

        // Loads all students from SQL Database into the Hash Table
        public void LoadFromDatabase()
        {
            try
            {
                // Fetch from DB
                var studentsFromDb = _context.Students.ToList();
                
                // Clear existing hash table
                _studentHashTable.Clear();

                // Populate Hash Table
                foreach (var student in studentsFromDb)
                {
                    _studentHashTable.Insert(student.StudentID, student);
                }
            }
            catch (Exception ex)
            {
                // Log and re-throw so the UI knows something went wrong
                Console.WriteLine($"Error connecting to database: {ex.Message}");
                throw new Exception($"Database Connection Failed: {ex.Message}", ex);
            }
        }

        // Add a new student to both Database and Hash Table
        public bool AddStudent(Student student)
        {
            try
            {
                // 1. Validation
                if (_studentHashTable.ContainsKey(student.StudentID))
                {
                    return false; // ID already exists locally
                }

                // 2. Add to Database
                _context.Students.Add(student);
                _context.SaveChanges(); // Persist to SQL

                // 3. Add to Hash Table
                _studentHashTable.Insert(student.StudentID, student);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Update student in both Database and Hash Table
        public bool UpdateStudent(Student student)
        {
            try
            {
                // 1. Check existence
                if (!_studentHashTable.ContainsKey(student.StudentID))
                {
                    return false;
                }

                // 2. Update Database
                var dbStudent = _context.Students.Find(student.StudentID);
                if (dbStudent != null)
                {
                    // Update properties manually to ensure tracking
                    dbStudent.StudentName = student.StudentName;
                    dbStudent.Gender = student.Gender;
                    dbStudent.DateOfBirth = student.DateOfBirth;
                    dbStudent.Phone = student.Phone;
                    dbStudent.Course = student.Course;
                    
                    _context.SaveChanges(); // Persist
                }

                // 3. Update Hash Table
                _studentHashTable.Insert(student.StudentID, student);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Delete student from both Database and Hash Table
        public bool DeleteStudent(int studentId)
        {
            try
            {
                // 1. Remove from Database
                var dbStudent = _context.Students.Find(studentId);
                if (dbStudent != null)
                {
                    _context.Students.Remove(dbStudent);
                    _context.SaveChanges();
                }

                // 2. Remove from Hash Table
                return _studentHashTable.Delete(studentId);
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Find student by ID
        public Student FindStudentById(int studentId)
        {
            try
            {
                return _studentHashTable.Search(studentId);
            }
            catch
            {
                return null;
            }
        }

        // Get all students from the Hash Table
        public List<Student> GetAllStudents()
        {
            return _studentHashTable.GetAllValues();
        }

        // Check if student exists
        public bool StudentExists(int studentId)
        {
            return _studentHashTable.ContainsKey(studentId);
        }

        public List<Student> SearchStudentsByName(string name)
        {
            var results = new List<Student>();
            var allStudents = _studentHashTable.GetAllValues();

            foreach (var student in allStudents)
            {
                if (student.StudentName.Contains(name, StringComparison.OrdinalIgnoreCase))
                {
                    results.Add(student);
                }
            }
            return results;
        }

        public int GetStudentCount()
        {
            return _studentHashTable.Size;
        }

        public void Clear()
        {
            _studentHashTable.Clear();
            // Note: We typically don't clear the whole DB here unless requested
        }
    }
}

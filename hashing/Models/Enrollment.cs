using System;

namespace StudentManagementSystem.Models
{
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int StudentID { get; set; }
        public int ClassID { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public decimal? Grade { get; set; }

        // Navigation properties
        public Student Student { get; set; }
        public Class Class { get; set; }
    }
}

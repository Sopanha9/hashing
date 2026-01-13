namespace StudentManagementSystem.Models
{
    public class Class
    {
        public int ClassID { get; set; }
        public int CourseID { get; set; }
        public int TeacherID { get; set; }
        public string Semester { get; set; }
        public string RoomNumber { get; set; }

        // Navigation properties
        public Course Course { get; set; }
        public Teacher Teacher { get; set; }
    }
}

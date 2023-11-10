using System.ComponentModel.DataAnnotations;

namespace MoodleExtensionAPI.Models
{
    public class Student
    {
        [Key]
        public int ID { get; set; }
        public string Neptuncode { get; set; }
        public int MoodleID { get; set; }
        public string? Training { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public List<Test> Tests { get; } = new();
        public List<TakenCourse> TakenCourses { get; } = new();
    }
}

using System.ComponentModel.DataAnnotations;

namespace MoodleExtensionAPI.Models
{
    public class TakenCourse
    {
        [Key]
        public int TakenCourseID { get; set; }

        public ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
        public Subject? Subject { get; set; }
        public ICollection<Semester> Semesters { get; set; } = new List<Semester>();
        public ICollection<Student> Students { get; set; } = new List<Student>();
        public string? SignatureState { get; set; }
        public int? Grade { get; set; }
        public int? OfferedGrade { get; set; }

    }
}

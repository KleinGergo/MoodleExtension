using System.ComponentModel.DataAnnotations;

namespace MoodleExtensionAPI.Models
{
    public class TakenCourse
    {
        [Key]
        public int TakenCourseID { get; set; }

        public Subject? Subject { get; set; }
        public Student? Student { get; set; }
        public string? SignatureState { get; set; }
        public string? Grade { get; set; }
        public string? OfferedGrade { get; set; }

        public TakenCourse(Subject Subject)
        {
            this.Subject = Subject;
        }
        public TakenCourse()
        {

        }

    }
}

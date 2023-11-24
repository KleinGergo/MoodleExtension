using System.ComponentModel.DataAnnotations;

namespace MoodleExtensionAPI.Models
{
    public class Semester
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }
        public int Year { get; set; }
        public List<TakenCourse> TakenCourses { get; } = new();

    }
}

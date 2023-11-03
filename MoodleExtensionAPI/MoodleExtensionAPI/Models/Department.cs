using System.ComponentModel.DataAnnotations;

namespace MoodleExtensionAPI.Models
{
    public class Department
    {
        [Key]
        public int ID { get; set; }
        public string? Name { get; set; }
        public ICollection<Teacher> Teachers { get; } = new List<Teacher>();
        public ICollection<Training> Trainings { get; } = new List<Training>();
    }
}

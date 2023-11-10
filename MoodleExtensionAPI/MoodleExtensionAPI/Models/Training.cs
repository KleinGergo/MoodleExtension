using System.ComponentModel.DataAnnotations;
namespace MoodleExtensionAPI.Models
{
    public class Training
    {
        [Key]
        public int ID { get; set; }
        public string? Name { get; set; }
        //public Department Department { get; set; }
    }
}

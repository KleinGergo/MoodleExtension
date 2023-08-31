using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoodleExtensionAPI.Models
{
    public class Teacher
    {
        [Key]
        public int ID { get; set; }
        public string? Name { get; set; }
        // Private field mapped using a public property
        [NotMapped]
        private string Password { get; set; }

        [Column("Password")] // Map to the "Password" column in the database
        public string PasswordDb
        {
            get { return Password; }
            set { Password = value; }
        }
        public string? Email { get; set; }
        public ICollection<Department> DepartmentID { get; }
        public ICollection<TakenCourse> TakenCourses { get; }
    }
}

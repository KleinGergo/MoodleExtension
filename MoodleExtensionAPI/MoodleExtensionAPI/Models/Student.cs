using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodleExtensionAPI.Models
{
    public class Student
    {
        [Key]
        public string Neptuncode { get; set; }
        public string? Training { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public ICollection<Test> Tests { get; set; }
        public ICollection<TakenCourse> TakenCourses { get; set; }

    }
}

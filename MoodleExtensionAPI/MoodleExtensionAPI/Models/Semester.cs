using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoodleExtensionAPI.Models
{
    public class Semester
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }
        public int Year { get; set; }
        public ICollection<TakenCourse> TakenCourses { get; }
    }
}

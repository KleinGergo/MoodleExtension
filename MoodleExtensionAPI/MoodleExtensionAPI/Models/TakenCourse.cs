using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoodleExtensionAPI.Models
{
    public class TakenCourse
    {
        [Key]
        public int TakenCourseID { get; set; }
        public int TeacherID { get; set; }
        public Teacher Teacher { get; set; }
        public string SubjectID { get; set; }
        public Subject Subject { get; set; }
        public int SemesterID { get; set; }
        public Semester Semester { get; set; }
        public string Neptuncode { get; set; }
        public Student Student { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoodleExtensionAPI.Models
{
    public class Department
    {
        [Key]
        public int ID { get; set; }
        public string? Name { get; set; }

        public int TrainingID { get; set; }
        public Training Training { get; set;}
        public int TeacherID { get; set; }
        public Teacher Teacher { get; set;}
    }
}

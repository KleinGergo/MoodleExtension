﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace MoodleExtensionAPI.Models
{
    public class Training
    {
        [Key]
        public int ID { get; set; }
        public string? Name { get; set; }
        public ICollection<Department> DepartmentID { get; set; }
    }
}

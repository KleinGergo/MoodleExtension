using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoodleExtensionAPI.Models
{
    public class Test
    {
        public Test() { }
        public Test(int TestID, double Result, DateTime StartDate, DateTime CompletionDate, int TimeSpent, int TimeLimit, bool IsCompleted, string Type) { 
            this.TestID = TestID;
            this.Result = Result;
            this.StartDate = StartDate;
            this.CompletionDate = CompletionDate;
            this.TimeSpent = TimeSpent;
            this.TimeLimit  = TimeLimit;
            this.IsCompleted = IsCompleted;
            this.Type = Type;
            this.SubjectID = new List<Subject>();
        }
        [Key]
        public int TestID { get; set; }

        public ICollection<Subject> SubjectID { get; set; }
        public double Result { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime CompletionDate { get; set; } //in seconds
        public int TimeSpent { get; set; } //in seconds
        public int TimeLimit { get; set; } //in seconds
        public bool  IsCompleted { get; set; } 
        public string Type { get; set; }
        public string? Label { get; set; }
        public int? PreviousTestID { get; set; }
        public string Neptuncode { get; set; }
        public Student Student { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace MoodleExtensionAPI.Models
{
    public class Test
    {
        public Test() { }
        public Test(int TestID, Subject Subject, double Result, DateTime StartDate, DateTime CompletionDate, int TimeSpent, int TimeLimit, bool IsCompleted, string Type)
        {
            this.TestID = TestID;
            this.Result = Result;
            this.StartDate = StartDate;
            this.CompletionDate = CompletionDate;
            this.TimeSpent = TimeSpent;
            this.TimeLimit = TimeLimit;
            this.IsCompleted = IsCompleted;
            this.Type = Type;
            this.Subject = Subject;
        }
        [Key]
        public int TestID { get; set; }

        public Subject Subject { get; set; }
        public double Result { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime CompletionDate { get; set; }
        public int TimeSpent { get; set; } //in seconds
        public int TimeLimit { get; set; } //in seconds
        public bool IsCompleted { get; set; }
        public string Type { get; set; }
        public string? Label { get; set; }
        public double GradeMax { get; set; }
        public double GradeMin { get; set; }
        public int? PreviousTestID { get; set; }
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}

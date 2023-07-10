using Microsoft.VisualBasic;

namespace MoodleExtensionAPI.Models
{
    public class Test
    {
        public Test() { }
        public Test(int TestID, string SubjectID, double Result, DateTime StartDate, DateTime CompletionDate, int TimeSpent, int TimeLimit, bool IsCompleted, string Type) { 
            this.TestID = TestID;
            this.SubjectID = SubjectID;
            this.Result = Result;
            this.StartDate = StartDate;
            this.CompletionDate = CompletionDate;
            this.TimeSpent = TimeSpent;
            this.TimeLimit  = TimeLimit;
            this.IsCompleted = IsCompleted;
            this.Type = Type;
        }

        public int TestID { get; set; }
        public string SubjectID { get; set; }
        public double Result { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime CompletionDate { get; set; } //in seconds
        public int TimeSpent { get; set; } //in seconds
        public int TimeLimit { get; set; } //in seconds
        public bool IsCompleted { get; set; } 
        public string Type { get; set; }
        public string? Label { get; set; }
        public int? PreviousTestID { get; set; }
    }
}

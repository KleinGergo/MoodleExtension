using System.ComponentModel.DataAnnotations;

namespace MoodleExtensionAPI.Models
{
    public class Test
    {
        public Test() { }
        public Test(int TestID, Subject Subject, double Result, bool IsCompleted, string Type, Student Student)
        {
            this.TestID = TestID;
            this.Result = Result;
            this.IsCompleted = IsCompleted;
            this.Type = Type;
            this.Subject = Subject;
            this.Student = Student;
        }
        [Key]
        public int TestID { get; set; }
        public int MoodleTestID { get; set; }
        public Subject Subject { get; set; }
        public double? Result { get; set; }
        public bool IsCompleted { get; set; }
        public string Type { get; set; }
        public string? Label { get; set; }
        public double? GradeMax { get; set; }
        public double? GradeMin { get; set; }
        public int? PreviousTestID { get; set; }
        public Student Student { get; set; }
    }
}

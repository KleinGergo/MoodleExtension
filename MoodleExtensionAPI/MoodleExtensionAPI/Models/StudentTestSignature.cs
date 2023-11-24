namespace MoodleExtensionAPI.Models
{
    public class StudentTestSignature
    {
        public Student Student { get; set; }
        public List<Test> tests { get; set; }
        public string SignatureStatus { get; set; }
        public string Grade { get; set; }
        public string OfferedGrade { get; set; }

        public StudentTestSignature(Student Student, List<Test> tests, string SignatureStatus, string offeredGrade)
        {
            this.Student = Student;
            this.tests = tests;
            this.SignatureStatus = SignatureStatus;
            OfferedGrade = offeredGrade;
        }
        public StudentTestSignature() { }
    }
}

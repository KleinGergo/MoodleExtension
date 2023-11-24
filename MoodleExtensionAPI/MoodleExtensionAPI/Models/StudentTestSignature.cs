namespace MoodleExtensionAPI.Models
{
    public class StudentTestSignature
    {
        public Student Student { get; set; }
        public List<Test> tests { get; set; }
        public string SignatureStatus { get; set; }
        public string Grade { get; set; }

        public StudentTestSignature(Student Student, List<Test> tests, string SignatureStatus)
        {
            this.Student = Student;
            this.tests = tests;
            this.SignatureStatus = SignatureStatus;
        }
        public StudentTestSignature() { }
    }
}

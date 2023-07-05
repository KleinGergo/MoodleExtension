namespace MoodleExtensionAPI.Models
{
    public class Subject
    {
        public Subject() { }
        public Subject(string SubjectID, string DepartmentID, string SubjectName, int numberOfTests, string SignatureState, SignatureCondition signatureCondition, int Grade, int OfferedGrade) {
            this.SubjectID = SubjectID;
            this.DepartmentID = DepartmentID;
            this.SubjectName = SubjectName;
            this.numberOfTests = numberOfTests;
            this.SignatureState = SignatureState;
            this.SignatureCondition = SignatureCondition;
            this.Grade = Grade;
            this.OfferedGrade = OfferedGrade;
        }
       
        public string SubjectID { get; set; }
        public string DepartmentID { get; set; }
        public string SubjectName { get; set; }
        public int numberOfTests { get; set; }
        public string SignatureState { get; set; }
        public SignatureCondition SignatureCondition { get; set; }
        public int Grade { get; set; }
        public int OfferedGrade { get; set; }

    }
}

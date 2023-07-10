using System.Reflection.Metadata.Ecma335;

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
        public bool IsSignatureApproved(List<Test> tests, SignatureCondition signatureCondition)
        {
            int requiredAssigments = 0;
            int completedAssigments = 0;

            foreach (var condition in signatureCondition.conditions)
            {
                switch (condition.type)
                {
                    case "assigment":
                        if (!tests.Any(t => t.IsCompleted && t.Type == "assigment"))
                            return false;
                        break;

                    case "multipleAssigment":
                        requiredAssigments = condition.numberOfAssigments;
                        completedAssigments = tests.Count(t => t.IsCompleted && t.Type == "assigment");
                        if (completedAssigments < requiredAssigments)
                            return false;
                        break;

                    case "testPercentage":
                        double totalPercentage = tests.Sum(t => t.IsCompleted ? t.Result : 0.0);
                        double averagePercentage = totalPercentage / tests.Count;
                        if (averagePercentage < condition.minimumPercentage)
                            return false;
                        break;

                    case "allTestsWritten":
                        if (condition.required && !tests.All(t => t.IsCompleted))
                            return false;
                        break;

                    case "smallTestsWritten":
                        if (!tests.Any(t => t.IsCompleted && t.Type == "smallTestsWritten"))
                            return false;
                        break;

                    case "individualTestPercentage":
                        foreach (var test in tests)
                        {
                            if (test.IsCompleted && test.Result < condition.minimumPercentage)
                                return false;
                        }
                        break;

                    default:
                        return false; // Unknown condition type
                }
            }

            return true;
        }


    }

}

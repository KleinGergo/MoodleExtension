using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace MoodleExtensionAPI.Models
{
    public class Subject
    {
        private string signatureCondition;

        public Subject() { }

        public Subject(int SubjectID, Department Department, string SubjectName, string SignatureCondition)
        {
            this.SubjectID = SubjectID;
            this.Department = Department;
            this.SubjectName = SubjectName;
            signatureCondition = SignatureCondition;
        }

        [Key]
        public int SubjectID { get; set; }
        public int SubjectMoodleID { get; set; }
        public string? SubjectName { get; set; }
        public string? SignatureCondition { get; set; }
        public ICollection<Test>? Tests { get; set; } = new List<Test>();
        public Department? Department { get; set; }


        public SignatureCondition GetSignatureCondition()
        {
            SignatureCondition sign = JsonSerializer.Deserialize<SignatureCondition>(this.signatureCondition);
            return sign;
        }
        /*public bool IsSignatureApproved(List<Test> tests, SignatureCondition signatureCondition)
        {
            int? requiredAssigments = 0;
            int? completedAssigments = 0;

            foreach (var condition in signatureCondition.Conditions)
            {
                switch (condition.Type)
                {
                    case "assigment":
                        if (!tests.Any(t => t.IsCompleted && t.Type == "assigment"))
                            return false;
                        break;

                    case "multipleAssigment":
                        requiredAssigments = condition.NumberOfAssigments;
                        completedAssigments = tests.Count(t => t.IsCompleted && t.Type == "assigment");
                        if (completedAssigments < requiredAssigments)
                            return false;
                        break;

                    case "testPercentage":
                        double totalPercentage = tests.Sum(t => t.IsCompleted ? t.Result : 0.0);
                        double averagePercentage = totalPercentage / tests.Count;
                        if (averagePercentage < condition.MinimumPercentage)
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
        }*/


    }

}

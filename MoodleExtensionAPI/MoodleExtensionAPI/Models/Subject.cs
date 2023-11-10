using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace MoodleExtensionAPI.Models
{
    public class Subject
    {
        private string signatureCondition;

        public Subject() { }

        public Subject(int SubjectID, string SubjectName, string SignatureCondition)
        {
            this.SubjectID = SubjectID;
            //this.Department = Department;
            this.SubjectName = SubjectName;
            signatureCondition = SignatureCondition;
        }

        [Key]
        public int SubjectID { get; set; }
        public int SubjectMoodleID { get; set; }
        public string? SubjectName { get; set; }
        public string? SignatureCondition { get; set; }
        public List<Test>? Tests { get; } = new();
        //public Department? Department { get; set; }


        public SignatureCondition GetSignatureCondition()
        {
            SignatureCondition sign = JsonSerializer.Deserialize<SignatureCondition>(this.signatureCondition);
            return sign;
        }
        public bool IsSignatureApproved(List<Test> tests, SignatureCondition signatureCondition)
        {
            int? requiredAssigments = 0;
            int? completedAssigments = 0;

            foreach (var condition in signatureCondition.Conditions)
            {
                switch (condition.Type)
                {


                    case "multipleAssigment":
                        requiredAssigments = condition.NumberOfAssigments;
                        completedAssigments = tests.Count(t => t.IsCompleted && t.Type == "assigment" && t.Result > condition.RequiredIndividualAssigmentPercentage);
                        double? totalAssigmentPercentage = tests.Sum(t => t.IsCompleted && t.Type == "bigTest" ? t.Result : 0.0);
                        double? averageAssigmentPercentage = totalAssigmentPercentage / completedAssigments;
                        if (completedAssigments < requiredAssigments)
                            return false;
                        else if (averageAssigmentPercentage < condition.RequiredAvgAssigmentPercentage)
                            return false;
                        break;

                    case "bigTests":
                        int completedBigTests = tests.Count(t => t.IsCompleted && t.Type == "bigTests" && t.Result > condition.RequiredIndividualBigTestPercentage);
                        double? totalBigTestPercentage = tests.Sum(t => t.IsCompleted && t.Type == "bigTest" ? t.Result : 0.0);
                        double? averageBigTestPercentage = totalBigTestPercentage / completedBigTests;
                        if (averageBigTestPercentage < condition.RequiredAvgBigTestPercentage)
                            return false;
                        else if (completedBigTests < condition.RequiredNumberOfBigTests)
                            return false;
                        break;

                    case "smallTests":
                        int completedsmallTests = tests.Count(t => t.IsCompleted && t.Type == "smallTests" && t.Result > condition.RequiredIndividualSmallTestPercentage);
                        double? totalsmallTestsPercentage = tests.Sum(t => t.IsCompleted && t.Type == "smallTests" ? t.Result : 0.0);
                        double? averagesmallTestsPercentage = totalsmallTestsPercentage / completedsmallTests;
                        if (averagesmallTestsPercentage < condition.RequiredAvgBigTestPercentage)
                            return false;
                        else if (completedsmallTests < condition.RequiredNumberOfSmallTests)
                            return false;
                        break;

                    default:
                        return false; // Unknown condition type
                }
            }

            return true;
        }


    }




}



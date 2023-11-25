using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace MoodleExtensionAPI.Models
{
    public class Subject
    {


        public Subject() { }

        public Subject(int SubjectID, string SubjectName, string SignatureCondition)
        {
            this.SubjectID = SubjectID;
            this.SubjectName = SubjectName;
            this.SignatureCondition = SignatureCondition;
        }

        [Key]
        public int SubjectID { get; set; }
        public int SubjectMoodleID { get; set; }
        public string? SubjectName { get; set; }
        public string? SignatureCondition { get; set; }
        public string? OfferedGradeCondition { get; set; }
        public List<Test>? Tests { get; } = new();
        public List<Teacher>? Teachers { get; } = new();



        public OfferedGradeCondition GetOfferedGradeCondition()
        {
            try
            {
                OfferedGradeConditionWrapper condition = JsonSerializer.Deserialize<OfferedGradeConditionWrapper>(this.OfferedGradeCondition);
                return condition.GradeCondition;
            }
            catch (Exception ex)
            {
                // Log the exception for debugging
                Console.WriteLine($"Exception during deserialization: {ex.Message}");

                // Handle or rethrow the exception if appropriate for your case
                return null;
            }
        }
        public SignatureCondition GetSignatureCondition()
        {
            try
            {
                // Log the JSON string before deserialization
                Console.WriteLine($"JSON String from database: {this.SignatureCondition}");

                SignatureConditionWrapper sign = JsonSerializer.Deserialize<SignatureConditionWrapper>(this.SignatureCondition);

                // Log the deserialized object for debugging
                Console.WriteLine($"Deserialized SignatureCondition: {JsonSerializer.Serialize(sign)}");

                return sign.SignatureCondition;
            }
            catch (Exception ex)
            {
                // Log the exception for debugging
                Console.WriteLine($"Exception during deserialization: {ex.Message}");

                // Handle or rethrow the exception if appropriate for your case
                return null;
            }
        }

        // IsOfferedGradeApproved check if the list of tests met with the offered grade conditions.
        public bool IsOfferedGradeApproved(List<Test> tests, OfferedGradeCondition offeredGradeCondition)
        {
            if (offeredGradeCondition != null)
            {
                foreach (var condition in offeredGradeCondition.Conditions)
                {
                    switch (condition.Type)
                    {


                        case Constants.TypeMultipleAssigment:

                            // Count the number of completed assignments that meet certain criteria
                            int? completedAssigments = tests.Count(t => t.IsCompleted && t.Type == Constants.TypeMultipleAssigment && (t.Result / t.GradeMax) * 100 >= condition.RequiredIndividualAssigmentPercentage);
                            // Calculate the total result and max points for completed multiple assignments
                            double? totalAssigmentResult = tests.Sum(t => t.IsCompleted && t.Type == Constants.TypeMultipleAssigment ? t.Result : 0.0);
                            double? totalAssigmentMaxPoints = tests.Sum(t => t.IsCompleted && t.Type == Constants.TypeMultipleAssigment ? t.GradeMax : 0.0);
                            // Calculate the average assignment percentage
                            double? averageAssigmentPercentage = (totalAssigmentResult / totalAssigmentMaxPoints) * 100;
                            // Check if the completed assignments are less than the required number
                            if (completedAssigments < condition.RequiredNumberOfAssigments)
                                return false;
                            // Check if the average assignment percentage is less than the required percentage
                            else if (averageAssigmentPercentage < condition.RequiredAvgAssigmentPercentage)
                                return false;
                            // If both conditions are met, return true
                            break;

                        case Constants.TypeBigTests:
                            int completedBigTests = tests.Count(t => t.IsCompleted && t.Type == Constants.TypeBigTests && (t.Result / t.GradeMax) * 100 >= condition.RequiredIndividualBigTestPercentage);
                            double? totalBigTestPercentage = tests.Sum(t => t.IsCompleted && t.Type == Constants.TypeBigTests ? t.Result : 0.0);
                            double? totalBigTestMaxPoints = tests.Sum(t => t.IsCompleted && t.Type == Constants.TypeBigTests ? t.GradeMax : 0.0);
                            double? averageBigTestPercentage = (totalBigTestPercentage / totalBigTestMaxPoints) * 100;
                            if (averageBigTestPercentage < condition.RequiredAvgBigTestPercentage)
                                return false;
                            else if (completedBigTests < condition.RequiredNumberOfBigTests)
                                return false;
                            break;

                        case Constants.TypeSmallTests:
                            int completedSmallTests = tests.Count(t => t.IsCompleted && t.Type == Constants.TypeSmallTests && (t.Result / t.GradeMax) * 100 >= condition.RequiredIndividualSmallTestPercentage);
                            double? totalSmallTestsPercentage = tests.Sum(t => t.IsCompleted && t.Type == Constants.TypeSmallTests ? t.Result : 0.0);
                            double? totalSmallTestsMaxPoints = tests.Sum(t => t.IsCompleted && t.Type == Constants.TypeSmallTests ? t.GradeMax : 0.0);
                            double? averageSmallTestsPercentage = (totalSmallTestsPercentage / totalSmallTestsMaxPoints) * 100;
                            if (averageSmallTestsPercentage < condition.RequiredAvgBigTestPercentage)
                                return false;
                            else if (completedSmallTests < condition.RequiredNumberOfSmallTests)
                                return false;
                            break;


                    }

                }
                return true;
            }
            return false;
        }


        // IsSignatureApproved check if the list of tests, met with the signature condition.
        public bool IsSignatureApproved(List<Test> tests, SignatureCondition signatureCondition)
        {


            if (signatureCondition != null)
            {
                foreach (var condition in signatureCondition.Conditions)
                {
                    switch (condition.Type)
                    {


                        case "multipleAssigment":

                            // Count the number of completed assignments that meet certain criteria
                            int? completedAssigments = tests.Count(t => t.IsCompleted && t.Type == Constants.TypeMultipleAssigment && (t.Result / t.GradeMax) * 100 >= condition.RequiredIndividualAssigmentPercentage);
                            // Calculate the total result and max points for completed multiple assignments
                            double? totalAssigmentResult = tests.Sum(t => t.IsCompleted && t.Type == Constants.TypeMultipleAssigment ? t.Result : 0.0);
                            double? totalAssigmentMaxPoints = tests.Sum(t => t.IsCompleted && t.Type == Constants.TypeMultipleAssigment ? t.GradeMax : 0.0);
                            // Calculate the average assignment percentage
                            double? averageAssigmentPercentage = (totalAssigmentResult / totalAssigmentMaxPoints) * 100;
                            // Check if the completed assignments are less than the required number
                            if (completedAssigments < condition.RequiredNumberOfAssigments)
                                return false;
                            // Check if the average assignment percentage is less than the required percentage
                            else if (averageAssigmentPercentage < condition.RequiredAvgAssigmentPercentage)
                                return false;
                            // If both conditions are met, return true
                            break;

                        case "bigTests":
                            int completedBigTests = tests.Count(t => t.IsCompleted && t.Type == Constants.TypeBigTests && (t.Result / t.GradeMax) * 100 >= condition.RequiredIndividualBigTestPercentage);
                            double? totalBigTestPercentage = tests.Sum(t => t.IsCompleted && t.Type == Constants.TypeBigTests ? t.Result : 0.0);
                            double? totalBigTestMaxPoints = tests.Sum(t => t.IsCompleted && t.Type == Constants.TypeBigTests ? t.GradeMax : 0.0);
                            double? averageBigTestPercentage = (totalBigTestPercentage / totalBigTestMaxPoints) * 100;
                            if (averageBigTestPercentage < condition.RequiredAvgBigTestPercentage)
                                return false;
                            else if (completedBigTests < condition.RequiredNumberOfBigTests)
                                return false;
                            break;

                        case "smallTests":
                            int completedSmallTests = tests.Count(t => t.IsCompleted && t.Type == Constants.TypeSmallTests && (t.Result / t.GradeMax) * 100 >= condition.RequiredIndividualSmallTestPercentage);
                            double? totalSmallTestsPercentage = tests.Sum(t => t.IsCompleted && t.Type == Constants.TypeSmallTests ? t.Result : 0.0);
                            double? totalSmallTestsMaxPoints = tests.Sum(t => t.IsCompleted && t.Type == Constants.TypeSmallTests ? t.GradeMax : 0.0);
                            double? averageSmallTestsPercentage = (totalSmallTestsPercentage / totalSmallTestsMaxPoints) * 100;
                            if (averageSmallTestsPercentage < condition.RequiredAvgBigTestPercentage)
                                return false;
                            else if (completedSmallTests < condition.RequiredNumberOfSmallTests)
                                return false;
                            break;


                    }
                }
            }


            return true;
        }


    }




}



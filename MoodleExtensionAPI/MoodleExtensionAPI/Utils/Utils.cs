using MoodleExtensionAPI.Models;

namespace MoodleExtensionAPI.Utils
{
    public static class Utils
    {
        public static APICourseResponse GetCourseFromAPIResponse(List<APICourseResponse> list, string course)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] != null)
                {
                    if (list[i].displayname.Contains(course))
                    {
                        return list[i];
                    }
                }

            }
            return null;

        }
        public static List<StudentTestSignature> GetStudentTestSignatures(List<Test> tests)
        {
            List<StudentTestSignature> studentTestSignatures = new List<StudentTestSignature>();

            return studentTestSignatures;
        }
        public static StudentTestSignature GetSignatures(List<Test> tests, Student stud)
        {
            StudentTestSignature testResult = new StudentTestSignature();
            List<Test> studentTests = Utils.GetStudentTests(tests, stud);
            Subject subject = DatabaseUtils.GetSubjectByTests(tests);
            if (subject != null)
            {
                if (subject.GetSignatureCondition() != null)
                {
                    SignatureCondition signatureCondition = subject.GetSignatureCondition();
                    bool isSignature = subject.IsSignatureApproved(studentTests, signatureCondition);
                    if (isSignature)
                    {
                        testResult.SignatureStatus = "approved";

                    }
                    else
                    {
                        testResult.SignatureStatus = "denied";
                    }
                    testResult.Student = stud;
                    testResult.tests = studentTests;
                }

            }

            return testResult;
        }
        public static int CalculateForIndividualOfferedGrade(StudentTestSignature studentTestSignature, OfferedGradeCondition offeredGradeConditions, bool isOfferedGradeApproved)
        {
            foreach (var condition in offeredGradeConditions.Conditions)
            {
                switch (condition.Type)
                {
                    case Constants.TypeMultipleAssigment:
                        int countAssigment = studentTestSignature.tests.Count(t => t.Type == Constants.TypeMultipleAssigment && t.Result != null);
                        if (countAssigment < condition.RequiredNumberOfAssigments)
                        {
                            return 0;
                        }
                        break;
                    case Constants.TypeBigTests:
                        int countBigTests = studentTestSignature.tests.Count(t => t.Type == Constants.TypeBigTests && t.Result != null);
                        if (countBigTests < condition.RequiredNumberOfBigTests)
                        {
                            return 0;
                        }
                        break;
                    case Constants.TypeSmallTests:
                        int countSmallTests = studentTestSignature.tests.Count(t => t.Type == Constants.TypeSmallTests && t.Result != null);
                        if (countSmallTests < condition.RequiredNumberOfSmallTests)
                        {
                            return 0;
                        }
                        break;
                }
            }
            return GetIndividualOfferedGrade(offeredGradeConditions, studentTestSignature);
        }

        public static int GetIndividualOfferedGrade(OfferedGradeCondition offeredGradeConditions, StudentTestSignature studentTestSignature)
        {
            double? requiredIndividualAssigmentPercentage = 0;
            double? requiredIndividualBigTestPercentage = 0;
            double? requiredIndividualSmallTestPercentage = 0;
            double? requiredNumberOfAssigments = 0;
            double? requiredNumberOfSmallTests = 0;
            double? requiredNumberOfBigTests = 0;
            int? numberOfAssigments = 0;
            int? numberOfSmallTests = 0;
            int? numberOfBigTests = 0;
            GradeCondition gradingCondition = new GradeCondition();

            foreach (var condition in offeredGradeConditions.Conditions)
            {
                if (condition.Type == Constants.TypeMultipleAssigment)
                {
                    requiredIndividualAssigmentPercentage = condition.RequiredIndividualAssigmentPercentage;
                    requiredNumberOfAssigments = condition.RequiredNumberOfAssigments;
                    numberOfAssigments = condition.NumberOfAssigments;
                }
                if (condition.Type == Constants.TypeBigTests)
                {
                    requiredIndividualBigTestPercentage = condition.RequiredIndividualBigTestPercentage;
                    requiredNumberOfBigTests = condition.RequiredNumberOfBigTests;
                    numberOfBigTests = condition.NumberOfBigTests;
                }
                if (condition.Type == Constants.TypeSmallTests)
                {
                    requiredIndividualSmallTestPercentage = condition.RequiredIndividualSmallTestPercentage;
                    requiredNumberOfSmallTests = condition.RequiredNumberOfSmallTests;
                    numberOfSmallTests = condition.NumberOfSmallTests;
                }
                if (condition.Type == Constants.TypeOfferedGrade)
                {
                    gradingCondition = condition;
                }

            }
            int? numberOfAssigmentsWhichLower = null;
            int? numberOfBigTestsWhichLower = null;
            int? numberOfSmallTestsWhichLower = null;
            if (requiredIndividualAssigmentPercentage != 0)
            {
                numberOfAssigmentsWhichLower = studentTestSignature.tests.Count(t => t.Type == Constants.TypeMultipleAssigment && (t.Result / t.GradeMax) * 100 < requiredIndividualAssigmentPercentage);
            }
            if (requiredIndividualBigTestPercentage != 0)
            {
                numberOfBigTestsWhichLower = studentTestSignature.tests.Count(t => t.Type == Constants.TypeBigTests && (t.Result / t.GradeMax) * 100 < requiredIndividualBigTestPercentage);
            }
            if (requiredIndividualSmallTestPercentage != 0)
            {
                numberOfSmallTestsWhichLower = studentTestSignature.tests.Count(t => t.Type == Constants.TypeSmallTests && (t.Result / t.GradeMax) * 100 < requiredIndividualSmallTestPercentage);
            }

            if (numberOfAssigmentsWhichLower > numberOfAssigments - requiredNumberOfAssigments && numberOfAssigmentsWhichLower != null || numberOfBigTestsWhichLower > numberOfBigTests - requiredNumberOfBigTests && numberOfBigTestsWhichLower != null || numberOfSmallTestsWhichLower > numberOfSmallTests - requiredNumberOfSmallTests && numberOfSmallTestsWhichLower != null)
            {
                return 0;
            }
            double? lowestAssignmentPercentage = studentTestSignature.tests
                .Where(t => t.Type == Constants.TypeMultipleAssigment && t.Result != null) // Filter tests of a specific type
                .Min(t => (double)t.Result / t.GradeMax) * 100; // Calculate and find the minimum percentage
            double? lowestBigTestsPercentage = studentTestSignature.tests
               .Where(t => t.Type == Constants.TypeBigTests && t.Result != null) // Filter tests of a specific type
               .Min(t => (double)t.Result / t.GradeMax) * 100; // Calculate and find the minimum percentage
            double? lowestSmallTestPercentage = studentTestSignature.tests
               .Where(t => t.Type == Constants.TypeSmallTests && t.Result != null) // Filter tests of a specific type
               .Min(t => (double)t.Result / t.GradeMax) * 100; // Calculate and find the minimum percentage

            double? minValue = Math.Min(lowestAssignmentPercentage ?? double.MaxValue, Math.Min(lowestBigTestsPercentage ?? double.MaxValue, lowestSmallTestPercentage ?? double.MaxValue));
            if (minValue >= gradingCondition.OfferedGradeAPercentage && gradingCondition.OfferedGradeAPercentage != 0)
            {
                return 5;
            }
            if (minValue >= gradingCondition.OfferedGradeBPercentage && gradingCondition.OfferedGradeBPercentage != 0)
            {
                return 4;
            }
            if (minValue >= gradingCondition.OfferedGradeCPercentage && gradingCondition.OfferedGradeCPercentage != 0)
            {
                return 3;
            }
            if (minValue >= gradingCondition.OfferedGradeDPercentage && gradingCondition.OfferedGradeDPercentage != 0)
            {
                return 2;
            }
            return 0;
        }
        public static bool IsIndividualOfferedGradeCounts(OfferedGradeCondition conditions)
        {
            foreach (var condition in conditions.Conditions)
            {
                if (condition.RequiredIndividualAssigmentPercentage > 0 || condition.RequiredIndividualBigTestPercentage > 0 || condition.RequiredIndividualSmallTestPercentage > 0)
                    return true;
            }
            return false;
        }
        public static string CalculateOfferedGrade(StudentTestSignature studentTestSignature, OfferedGradeCondition offeredGradeConditions, bool isOfferedGradeApproved)
        {
            int offeredGrade = 0;
            if (!isOfferedGradeApproved)
            {
                return "Nincs megajánlott jegy";
            }
            if (IsIndividualOfferedGradeCounts(offeredGradeConditions))
            {
                offeredGrade = CalculateForIndividualOfferedGrade(studentTestSignature, offeredGradeConditions, isOfferedGradeApproved);
            }

            GradeCondition gradeConditions = new GradeCondition();
            if (offeredGradeConditions != null)
            {
                if (offeredGradeConditions.Conditions != null)
                {
                    foreach (var condition in offeredGradeConditions.Conditions)
                    {
                        if (condition.Type == Constants.TypeOfferedGrade)
                        {
                            gradeConditions = condition;
                        }
                    }

                    if (gradeConditions != null)
                    {
                        double? assigmentTestResults = 0;
                        double? assigmentMaxPoints = 0;

                        double? bigTestsResults = 0;
                        double? bigTestsMaxPoints = 0;

                        double? smallTestResults = 0;
                        double? smallTestMaxPoints = 0;
                        foreach (var test in studentTestSignature.tests)
                        {
                            if (test.Type == Constants.TypeMultipleAssigment)
                            {
                                assigmentMaxPoints += test.GradeMax;
                                if (test.Result != null)
                                    assigmentTestResults += test.Result;

                            }
                            else if (test.Type == Constants.TypeBigTests)
                            {
                                bigTestsMaxPoints += test.GradeMax;
                                if (test.Result != null)
                                    bigTestsResults += test.Result;
                            }
                            else if (test.Type == Constants.TypeSmallTests)
                            {
                                smallTestMaxPoints += test.GradeMax;
                                if (test.Result != null)
                                    smallTestResults += test.Result;
                            }
                        }
                        double? assigmentPercentage = 0;
                        double? bigTestPercentage = 0;
                        double? smallTestPercentage = 0;
                        if (assigmentTestResults != 0 && assigmentMaxPoints != 0)
                        {
                            assigmentPercentage = (assigmentTestResults / assigmentMaxPoints) * 100;
                        }
                        if (bigTestsResults != 0 && bigTestsMaxPoints != 0)
                        {
                            bigTestPercentage = (bigTestsResults / bigTestsMaxPoints) * 100;
                        }
                        if (smallTestResults != 0 && smallTestMaxPoints != 0)
                        {
                            smallTestPercentage = (smallTestResults / smallTestMaxPoints) * 100;
                        }
                        int? avgOfferedGrade = GetOfferedGrade(assigmentPercentage, bigTestPercentage, smallTestPercentage, gradeConditions);
                        return GetAppropiateOfferedGrade(offeredGrade, avgOfferedGrade);
                    }
                }
            }
            return "Nincs megajánlott jegy";

        }
        static string GetAppropiateOfferedGrade(int grade1, int? grade2)
        {
            if (grade1 < grade2)
            {
                switch (grade1)
                {
                    case 0:
                        return "Nincs megajánlott jegy";
                        break;
                    case 2:
                        return "Elégséges";
                        break;

                    case 3:
                        return "Közepes";
                        break;
                    case 4:
                        return "Jó";
                        break;
                    case 5:
                        return "Jeles";
                        break;
                }
            }
            else
            {
                switch (grade2)
                {
                    case 0:
                        return "Nincs megajánlott jegy";
                        break;
                    case 2:
                        return "Elégséges";
                        break;

                    case 3:
                        return "Közepes";
                        break;
                    case 4:
                        return "Jó";
                        break;
                    case 5:
                        return "Jeles";
                        break;
                }
            }
            return "";
        }
        public static string CalculateFinalGrade(StudentTestSignature studentTestSignature, SignatureCondition SignatureCondition)
        {
            if (studentTestSignature.SignatureStatus == "denied")
            {
                return "Elégtelen";
            }
            Condition gradeConditions = new Condition();
            double? assigmentWeight = 0;
            double? bigTestWeight = 0;
            double? smallTestWeight = 0;
            if (SignatureCondition != null)
            {
                if (SignatureCondition.Conditions != null)
                {
                    foreach (var condition in SignatureCondition.Conditions)
                    {
                        if (condition.Type == Constants.TypeGrading)
                        {
                            gradeConditions = condition;
                        }
                        if (condition.Type == Constants.TypeMultipleAssigment)
                            assigmentWeight = condition.Weight;
                        if (condition.Type == Constants.TypeBigTests)
                            bigTestWeight = condition.Weight;
                        if (condition.Type == Constants.TypeSmallTests)
                            smallTestWeight = condition.Weight;

                    }
                    if (gradeConditions != null)
                    {
                        double? assigmentTestResults = 0;
                        double? assigmentMaxPoints = 0;

                        double? bigTestsResults = 0;
                        double? bigTestsMaxPoints = 0;

                        double? smallTestResults = 0;
                        double? smallTestMaxPoints = 0;

                        foreach (var test in studentTestSignature.tests)
                        {
                            if (test.Type == Constants.TypeMultipleAssigment)
                            {
                                assigmentMaxPoints += test.GradeMax;
                                if (test.Result != null)
                                    assigmentTestResults += test.Result;

                            }
                            else if (test.Type == Constants.TypeBigTests)
                            {
                                bigTestsMaxPoints += test.GradeMax;
                                if (test.Result != null)
                                    bigTestsResults += test.Result;
                            }
                            else if (test.Type == Constants.TypeSmallTests)
                            {
                                smallTestMaxPoints += test.GradeMax;
                                if (test.Result != null)
                                    smallTestResults += test.Result;
                            }
                        }
                        double? assigmentPercentage = 0;
                        double? bigTestPercentage = 0;
                        double? smallTestPercentage = 0;
                        if (assigmentTestResults != 0 && assigmentMaxPoints != 0 && assigmentWeight != 0)
                        {
                            assigmentPercentage = (assigmentTestResults / assigmentMaxPoints) * assigmentWeight;
                        }
                        if (bigTestsResults != 0 && bigTestsMaxPoints != 0 && bigTestWeight != 0)
                        {
                            bigTestPercentage = (bigTestsResults / bigTestsMaxPoints) * bigTestWeight;
                        }
                        if (smallTestResults != 0 && smallTestMaxPoints != 0 && smallTestWeight != 0)
                        {
                            smallTestPercentage = (smallTestResults / smallTestMaxPoints) * smallTestWeight;
                        }
                        return GetGrade(assigmentPercentage, bigTestPercentage, smallTestPercentage, gradeConditions);
                    }
                }

            }

            return "";


        }
        public static int? GetOfferedGrade(double? assigmentPercentage, double? bigTestPercentage, double? smallTestPercentage, GradeCondition condition)
        {
            double divider = 0;
            if (assigmentPercentage != 0)
            {
                divider++;
            }
            if (bigTestPercentage != 0)
            {
                divider++;
            }
            if (smallTestPercentage != 0)
            {
                divider++;
            }
            double? percentage = (assigmentPercentage + bigTestPercentage + smallTestPercentage) / divider;
            if (percentage >= condition.OfferedGradeAPercentage)
            {
                return 5;
            }
            else if (percentage >= condition.OfferedGradeBPercentage)
            {
                return 4;
            }
            else if (percentage >= condition.OfferedGradeCPercentage)
            {
                return 3;
            }
            else if (percentage >= condition.OfferedGradeDPercentage)
            {
                return 2;
            }
            else
            {
                return 0;
            }
        }
        public static string? GetGrade(double? assigmentPercentage, double? bigTestPercentage, double? smallTestPercentage, Condition condition)
        {
            double divider = 0;
            if (assigmentPercentage != 0)
            {
                divider++;
            }
            if (bigTestPercentage != 0)
            {
                divider++;
            }
            if (smallTestPercentage != 0)
            {
                divider++;
            }
            double? percentage = (assigmentPercentage + bigTestPercentage + smallTestPercentage) / divider;

            if (percentage >= condition.GradeAPercentage)
            {
                return "Jeles";
            }
            else if (percentage >= condition.GradeBPercentage)
            {
                return "Jó";
            }
            else if (percentage >= condition.GradeCPercentage)
            {
                return "Közepes";
            }
            else if (percentage >= condition.GradeDPercentage)
            {
                return "Elégséges";
            }
            else
            {
                return "Elégtelen";
            }
        }

        public static List<Test> GetStudentTests(List<Test> tests, Student stud)
        {
            List<Test> studentTests = new List<Test>();
            foreach (Test test in tests)
            {
                if (test != null)
                {
                    if (test.Student.Neptuncode == stud.Neptuncode)
                    {
                        studentTests.Add(test);
                    }
                }
            }
            return studentTests;

        }
    }
}


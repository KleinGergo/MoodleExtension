using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;

namespace MoodleExtensionAPI.Models.Tests
{
    [TestClass()]
    public class StatisticsTest
    {
        [TestMethod()]
        public void IsJsonReadWasCorrect()
        {
            //Given
            SignatureCondition signatureCondition = readConditionFromJson("pkg/test/signatureConditions.json");

            //When
            List<Condition> expectedConditionList = createExpectedConditionList();
            SignatureCondition expectedSignatureCondition = new SignatureCondition();
            expectedSignatureCondition.Type = "complex";
            expectedSignatureCondition.Conditions = expectedConditionList;

            //Then
            Assert.AreEqual(signatureCondition.Type, expectedSignatureCondition.Type);
            Assert.AreEqual(signatureCondition.Conditions.Count, expectedSignatureCondition.Conditions.Count);

            for (int i = 0; i < signatureCondition.Conditions.Count; i++)
            {
                Assert.AreEqual(signatureCondition.Conditions[i].Type, expectedSignatureCondition.Conditions[i].Type);
            }

        }
        [TestMethod()]
        public void IsStudentPassedShouldBeFalseBecauseOfAssigment()
        {
            // Should be false, because the student does not completed the assigment
            List<Test> mockTests = CreateMockTests();
            SignatureCondition conditions = readConditionFromJson("pkg/test/signatureConditions.json");
            Subject sub = new Subject();
            bool IsAprroved = sub.IsSignatureApproved(mockTests, conditions);
            Assert.IsFalse(IsAprroved);

        }
        [TestMethod()]
        public void IsStudentPassedShouldBeFalseBecausOfIndividualTestsResult()
        {
            // Should be false, because the student does not completed the assigment
            List<Test> mockTests = CreateMockTests();

            Subject mockSubject = new Subject(
                SubjectID: 1,
                SubjectName: "mockSubject",
                SignatureCondition: "none"
               );
            Test mockTest = new Test(
             TestID: 1,
             Subject: mockSubject,
             Result: 0.88,
             IsCompleted: true,
             Type: "multipleAssigment",
             Student: new Student()
             );
            mockTests.Add(mockTest);
            SignatureCondition conditions = readConditionFromJson("pkg/test/signatureConditions.json");
            Subject sub = new Subject();
            bool IsAprroved = sub.IsSignatureApproved(mockTests, conditions);
            Assert.IsFalse(IsAprroved);

        }
        public List<Test> CreateMockTests()
        {

            Subject mockSubject = new Subject(
                SubjectID: 1,
                SubjectName: "mockSubject",
                SignatureCondition: "none"
               );
            Test mockTest1 = new Test(
           TestID: 1,
           Subject: mockSubject,
           Result: 0.88,
           IsCompleted: true,
           Type: "moodle",
           Student: new Student()
           );
            Test mockTest2 = new Test(
             TestID: 2,
             Subject: mockSubject,
             Result: 0.67,
             IsCompleted: true,
             Type: "moodle",
             Student: new Student()
             );
            Test mockTest3 = new Test(
             TestID: 3,
             Subject: mockSubject,
             Result: 0.78,
             IsCompleted: true,
             Type: "moodle",
             Student: new Student());
            List<Test> list = new List<Test>();
            list.Add(mockTest1);
            list.Add(mockTest2);
            list.Add(mockTest3);
            return list;
        }
        public SignatureCondition readConditionFromJson(string filePath)
        {
            string json = File.ReadAllText(filePath);

            SignatureConditionWrapper wrapper = JsonSerializer.Deserialize<SignatureConditionWrapper>(json);
            SignatureCondition condition = wrapper.SignatureCondition;

            return condition;
        }
        public List<Condition> createExpectedConditionList()
        {
            List<Condition> conditionList = new List<Condition>();
            Condition conditionAssigment = new Condition(
                Type: Constants.TypeMultipleAssigment,
                NumberOfTest: 2,
                RequiredNumberOfTest:2,
                RequiredIndividualTestPercentage:60.0,
                RequiredAvgTestPercentage:75.0,
                Weight: 0.1
                );
            Condition conditionMultipleAssigment = new Condition(
                Type: Constants.TypeBigTests,
                NumberOfTest: 5,
                RequiredNumberOfTest: 2,
                RequiredIndividualTestPercentage:60.0,
                RequiredAvgTestPercentage:75.0,
                Weight:0.7
                );
            Condition conditionTestPercentage = new Condition(
                Type: Constants.TypeSmallTests,
                NumberOfTest: 5,
                RequiredNumberOfTest: 2,
                RequiredIndividualTestPercentage:60.0,
                RequiredAvgTestPercentage:75.0,
                Weight:0.2
                );
            Condition conditionAllTestWritten = new Condition(
                Type: Constants.TypeGrading,
                GradeA:80.0,
                GradeB: 70.0,
                GradeC: 60.0,
                GradeD: 50.0
                );
            Condition smallTestWritten = new Condition(
                Type: Constants.TypeOfferedGrade,
                GradeA:90.0,
                GradeB: 80.0,
                GradeC: 80.0,
                GradeD: 80.0
                );

            conditionList.Add(conditionAssigment);
            conditionList.Add(conditionMultipleAssigment);
            conditionList.Add(conditionTestPercentage);
            conditionList.Add(conditionAllTestWritten);
            conditionList.Add(smallTestWritten);

            return conditionList;
        }
    }
}
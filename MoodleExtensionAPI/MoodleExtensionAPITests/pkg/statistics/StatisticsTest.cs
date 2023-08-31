using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoodleExtensionAPI.Models;
using MoodleExtensionAPI.pkg;
using MoodleExtensionAPI.pkg.statistics;
using System.IO;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

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
            expectedSignatureCondition.type = "complex";
            expectedSignatureCondition.conditions = expectedConditionList;

            //Then
            Assert.AreEqual(signatureCondition.type, expectedSignatureCondition.type);
            Assert.AreEqual(signatureCondition.conditions.Count, expectedSignatureCondition.conditions.Count);

            for (int i = 0; i < signatureCondition.conditions.Count; i++)
            {
                Assert.AreEqual(signatureCondition.conditions[i].type, expectedSignatureCondition.conditions[i].type);
                Assert.AreEqual(signatureCondition.conditions[i].required, expectedSignatureCondition.conditions[i].required);
                Assert.AreEqual(signatureCondition.conditions[i].minimumPercentage, expectedSignatureCondition.conditions[i].minimumPercentage);
            }

        }
        [TestMethod()]
        public void IsStudentPassedShouldBeFalseBecauseOfAssigment() {
            // Should be false, because the student does not completed the assigment
            List<Test> mockTests = CreateMockTests();
            SignatureCondition conditions = readConditionFromJson("pkg/test/signatureConditions.json");
            Subject sub = new Subject();
            bool IsAprroved = sub.IsSignatureApproved(mockTests, conditions);
            Assert.IsFalse(IsAprroved);
            
        }
        [TestMethod()]
        public void IsStudentPassedShouldBeFalseBecausOfIndividualTestsResult() {
            // Should be false, because the student does not completed the assigment
            List<Test> mockTests = CreateMockTests();
            Test mockTest = new Test(
             TestID: 1,
             Result: 0.88,
             StartDate: DateTime.Now,
             CompletionDate: DateTime.Now,
             TimeSpent: 10,
             TimeLimit: 300,
             IsCompleted: true,
             Type: "assigment"
             );
            Subject mockSubject = new Subject(
                SubjectID: "VEMISAB123213",
                DepartmentID: 1,
                SubjectName: "mockSubject",
                numberOfTests: 1,
                SignatureState: "NotApproved",
                SignatureCondition: "none"
               );
            mockTest.SubjectID.Add(mockSubject);
            mockTests.Add(mockTest);
            SignatureCondition conditions = readConditionFromJson("pkg/test/signatureConditions.json");
            Subject sub = new Subject();
            bool IsAprroved = sub.IsSignatureApproved(mockTests, conditions);
            Assert.IsFalse(IsAprroved);
            
        }
        public List<Test> CreateMockTests()
        {
            Test mockTest1 = new Test(
             TestID: 1,
             Result: 0.88,
             StartDate: DateTime.Now,
             CompletionDate: DateTime.Now,
             TimeSpent: 10,
             TimeLimit: 300,
             IsCompleted: true,
             Type: "moodle"
             );
            Subject mockSubject = new Subject(
                SubjectID: "VEMISAB123213",
                DepartmentID: 1,
                SubjectName: "mockSubject",
                numberOfTests: 1,
                SignatureState: "NotApproved",
                SignatureCondition: "none"
               );
            mockTest1.SubjectID.Add(mockSubject);
            Test mockTest2 = new Test(
             TestID: 2,      
             Result: 0.67,
             StartDate: DateTime.Now,
             CompletionDate: DateTime.Now,
             TimeSpent: 10,
             TimeLimit: 300,
             IsCompleted: true,
             Type: "moodle");
            mockTest2.SubjectID.Add(mockSubject);
            Test mockTest3 = new Test(
             TestID: 3,
             Result: 0.78,
             StartDate: DateTime.Now,
             CompletionDate: DateTime.Now,
             TimeSpent: 10,
             TimeLimit: 300,
             IsCompleted: true,
             Type: "moodle");
            mockTest3.SubjectID.Add(mockSubject);
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
            SignatureCondition condition = wrapper.signatureCondition;

            return condition;
        }
        public List<Condition> createExpectedConditionList() {
            List<Condition> conditionList = new List<Condition>();
            Condition conditionAssigment = new Condition(
                Type: Constants.TypeAssigment,
                Required: true
                );
            Condition conditionMultipleAssigment = new Condition(
                Type: Constants.TypeMultipleAssigment,
                Required: true,
                2
                );
            Condition conditionTestPercentage = new Condition(
                Type: Constants.TypeTestPercentage,
                MinimumPercentage: 75
                );
            Condition conditionAllTestWritten = new Condition(
                Type: Constants.TypeAllTestsWritten,
                Required: false,
                3
                ); 
            Condition smallTestWritten = new Condition(
                Type: Constants.TypeSmallTestsWritten,
                Required: true,
                5
                );
            Condition individualTestPercentage = new Condition(
                Type: Constants.TypeIndividualTestPercentage,
                MinimumPercentage: 60
                );
            conditionList.Add( conditionAssigment );
            conditionList.Add(conditionMultipleAssigment);
            conditionList.Add(conditionTestPercentage);
            conditionList.Add(conditionAllTestWritten);
            conditionList.Add(smallTestWritten);
            conditionList.Add(individualTestPercentage);

            return conditionList;
        }
    }
}
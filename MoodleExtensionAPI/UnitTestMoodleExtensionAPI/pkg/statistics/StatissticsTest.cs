
using MoodleExtensionAPI.Models;
using Newtonsoft.Json;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

[TestClass]
public class SubjectTests
{
    [DataTestMethod]
    [DataRow("VEMISAB321867", "DEP123", "Mathematics", 3, "Signed", 85, 90)]
    [DataRow("ABC123", "DEP456", "Physics", 5, "Unsigned", 75, 80)]
    public void SubjectProperties_InitializedCorrectly(string subjectID, string departmentID, string subjectName, int numberOfTests, string signatureState, int grade, int offeredGrade)
    {
        System.Diagnostics.Debugger.Launch();

        // Arrange

        List<Condition> conditions = new List<Condition>();
        SignatureCondition signatureCondition = readConditionFromJson("test/signatureConditions.json");
   

        // Act
        Subject subject = new Subject
        {
            SubjectID = subjectID,
            DepartmentID = departmentID,
            SubjectName = subjectName,
            numberOfTests = numberOfTests,
            SignatureState = signatureState,
            SignatureCondition = signatureCondition,
            Grade = grade,
            OfferedGrade = offeredGrade
        };

        // Assert
        Assert.AreEqual(subjectID, subject.SubjectID);
        Assert.AreEqual(departmentID, subject.DepartmentID);
        Assert.AreEqual(subjectName, subject.SubjectName);
        Assert.AreEqual(numberOfTests, subject.numberOfTests);
        Assert.AreEqual(signatureState, subject.SignatureState);
        Assert.AreEqual(signatureCondition, subject.SignatureCondition);
        Assert.AreEqual(grade, subject.Grade);
        Assert.AreEqual(offeredGrade, subject.OfferedGrade);
    }

    public SignatureCondition readConditionFromJson(string filePath)
    {
        string json = File.ReadAllText(filePath);

        SignatureCondition condition = JsonConvert.DeserializeObject<SignatureCondition>(json);

        return condition;
    }

}

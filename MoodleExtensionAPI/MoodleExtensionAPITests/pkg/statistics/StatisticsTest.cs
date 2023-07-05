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
                Required: true
                );
            Condition conditionTestPercentage = new Condition(
                Type: Constants.TypeTestPercentage,
                MinimumPercentage: 75
                );
            Condition conditionAllTestWritten = new Condition(
                Type: Constants.TypeAllTestsWritten,
                Required: false
                ); 
            Condition smallTestWritten = new Condition(
                Type: Constants.TypeSmallTestsWritten,
                Required: true
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
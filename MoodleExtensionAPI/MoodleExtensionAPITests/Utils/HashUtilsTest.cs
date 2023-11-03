using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MoodleExtensionAPITests.Utils
{
    [TestClass()]
    public class HashUtilsTest
    {
        [TestMethod()]
        public void IsHashStringCorrect()
        {
            string expectedOutput = "9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08";
            Assert.AreEqual(HashUtils.HashString("test"), expectedOutput);
        }
    }
}

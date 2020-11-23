using Aether.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aether.Tests.Extensions
{
    [TestClass]
    public class StringExtensionsTests
    {
        [TestMethod]
        [DataRow("abcdef", "abcdef", true)]
        [DataRow("ABCdef", "abcdef", true)]
        [DataRow("ABC", "def", false)]
        [DataRow("", "", true)]
        public void LikeTest(string str1, string str2, bool expectedResult)
        {
            Assert.AreEqual(expectedResult, str1.Like(str2));
        }

        [TestMethod]
        [DataRow("abcdef", true)]
        [DataRow("    ", false)]
        [DataRow("", false)]
        [DataRow(null, false)]
        public void ExistsTest(string str, bool expectedResult)
        {
            Assert.AreEqual(expectedResult, str.Exists());
        }
    }
}

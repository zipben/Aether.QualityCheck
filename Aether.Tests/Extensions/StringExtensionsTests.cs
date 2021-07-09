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

        [TestMethod]
        [DataRow("abcdef", "abcdef")]
        [DataRow("ThisIsACamel", "This Is A Camel")]
        [DataRow("THIIISSSSIsACamel", "THIIISSSS Is A Camel")]
        [DataRow("PartyInTheUSA", "Party In The USA")]
        [DataRow("IAmnotAProperlyConstructedSentence", "I Amnot A Properly Constructed Sentence")]
        public void SplitCameCaseTest(string str, string expectedResult)
        {
            Assert.AreEqual(expectedResult, str.SplitCamelCase());
        }

        [TestMethod]
        [DataRow("abcdef")]
        [DataRow("ThisIsACamel")]
        [DataRow("THIIISSSSIsACamel")]
        [DataRow("PartyInTheUSA")]
        [DataRow("IAmnotAProperlyConstructedSentence")]
        public void EncodeString(string str)
        {
            var encodedString = str.Encode64();

            var base64EncodedBytes = System.Convert.FromBase64String(encodedString);
            var decodedString = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);

            Assert.AreEqual(str, decodedString);
        }

        [TestMethod]
        [DataRow("abcdef")]
        [DataRow("ThisIsACamel")]
        [DataRow("THIIISSSSIsACamel")]
        [DataRow("PartyInTheUSA")]
        [DataRow("IAmnotAProperlyConstructedSentence")]
        public void DecodeString(string str)
        {
            var encodedString = str.Encode64();

            var decodedString = encodedString.Decode64();

            Assert.AreEqual(str, decodedString);
        }
    }
}

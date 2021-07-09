using Aether.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Aether.Tests.Extensions
{
    [TestClass]
    public class GenericExtensionsTests
    {
        [TestMethod]
        public void DecodeString()
        {
            var testObj = new Dictionary<string, string>() { { "test", "object" } };

            string encoded = testObj.Encode64();

            Dictionary<string, string> decodedObject = encoded.Decode64<Dictionary<string, string>>();

            CollectionAssert.AreEqual(testObj.Values, decodedObject.Values);
        }
    }
}

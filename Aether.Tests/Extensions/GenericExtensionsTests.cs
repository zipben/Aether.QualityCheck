using Aether.Extensions;
using Aether.Models.ErisClient;
using AutoBogus;
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

        [TestMethod]
        public void CompressDecompressDictionary()
        {
            var testObj = new Dictionary<string, string>() { { "test", "object" } };

            string encoded = testObj.Compress();

            Dictionary<string, string> decodedObject = encoded.Decompress<Dictionary<string, string>>();

            CollectionAssert.AreEqual(testObj.Values, decodedObject.Values);
        }

        [TestMethod]
        public void CompressDecompressIdentifiersRoot()
        {
            var testObj = AutoFaker.Generate<IdentifiersRoot>();

            string compressed = testObj.Compress();

            IdentifiersRoot decompressed = compressed.Decompress<IdentifiersRoot>();

            Assert.AreEqual(decompressed.SluggishHash(), testObj.SluggishHash());
        }
    }
}

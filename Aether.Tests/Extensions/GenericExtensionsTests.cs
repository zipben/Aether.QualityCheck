using Aether.Extensions;
using Aether.Models.ErisClient;
using AutoBogus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Sockets;

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


        [TestMethod]
        public void GenerateHttpStringContentNullTest()
        {
            string data = null;
            var x = data.GenerateHttpStringContent();
            Assert.IsNotNull(x);

        }
        [TestMethod]
        [ExpectedException(typeof(JsonSerializationException))]
        public void GenerateHttpStringContentIllegalObjectTest()
        {
            var data = new TcpClient();
            var x = data.GenerateHttpStringContent();
        }

        [TestMethod]
        [ExpectedException(typeof(JsonSerializationException))]
        public void Encode64IllegalObjectTest()
        {
            var data = new TcpClient();
            var x = data.Encode64();
        }

        [TestMethod]
        [ExpectedException(typeof(JsonSerializationException))]
        public void CompressIllegalObjectTest()
        {
            var data = new TcpClient();
            var x = data.Compress();
        }
    }
}

using Aether.Extensions;
using Aether.Models;
using AutoBogus;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aether.Tests.Models
{
    [TestClass]
    public class MessageBodyTests
    {
        [TestMethod]
        public void MessageBodyTest()
        {
            MessageBody testModelA = AutoFaker.Generate<MessageBody>();
            MessageBody testModelB = testModelA.SluggishClone();
            Assert.AreEqual(testModelA.SluggishHash(), testModelB.SluggishHash());
        }
    }
}

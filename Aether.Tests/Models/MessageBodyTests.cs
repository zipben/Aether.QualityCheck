using Aether.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aether.Tests.Models
{
    [TestClass]
    public class MessageBodyTests
    {
        [TestMethod]
        public void MessageBodyMessageTypeTest()
        {
            var test = Guid.NewGuid().ToString();
            var model = new MessageBody();
            model.MessageType = test;
            Assert.AreEqual(test, model.MessageType);
        }

        [TestMethod]
        public void MessageBodyMessageIdTest()
        {
            var test = Guid.NewGuid().ToString();
            var model = new MessageBody();
            model.MessageId = test;
            Assert.AreEqual(test, model.MessageId);
        }

        [TestMethod]
        public void MessageBodyTopicArnTest()
        {
            var test = Guid.NewGuid().ToString();
            var model = new MessageBody();
            model.TopicArn = test;
            Assert.AreEqual(test, model.TopicArn);
        }

        [TestMethod]
        public void MessageBodySerializedMessageTest()
        {
            var test = Guid.NewGuid().ToString();
            var model = new MessageBody();
            model.SerializedMessage = test;
            Assert.AreEqual(test, model.SerializedMessage);
        }

        [TestMethod]
        public void MessageBodyTimestampTest()
        {
            var test = DateTime.Now;
            var model = new MessageBody();
            model.Timestamp = test;
            Assert.AreEqual(test, model.Timestamp);
        }

        [TestMethod]
        public void MessageBodySignatureVersionTest()
        {
            var test = 55;
            var model = new MessageBody();
            model.SignatureVersion = test;
            Assert.AreEqual(test, model.SignatureVersion);
        }

        [TestMethod]
        public void MessageBodySignatureTest()
        {
            var test = Guid.NewGuid().ToString();
            var model = new MessageBody();
            model.Signature = test;
            Assert.AreEqual(test, model.Signature);
        }

        [TestMethod]
        public void MessageBodySigningCertUrlTest()
        {
            var test = Guid.NewGuid().ToString();
            var model = new MessageBody();
            model.SigningCertUrl = test;
            Assert.AreEqual(test, model.SigningCertUrl);
        }

        [TestMethod]
        public void MessageBodyUnsubscribeUrlTest()
        {
            var test = Guid.NewGuid().ToString();
            var model = new MessageBody();
            model.UnsubscribeUrl = test;
            Assert.AreEqual(test, model.UnsubscribeUrl);
        }
    }
}

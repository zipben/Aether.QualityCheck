using Aether.Extensions;
using Aether.Models.ErisClient;
using AutoBogus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aether.Tests.Models
{
    [TestClass]
    public class IdentifierModelTests
    {

        [TestMethod]
        public void ModelProps_TestHash()
        {
            IdentifierRequestModel model = AutoFaker.Generate<IdentifierRequestModel>();
            IdentifierRequestModel copy = model.SluggishClone();

            Assert.AreEqual(model.SluggishHash(), copy.SluggishHash());
        }

    }
}

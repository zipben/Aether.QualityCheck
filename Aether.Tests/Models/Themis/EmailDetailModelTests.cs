using Aether.Extensions;
using Aether.Models.Themis;
using AutoBogus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aether.Tests.Models.Themis
{
    [TestClass]
    public class EmailDetailModelTests
    {
        [TestMethod]
        public void EmailDetailModelTest()
        {
            EmailDetailModel testModelA = AutoFaker.Generate<EmailDetailModel>();
            EmailDetailModel testModelB = testModelA.SluggishClone();
            Assert.AreEqual(testModelA.SluggishHash(), testModelB.SluggishHash());
        }
    }
}

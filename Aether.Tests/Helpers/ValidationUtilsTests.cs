using Aether.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aether.Tests.Helpers
{
    [TestClass]
    public class ValidationUtilsTests
    {
        [TestMethod]
        [DataRow("zip@gmail.com")]
        [DataRow("burt@gurt.co.uk")]
        [DataRow("guerder@curn.co")]
        public void IsValidEmailString_AllValid(string str)
        {
            Assert.IsTrue(ValidationUtils.IsValidEmailAddress(str));
        }

        [TestMethod]
        [DataRow("zipATgmail")]
        [DataRow("burtASKJASKJAgurt")]
        [DataRow("guerder")]
        public void IsValidEmailString_AllInvalid(string str)
        {
            Assert.IsFalse(ValidationUtils.IsValidEmailAddress(str));
        }
    }
}

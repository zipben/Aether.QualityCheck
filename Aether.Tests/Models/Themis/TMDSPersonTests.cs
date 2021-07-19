using Aether.Models.Themis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aether.Tests.Models.Themis
{
    [TestClass]
    public class TMDSPersonTests
    {
        [TestMethod]
        public void TMDSPersonRockHumanIDTest()
        {
            var person = new TMDSPerson();
            var test = Guid.NewGuid().ToString();
            person.RockHumanID = test;
            Assert.AreEqual(test, person.RockHumanID);
        }

        [TestMethod]
        public void TMDSPersonCommonIDTest()
        {
            var person = new TMDSPerson();
            var test = Guid.NewGuid().ToString();
            person.CommonID = test;
            Assert.AreEqual(test, person.CommonID);
        }

        [TestMethod]
        public void TMDSPersonFirstNameTest()
        {
            var person = new TMDSPerson();
            var test = Guid.NewGuid().ToString();
            person.FirstName = test;
            Assert.AreEqual(test, person.FirstName);
        }

        [TestMethod]
        public void TMDSPersonLastNameTest()
        {
            var person = new TMDSPerson();
            var test = Guid.NewGuid().ToString();
            person.LastName = test;
            Assert.AreEqual(test, person.LastName);
        }

        [TestMethod]
        public void TMDSPersonEmailTest()
        {
            var person = new TMDSPerson();
            var test = Guid.NewGuid().ToString();
            person.Email = test;
            Assert.AreEqual(test, person.Email);
        }

        [TestMethod]
        public void TMDSPersonTitleTest()
        {
            var person = new TMDSPerson();
            var test = Guid.NewGuid().ToString();
            person.Title = test;
            Assert.AreEqual(test, person.Title);
        }

        [TestMethod]
        public void TMDSPersonBusinessAreaTest()
        {
            var person = new TMDSPerson();
            var test = Guid.NewGuid().ToString();
            person.BusinessArea = test;
            Assert.AreEqual(test, person.BusinessArea);
        }

        [TestMethod]
        public void TMDSPersonCompanyTest()
        {
            var person = new TMDSPerson();
            var test = Guid.NewGuid().ToString();
            person.Company = test;
            Assert.AreEqual(test, person.Company);
        }
    }
}

using Aether.Models.Themis;
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
        public void EmailDetailModelIdTest()
        {
            var test = Guid.NewGuid().ToString();
            var model = new EmailDetailModel();
            model.Id = test;
            Assert.AreEqual(test, model.Id);
        }

        [TestMethod]
        public void EmailDetailModelCaseNameTest()
        {
            var test = Guid.NewGuid().ToString();
            var model = new EmailDetailModel();
            model.CaseName = test;
            Assert.AreEqual(test, model.CaseName);
        }

        [TestMethod]
        public void EmailDetailModelFromTest()
        {
            var test = Guid.NewGuid().ToString();
            var model = new EmailDetailModel();
            model.From = test;
            Assert.AreEqual(test, model.From);
        }

        [TestMethod]
        public void EmailDetailModelToTest()
        {
            var test = Guid.NewGuid().ToString();
            var model = new EmailDetailModel();
            model.To = new List<string>();
            model.To.Add(test);
            Assert.IsTrue(model.To.Contains(test));
        }

        [TestMethod]
        public void EmailDetailModelSubjectTest()
        {
            var test = Guid.NewGuid().ToString();
            var model = new EmailDetailModel();
            model.Subject = test;
            Assert.AreEqual(test, model.Subject);
        }

        [TestMethod]
        public void EmailDetailModelBodyTest()
        {
            var test = Guid.NewGuid().ToString();
            var model = new EmailDetailModel();
            model.Body = test;
            Assert.AreEqual(test, model.Body);
        }

        [TestMethod]
        public void EmailDetailModelCreatedByIdTest()
        {
            var test = 56;
            var model = new EmailDetailModel();
            model.CreatedById = test;
            Assert.AreEqual(test, model.CreatedById);
        }

        [TestMethod]
        public void EmailDetailModelCreatedDateTest()
        {
            var test = DateTime.Now;
            var model = new EmailDetailModel();
            model.CreatedDate = test;
            Assert.AreEqual(test, model.CreatedDate);
        }

        [TestMethod]
        public void EmailDetailModelLastUpdatedByIdTest()
        {
            var test = 57;
            var model = new EmailDetailModel();
            model.LastUpdatedById = test;
            Assert.AreEqual(test, model.LastUpdatedById);
        }

        [TestMethod]
        public void EmailDetailModelLastUpdatedDateTest()
        {
            var test = DateTime.Now;
            var model = new EmailDetailModel();
            model.LastUpdatedDate = test;
            Assert.AreEqual(test, model.LastUpdatedDate);
        }

        [TestMethod]
        public void EmailDetailModelEmailConfirmationIdTest()
        {
            var test = Guid.NewGuid();
            var model = new EmailDetailModel();
            model.EmailConfirmationId = test;
            Assert.AreEqual(test, model.EmailConfirmationId);
        }

        [TestMethod]
        public void EmailDetailModelIsConfirmedTest()
        {
            var test = true;
            var model = new EmailDetailModel();
            model.IsConfirmed = test;
            Assert.AreEqual(test, model.IsConfirmed);
        }
    }
}

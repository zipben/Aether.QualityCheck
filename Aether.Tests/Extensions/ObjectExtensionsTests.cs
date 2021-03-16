using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aether.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Aether.Models;
using static Aether.Models.NotificationServiceEmailBody;

namespace Aether.Extensions.Tests
{
    [TestClass()]
    public class ObjectExtensionsTests
    {
        [TestMethod]
        public void TestClone_EmailSendModel()
        {
            EmailSendModel model = new EmailSendModel()
            {
                TemplateId = "TemplateId",
                Stage = "Stage",
                ApplicationId = "ApplicationId",
                From = "From",
                To = new List<string>() { "Item1", "Item2" },
                CC = new List<string>() { "ThisIs", "THeCCList" },
                Subject = "Subjecttt",
                Body = "BodyBaby"
            };

            var clone = model.SluggishClone();

            Assert.AreEqual(clone.SluggishHash(), model.SluggishHash());
        }

        [TestMethod]
        public void TestClone_Classification()
        {
            Classification model = new Classification()
            {
                ClassificationName = "ClassificationName",
                FieldValues = new Dictionary<string, List<string>>() { { "Keybaby", "LIST".CreateList() } },
                Category = "Category"
            };


            var clone = model.SluggishClone();

            Assert.AreEqual(clone.SluggishHash(), model.SluggishHash());
        }

        [TestMethod]
        public void TestClone_Classification_CheckForTangledReferences()
        {
            Classification model = new Classification()
            {
                ClassificationName = "ClassificationName",
                FieldValues = new Dictionary<string, List<string>>() { { "Keybaby", "LIST".CreateList() } },
                Category = "Category"
            };


            var clone = model.SluggishClone();

            model.FieldValues["NewKey"] = "newList".CreateList();

            Assert.AreNotEqual(clone.FieldValues.Count, model.FieldValues.Count);
        }

        [TestMethod]
        public void TestHash_HashIsntZero()
        {
            Classification model = new Classification()
            {
                ClassificationName = "ClassificationName",
                FieldValues = new Dictionary<string, List<string>>() { { "Keybaby", "LIST".CreateList() } },
                Category = "Category"
            };

            Assert.AreNotEqual(0, model.SluggishHash());
        }

        [TestMethod]
        public void TestHash_HashValuesAreTheSame()
        {
            Classification model1 = new Classification()
            {
                ClassificationName = "ClassificationName",
                Category = "Category"
            };

            Classification model2 = new Classification()
            {
                ClassificationName = "ClassificationName",
                Category = "Category"
            };

            Assert.AreEqual(model1.SluggishHash(), model2.SluggishHash());
        }

        [TestMethod]
        public void TestHash_HashValuesAreDifferent()
        {
            Classification model1 = new Classification()
            {
                ClassificationName = "ClassificationName",
                Category = "Category"
            };

            Classification model2 = new Classification()
            {
                ClassificationName = "IAMDIFFERENT",
                Category = "IAMALSODIFFERENT"
            };

            Assert.AreNotEqual(model1.SluggishHash(), model2.SluggishHash());
        }

        [TestMethod]
        public void TestHash_HashValuesAreTheSame_NestedObjects()
        {
            Classification model1 = new Classification()
            {
                ClassificationName = "ClassificationName",
                Category = "Category",
                FieldValues = new Dictionary<string, List<string>>() { { "Key", "Value".CreateList()} }
            };

            Classification model2 = new Classification()
            {
                ClassificationName = "ClassificationName",
                Category = "Category",
                FieldValues = new Dictionary<string, List<string>>() { { "Key", "Value".CreateList() } }
            };

            Assert.AreEqual(model1.SluggishHash(), model2.SluggishHash());
        }

        [TestMethod]
        public void TestHash_HashValuesAreDifferent_NestedObjects()
        {
            Classification model1 = new Classification()
            {
                ClassificationName = "ClassificationName",
                Category = "Category",
                FieldValues = new Dictionary<string, List<string>>() { { "Key", "Value".CreateList() } }
            };

            Classification model2 = new Classification()
            {
                ClassificationName = "ClassificationName",
                Category = "Category",
                FieldValues = new Dictionary<string, List<string>>() { { "Key", "Value22222".CreateList() } }
            };

            Assert.AreNotEqual(model1.SluggishHash(), model2.SluggishHash());
        }
    }
}
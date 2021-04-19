using System;
using System.Collections.Generic;
using System.Text;
using Aether.Helpers;
using APILogger.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Aether.Tests.Helpers
{
    [TestClass]
    public class MethodHelperTests
    {
        private MethodHelper _target;
        private Mock<IApiLogger> _mockLogger;

        private string _testMethodName;
        private string _testParam1;
        private string _testParam2;
        private string _testParam3;
        private object _testObjToLog;
        private string _testMessage;
        private object _testReturnObject;

        [TestInitialize]
        public void Init()
        {
            _mockLogger = new Mock<IApiLogger>();

            _target = new MethodHelper(_mockLogger.Object);

            SetupTestData();
        }

        private void SetupTestData()
        {
            _testMethodName = Guid.NewGuid().ToString();
            _testParam1 = Guid.NewGuid().ToString();
            _testParam2 = Guid.NewGuid().ToString();
            _testParam3 = Guid.NewGuid().ToString();
            _testObjToLog = new { _testParam1, _testParam2, _testParam3 };
            _testMessage = Guid.NewGuid().ToString();
            _testReturnObject = new object();
        }

        [TestMethod]
        public void BeginTest_Params()
        {
            _target.Begin(out var actualMethodName, out var actualObjectToLog, _testMethodName, new { _testParam1, _testParam2, _testParam3 });

            _mockLogger.Verify(x => x.LogInfo(It.IsAny<string>(), _testObjToLog), Times.Once);

            Assert.AreEqual(_testMethodName, actualMethodName);
            Assert.AreEqual(_testObjToLog, actualObjectToLog);
        }

        [TestMethod]
        public void BeginTest_Function()
        {
            _target.Begin(out var actualMethodName, out var actualObjectToLog, _testMethodName, MakeObjToLog);

            _mockLogger.Verify(x => x.LogInfo(It.IsAny<string>(), _testObjToLog), Times.Once);

            Assert.AreEqual(_testMethodName, actualMethodName);
            Assert.AreEqual(_testObjToLog, actualObjectToLog);
        }

        private object MakeObjToLog() =>
            new { _testParam1, _testParam2, _testParam3 };

        [TestMethod]
        public void EndRequestOkTest_OkResult()
        {
            var actionResult = _target.EndRequestOk(_testMethodName, _testObjToLog);

            _mockLogger.Verify(x => x.LogInfo(It.IsAny<string>(), _testObjToLog), Times.Once);

            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(OkResult));
        }

        [TestMethod]
        public void EndRequestOkTest_OkObjectResult_ReturnString()
        {
            var actionResult = _target.EndRequestOk(_testMethodName, _testMessage, _testObjToLog);

            _mockLogger.Verify(x => x.LogInfo(It.IsAny<string>(), _testObjToLog), Times.Once);

            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult));

            var okResult = (OkObjectResult)actionResult;
            Assert.IsInstanceOfType(okResult.Value, typeof(string));
            Assert.AreEqual(_testMessage, okResult.Value);
        }

        [TestMethod]
        public void EndRequestOkTest_OkObjectResult_ReturnObject()
        {
            var actionResult = _target.EndRequestOk(_testMethodName, _testReturnObject, _testObjToLog);

            _mockLogger.Verify(x => x.LogInfo(It.IsAny<string>(), _testObjToLog), Times.Once);

            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult.Result, typeof(OkObjectResult));

            var okResult = (OkObjectResult) actionResult.Result;
            Assert.IsInstanceOfType(okResult.Value, typeof(object));
            Assert.AreEqual(_testReturnObject, okResult.Value);
        }

        [TestMethod]
        public void EndRequestBadRequestTest()
        {
            var actionResult = _target.EndRequestBadRequest(_testMessage, _testMethodName, null, _testObjToLog);

            _mockLogger.Verify(x => x.LogError(It.IsAny<string>(), _testObjToLog, null), Times.Once);

            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void EndRequestNotFoundTest()
        {
            var actionResult = _target.EndRequestNotFound(_testMethodName, _testObjToLog);

            _mockLogger.Verify(x => x.LogInfo(It.IsAny<string>(), _testObjToLog), Times.Once);

            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }
    }
}

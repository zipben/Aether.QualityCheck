using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aether.QualityChecks.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Aether.QualityChecks.IntegrationTests.TestQualityChecks;
using System.Threading.Tasks;
using Moq;
using Microsoft.AspNetCore.Http;

namespace Aether.QualityChecks.Helpers.Tests
{
    [TestClass()]
    public class QualityCheckExecutionHandlerTests
    {
        Mock<HttpRequest> _postRequest;
        Mock<HttpRequest> _getRequest;

        [TestInitialize]
        public void Init() 
        {
            _getRequest = new Mock<HttpRequest>();
            _getRequest.Setup(g => g.Method).Returns("GET");
        }


        [TestMethod()]
        public async Task ExecuteQualityCheckTest_AsyncWithInitAndTearDown()
        {
            Mock<IStepExecutionTester> stepTester = new Mock<IStepExecutionTester>();
            QualityCheckExecutionHandler handler = new QualityCheckExecutionHandler();
            AsyncWithInitAndTearDown testQc = new AsyncWithInitAndTearDown(stepTester.Object);

            var response = await handler.ExecuteQualityCheck(testQc);

            stepTester.Verify(s => s.Initialize(), Times.Once);
            stepTester.Verify(s => s.Step(), Times.Exactly(4));
            stepTester.Verify(s => s.Teardown(), Times.Once);
        }

        [TestMethod()]
        public async Task ExecuteQualityCheckTest_InitAndTearDownWith4Steps()
        {
            Mock<IStepExecutionTester> stepTester = new Mock<IStepExecutionTester>();
            QualityCheckExecutionHandler handler = new QualityCheckExecutionHandler();
            InitAndTearDownWith4Steps testQc = new InitAndTearDownWith4Steps(stepTester.Object);

            var response = await handler.ExecuteQualityCheck(testQc);

            stepTester.Verify(s => s.Initialize(), Times.Once);
            stepTester.Verify(s => s.Step(), Times.Exactly(4));
            stepTester.Verify(s => s.Teardown(), Times.Once);
        }

        [TestMethod()]
        public async Task ExecuteQualityCheckTest_MixedAsyncInitAndTearDownWith4Steps()
        {
            Mock<IStepExecutionTester> stepTester = new Mock<IStepExecutionTester>();
            QualityCheckExecutionHandler handler = new QualityCheckExecutionHandler();
            MixedAsyncInitAndTearDownWith4Steps testQc = new MixedAsyncInitAndTearDownWith4Steps(stepTester.Object);

            var response = await handler.ExecuteQualityCheck(testQc);

            stepTester.Verify(s => s.Initialize(), Times.Once);
            stepTester.Verify(s => s.Step(), Times.Exactly(4));
            stepTester.Verify(s => s.Teardown(), Times.Once);
        }

        [TestMethod()]
        public async Task ExecuteQualityCheckTest_AsyncWithNoInitAndNoTeardown()
        {
            Mock<IStepExecutionTester> stepTester = new Mock<IStepExecutionTester>();
            QualityCheckExecutionHandler handler = new QualityCheckExecutionHandler();
            AsyncWithNoSteps testQc = new AsyncWithNoSteps(stepTester.Object);

            var response = await handler.ExecuteQualityCheck(testQc);

            stepTester.Verify(s => s.Initialize(), Times.Once);
            stepTester.Verify(s => s.Step(), Times.Never);
            stepTester.Verify(s => s.Teardown(), Times.Once);
        }

        [TestMethod()]
        public async Task ExecuteQualityCheckTest_AsyncWithInitAndTearDownPlusDataSteps()
        {
            Mock<IStepExecutionTester> stepTester = new Mock<IStepExecutionTester>();
            QualityCheckExecutionHandler handler = new QualityCheckExecutionHandler();
            AsyncWithInitAndTearDownPlusDataSteps testQc = new AsyncWithInitAndTearDownPlusDataSteps(stepTester.Object);

            var response = await handler.ExecuteQualityCheck(testQc);

            stepTester.Verify(s => s.Initialize(), Times.Once);
            stepTester.Verify(s => s.Step(), Times.Exactly(2));
            stepTester.Verify(s => s.Step(1,2), Times.Once);
            stepTester.Verify(s => s.Step(3,4), Times.Once);
            stepTester.Verify(s => s.Step("hi", "there"), Times.Once);
            stepTester.Verify(s => s.Step("bye", "now"), Times.Once);
            stepTester.Verify(s => s.Teardown(), Times.Once);
        }

        [TestMethod()]
        public async Task ExecuteQualityCheckTest_AsyncWithDataInitAndTearDownPlusDataSteps()
        {
            Mock<IStepExecutionTester> stepTester = new Mock<IStepExecutionTester>();
            QualityCheckExecutionHandler handler = new QualityCheckExecutionHandler();
            AsyncWithDataInitAndTearDownPlusDataSteps testQc = new AsyncWithDataInitAndTearDownPlusDataSteps(stepTester.Object);

            var response = await handler.ExecuteQualityCheck(testQc);

            //All the executios are doubled because the init with Data reruns the whole test
            stepTester.Verify(s => s.InitializeWithData(1, 2, 3), Times.Once);
            stepTester.Verify(s => s.InitializeWithData(3, 4, 5), Times.Once);
            stepTester.Verify(s => s.Step(), Times.Exactly(4));
            stepTester.Verify(s => s.Step(1,2), Times.Exactly(2));
            stepTester.Verify(s => s.Step(3,4), Times.Exactly(2));
            stepTester.Verify(s => s.Step("hi", "there"), Times.Exactly(2));
            stepTester.Verify(s => s.Step("bye", "now"), Times.Exactly(2));
            stepTester.Verify(s => s.Teardown(), Times.Exactly(2));
        }

    }

    public interface IStepExecutionTester
    {
        public void Initialize();
        public void InitializeWithData(int val, int val2, int val3);
        public void Step();
        public void Step(object param);
        public void Step(object param1, object param2);
        public void Teardown();
    }
}
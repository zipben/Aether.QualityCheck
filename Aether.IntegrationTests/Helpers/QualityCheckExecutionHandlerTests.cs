using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aether.QualityChecks.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Aether.QualityChecks.IntegrationTests.TestQualityChecks;
using System.Threading.Tasks;
using Moq;

namespace Aether.QualityChecks.Helpers.Tests
{
    [TestClass()]
    public class QualityCheckExecutionHandlerTests
    {
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

    }

    public interface IStepExecutionTester
    {
        public void Initialize();
        public void Step();
        public void Teardown();
    }
}
using Aether.QualityChecks.Attributes;
using Aether.QualityChecks.Helpers;
using Aether.QualityChecks.Helpers.Tests;
using Aether.QualityChecks.Interfaces;
using Aether.QualityChecks.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aether.QualityChecks.IntegrationTests.TestQualityChecks
{
    public class MixedAsyncInitAndTearDownWith4Steps : IQualityCheck
    {
        private readonly IStepExecutionTester _tester;

        public MixedAsyncInitAndTearDownWith4Steps(IStepExecutionTester tester)
        {
            _tester = tester;
        }

        [QualityCheckInitialize]
        public async Task Init()
        {
            _tester.Initialize();
        }

        [QualityCheckStep(1)]
        public async Task Step1()
        {
            _tester.Step();
            Step.Proceed();
        }

        [QualityCheckStep(2)]
        public void Step2()
        {
            _tester.Step();
            Step.Proceed();
        }

        [QualityCheckStep(3)]
        public async Task Step3()
        {
            _tester.Step();
            Step.Proceed();
        }

        [QualityCheckStep(4)]
        public void Step4()
        {
            _tester.Step();
            Step.Proceed();
        }

        [QualityCheckTearDown]
        public async Task TearDown()
        {
            _tester.Teardown();
        }

        public Task<QualityCheckResponseModel> RunAsync()
        {
            throw new NotImplementedException();
        }

        public Task TearDownAsync()
        {
            throw new NotImplementedException();
        }
    }
}

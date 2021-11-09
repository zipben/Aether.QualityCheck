using Aether.QualityChecks.Attributes;
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

        public string LogName => nameof(AsyncWithInitAndTearDown);

        [QualityCheckInitialize]
        public async Task Init()
        {
            _tester.Initialize();
        }

        [QualityCheckStep(1)]
        public async Task<StepResponse> Step1()
        {
            _tester.Step();
            return new StepResponse() { Name = nameof(Step1) };
        }

        [QualityCheckStep(2)]
        public StepResponse Step2()
        {
            _tester.Step();
            return new StepResponse() { Name = nameof(Step2) };
        }

        [QualityCheckStep(3)]
        public async Task<StepResponse> Step3()
        {
            _tester.Step();
            return new StepResponse() { Name = nameof(Step3) };
        }

        [QualityCheckStep(4)]
        public StepResponse Step4()
        {
            _tester.Step();
            return new StepResponse() { Name = nameof(Step4) };
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

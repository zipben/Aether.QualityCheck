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
    public class AsyncWithNoInitAndNoTeardown : IQualityCheck
    {
        private readonly IStepExecutionTester _tester;

        public AsyncWithNoInitAndNoTeardown(IStepExecutionTester tester)
        {
            _tester = tester;
        }

        public string LogName => nameof(AsyncWithInitAndTearDown);

        [QualityCheckStep(1)]
        public async Task<StepResponse> Step1()
        {
            _tester.Step();
            return new StepResponse() { Name = nameof(Step1) };
        }

        [QualityCheckStep(2)]
        public async Task<StepResponse> Step2()
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
        public async Task<StepResponse> Step4()
        {
            _tester.Step();
            return new StepResponse() { Name = nameof(Step4) };
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

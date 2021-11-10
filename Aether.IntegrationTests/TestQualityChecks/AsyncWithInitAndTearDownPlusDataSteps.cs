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
    public class AsyncWithInitAndTearDownPlusDataSteps : IQualityCheck
    {
        private readonly IStepExecutionTester _tester;

        public AsyncWithInitAndTearDownPlusDataSteps(IStepExecutionTester tester)
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

        [QualityCheckData(1, 2)]
        [QualityCheckData(3, 4)]
        [QualityCheckStep(2)]
        public async Task<StepResponse> Step2(int param1, int param2)
        {
            _tester.Step(param1, param2);
            return new StepResponse() { Name = nameof(Step2) };
        }

        [QualityCheckData("hi", "there")]
        [QualityCheckData("bye", "now")]
        [QualityCheckStep(3)]
        public async Task<StepResponse> Step3(string param1, string param2)
        {
            _tester.Step(param1, param2);
            return new StepResponse() { Name = nameof(Step3) };
        }

        [QualityCheckStep(4)]
        public async Task<StepResponse> Step4()
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

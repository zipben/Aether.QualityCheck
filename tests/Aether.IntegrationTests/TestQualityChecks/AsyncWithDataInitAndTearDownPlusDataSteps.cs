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
    public class AsyncWithDataInitAndTearDownPlusDataSteps : IQualityCheck
    {
        private readonly IStepExecutionTester _tester;

        public AsyncWithDataInitAndTearDownPlusDataSteps(IStepExecutionTester tester)
        {
            _tester = tester;
        }

        [QualityCheckInitialize]
        [QualityCheckInitializeData(1, 2, 3)]
        [QualityCheckInitializeData(3, 4, 5)]
        public async Task Init(int val1, int val2, int val3)
        {
            _tester.InitializeWithData(val1, val2, val3);
        }

        [QualityCheckStep(1)]
        public async Task Step1()
        {
            _tester.Step();
            Step.Proceed();
        }

        [QualityCheckData(1, 2)]
        [QualityCheckData(3, 4)]
        [QualityCheckStep(2)]
        public async Task Step2(int param1, int param2)
        {
            _tester.Step(param1, param2);
            Step.Proceed();
        }

        [QualityCheckData("hi", "there")]
        [QualityCheckData("bye", "now")]
        [QualityCheckStep(3)]
        public async Task Step3(string param1, string param2)
        {
            _tester.Step(param1, param2);
            Step.Proceed();
        }

        [QualityCheckStep(4)]
        public async Task Step4()
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

﻿using Aether.QualityChecks.Attributes;
using Aether.QualityChecks.Interfaces;
using Aether.QualityChecks.Models;
using SmokeAndMirrors.TestDependencies;
using System.Threading.Tasks;

namespace SmokeAndMirrors.QualityChecks
{
    public class DummyQualityCheckFail : IQualityCheck
    {
        private readonly IYeOldDependencyTest _testDependency;

        public DummyQualityCheckFail(IYeOldDependencyTest testDependency)
        {
            _testDependency = testDependency;
        }

        public string LogName => nameof(DummyQualityCheckFail);

        [QualityCheckInitialize]
        public async Task Init()
        {
            await _testDependency.FindGoldAsync(); 
        }

        [QualityCheckStep(1)]
        public async Task<StepResponse> Step1()
        {
            await _testDependency.FindGoldAsync();
            return new StepResponse() { Name = nameof(Step1) };
        }

        [QualityCheckStep(2)]
        public async Task<StepResponse> Step2()
        {
            await _testDependency.FindGoldAsync();
            return new StepResponse() { Name = nameof(Step2) };
        }

        [QualityCheckStep(3)]
        public async Task<StepResponse> Step3()
        {
            await _testDependency.FindGoldAsync();
            return new StepResponse() { Name = nameof(Step3) };
        }

        [QualityCheckStep(4)]
        public async Task<StepResponse> Step4()
        {
            await _testDependency.FindGoldAsync();
            return new StepResponse() { Name = nameof(Step4), StepPassed = false };
        }

        [QualityCheckTearDown]
        public async Task TearDown()
        {
            await _testDependency.DeleteGoldAsync();
        }

    }
}

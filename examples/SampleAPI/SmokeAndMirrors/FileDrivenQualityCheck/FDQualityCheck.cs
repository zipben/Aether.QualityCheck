using Aether.QualityChecks.Attributes;
using Aether.QualityChecks.Helpers;
using Aether.QualityChecks.Interfaces;
using Aether.QualityChecks.Models;
using SmokeAndMirrors.TestDependencies;
using System;
using System.Text;
using System.Threading.Tasks;

namespace SmokeAndMirrors.QualityChecks
{
    public class DummyFDQualityCheckPass : IQualityCheck
    {
        private readonly IYeOldDependencyTest _testDependency;

        public DummyFDQualityCheckPass(IYeOldDependencyTest testDependency)
        {
            _testDependency = testDependency;
        }

        [QualityCheckInitialize]
        public async Task Init()
        {
            
        }

        [QualityCheckStep(1)]
        public async Task Step1()
        {
            await _testDependency.FindGoldAsync();
            Step.Proceed();
        }

        [QualityCheckStep(2)]
        public async Task Step2()
        {
            await _testDependency.FindGoldAsync();
            Step.ProceedIf(() => { return true; }, successMessage: "I did it with a function");
        }

        [QualityCheckStep(3)]
        public async Task Step3()
        {
            await _testDependency.FindGoldAsync();
            Step.Proceed();
        }

        [QualityCheckStep(4)]
        public async Task Step4()
        {
            await _testDependency.FindGoldAsync();
            Step.Proceed();
        }

        [QualityCheckTearDown]
        public async Task TearDown()
        {
            await _testDependency.DeleteGoldAsync();
        }

    }
}

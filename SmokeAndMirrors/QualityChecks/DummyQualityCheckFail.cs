using Aether.QualityChecks.Attributes;
using Aether.QualityChecks.Helpers;
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
        public async Task Step1()
        {
            await _testDependency.FindGoldAsync();
            Step.Proceed();
        }

        [QualityCheckStep(2)]
        public async Task Step2()
        {
            await _testDependency.FindGoldAsync();
            Step.Proceed();
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
            Step.Fail();
        }

        [QualityCheckTearDown]
        public async Task TearDown()
        {
            await _testDependency.DeleteGoldAsync();
        }

    }
}

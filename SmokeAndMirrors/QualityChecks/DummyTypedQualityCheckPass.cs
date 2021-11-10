using Aether.QualityChecks.Attributes;
using Aether.QualityChecks.Helpers;
using Aether.QualityChecks.Interfaces;
using Aether.QualityChecks.Models;
using SmokeAndMirrors.TestDependencies;
using System.Threading.Tasks;

namespace SmokeAndMirrors.QualityChecks
{
    public class DummyTypedQualityCheckPass : IQualityCheck<DummyTypedQualityCheckPass>
    {
        private readonly IYeOldDependencyTest _testDependency;

        public DummyTypedQualityCheckPass(IYeOldDependencyTest testDependency)
        {
            _testDependency = testDependency;
        }

        public string LogName => nameof(DummyTypedQualityCheckPass);

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
        public void Step3()
        {
            Step.Warn("THIS IS A WARNING", new { fun = "testing" });
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

using Aether.QualityChecks.Attributes;
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
        public async Task<StepResponse> Step1()
        {
            await _testDependency.FindGoldAsync();
            return new StepResponse() { Name = nameof(Step1), StepPassed = true };
        }

        [QualityCheckStep(2)]
        public async Task<StepResponse> Step2()
        {
            await _testDependency.FindGoldAsync();
            return new StepResponse() { Name = nameof(Step2), StepPassed = true };
        }

        [QualityCheckStep(3)]
        public async Task<StepResponse> Step3()
        {
            await _testDependency.FindGoldAsync();
            return new StepResponse() { Name = nameof(Step3), StepPassed = true };
        }

        [QualityCheckStep(4)]
        public async Task<StepResponse> Step4()
        {
            await _testDependency.FindGoldAsync();
            return new StepResponse() { Name = nameof(Step4), StepPassed = true };
        }

        [QualityCheckTearDown]
        public async Task TearDown()
        {
            await _testDependency.DeleteGoldAsync();
        }
    }
}

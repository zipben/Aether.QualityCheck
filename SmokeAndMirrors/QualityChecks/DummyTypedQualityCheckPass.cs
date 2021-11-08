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

        public string LogName => nameof(DummyQualityCheckPass);

        public async Task<QualityCheckResponseModel> RunAsync()
        {
            QualityCheckResponseModel response = new QualityCheckResponseModel(LogName);

            response.Steps.Add(new StepResponse() { Name = "step", Message = "Did a test step", StepPassed = true });

            await _testDependency.FindGoldAsync();

            response.Steps.Add(new StepResponse() { Name = "FindGold", Message = "Gold Found", StepPassed = true });

            return response;
        }

        public async Task TearDownAsync()
        {
            await _testDependency.DeleteGoldAsync();
        }
    }
}

using Aether.QualityChecks.Attributes;
using Aether.QualityChecks.Interfaces;
using Aether.QualityChecks.Models;
using SmokeAndMirrors.TestDependencies;
using System.Threading.Tasks;

namespace SmokeAndMirrors.QualityChecks
{
    public class DummyQualityCheckPassWithDataSteps : IQualityCheck
    {
        private readonly IYeOldDependencyTest _testDependency;

        public DummyQualityCheckPassWithDataSteps(IYeOldDependencyTest testDependency)
        {
            _testDependency = testDependency;
        }

        public string LogName => nameof(DummyQualityCheckPassWithDataSteps);

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

        [QualityCheckData(1, 2, 3)]
        [QualityCheckData(4, 5, 6)]
        [QualityCheckStep(2)]
        public async Task<StepResponse> Step2(int num1, int num2, int num3)
        {
            await _testDependency.FindGoldAsync();
            return new StepResponse() { Name = nameof(Step2), StepPassed = true, Message = $"{num1} - {num2} - {num3}" };
        }


        [QualityCheckData("hi", "this is a test", "goodbye")]
        [QualityCheckStep(3)]
        public async Task<StepResponse> Step3(string greeting, string message, string farewell)
        {
            await _testDependency.FindGoldAsync();
            return new StepResponse() { Name = nameof(Step3), StepPassed = true, Message = $"{greeting} - {message} - {farewell}" };
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

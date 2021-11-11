using Aether.QualityChecks.Attributes;
using Aether.QualityChecks.Helpers;
using Aether.QualityChecks.Interfaces;
using Aether.QualityChecks.Models;
using SmokeAndMirrors.TestDependencies;
using System.Net.Http;
using System.Threading.Tasks;

namespace SmokeAndMirrors.QualityChecks
{
    public class DummyQualityCheckFail : IQualityCheck<DummyQualityCheckFail>
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
            Step.ProceedIf(true);
        }

        [QualityCheckStep(2)]
        public async Task Step2()
        {
            await _testDependency.FindGoldAsync();
            Step.Warn("its all burning down, but its fine", null, new HttpRequestException("you have bad internet"));
        }

        [QualityCheckStep(3)]
        public async Task Step3()
        {
            await _testDependency.FindGoldAsync();
            Step.ProceedIf(false, failedMessage: "And now its all borked");
        }

        [QualityCheckStep(4)]
        public async Task Step4()
        {
            await _testDependency.FindGoldAsync();
            Step.Proceed("You shouldnt see me");
        }

        [QualityCheckTearDown]
        public async Task TearDown()
        {
            await _testDependency.DeleteGoldAsync();
        }

    }
}

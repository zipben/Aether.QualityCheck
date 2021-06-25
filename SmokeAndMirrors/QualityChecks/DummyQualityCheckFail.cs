using Aether.Interfaces;
using Aether.Models;
using SmokeAndMirrors.TestDependencies;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<QualityCheckResponseModel> RunAsync()
        {
            QualityCheckResponseModel response = new QualityCheckResponseModel(LogName);

            response.Steps.Add(new StepResponse() { Name = "step", Message = "Did a test step", StepPassed = true });

            await _testDependency.FindGoldAsync();

            response.Steps.Add(new StepResponse() { Name = "FindGold", Message = "Gold Not Found", StepPassed = false });

            return response;
        }

        public async Task TearDownAsync()
        {
            await _testDependency.DeleteGoldAsync();
        }
    }
}

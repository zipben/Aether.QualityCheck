﻿using Aether.Interfaces;
using Aether.Models;
using SmokeAndMirrors.TestDependencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmokeAndMirrors.QualityChecks
{
    public class DummyQualityCheckPass : IQualityCheck<DummyTypedQualityCheckPass>
    {
        private readonly IYeOldDependencyTest _testDependency;

        public DummyQualityCheckPass(IYeOldDependencyTest testDependency)
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

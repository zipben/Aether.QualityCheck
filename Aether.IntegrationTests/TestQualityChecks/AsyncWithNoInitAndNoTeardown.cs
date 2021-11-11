﻿using Aether.QualityChecks.Attributes;
using Aether.QualityChecks.Helpers;
using Aether.QualityChecks.Helpers.Tests;
using Aether.QualityChecks.Interfaces;
using Aether.QualityChecks.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aether.QualityChecks.IntegrationTests.TestQualityChecks
{
    public class AsyncWithNoInitAndNoTeardown : IQualityCheck
    {
        private readonly IStepExecutionTester _tester;

        public AsyncWithNoInitAndNoTeardown(IStepExecutionTester tester)
        {
            _tester = tester;
        }

        [QualityCheckStep(1)]
        public async Task Step1()
        {
            _tester.Step();
            Step.Proceed();
        }

        [QualityCheckStep(2)]
        public async Task Step2()
        {
            _tester.Step();
            Step.Proceed();
        }

        [QualityCheckStep(3)]
        public async Task Step3()
        {
            _tester.Step();
            Step.Proceed();
        }

        [QualityCheckStep(4)]
        public async Task Step4()
        {
            _tester.Step();
            Step.Proceed();
        }

        public Task<QualityCheckResponseModel> RunAsync()
        {
            throw new NotImplementedException();
        }

        public Task TearDownAsync()
        {
            throw new NotImplementedException();
        }
    }
}

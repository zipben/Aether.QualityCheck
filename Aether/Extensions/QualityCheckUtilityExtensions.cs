using Aether.Models;
using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aether.Extensions
{
    public static class QualityCheckHelperExtensions
    {
        public static QualityCheckResponseModel QuickConvertToQualityCheckResponse(this bool checkPassed, string checkName, string stepName)
        {
            Guard.Against.Null(checkName, nameof(checkName));
            Guard.Against.Null(stepName, nameof(stepName));

            QualityCheckResponseModel model = new QualityCheckResponseModel(checkName);

            StepResponse step = new StepResponse();
            step.Name = stepName;
            step.StepPassed = checkPassed;

            model.Steps.Add(step);

            return model;
        }
    }
}

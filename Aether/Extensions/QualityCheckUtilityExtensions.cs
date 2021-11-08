using Aether.QualityChecks.Models;
using Ardalis.GuardClauses;

namespace Aether.QualityChecks.Extensions
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

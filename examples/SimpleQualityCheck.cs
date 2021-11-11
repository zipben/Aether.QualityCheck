using Aether.QualityChecks.Attributes;
using Aether.QualityChecks.Helpers;
using Aether.QualityChecks.Interfaces;
using Aether.QualityChecks.Models;
using SmokeAndMirrors.TestDependencies;
using System.Threading.Tasks;

namespace SmokeAndMirrors.QualityChecks
{
    public class SimpleQualityCheck : IQualityCheck
    {
        private readonly IPersonServiceClient _personServiceClient;

        public SimpleQualityCheck(IPersonServiceClient personServiceClient)
        {
            _testDependency = testDependency;
        }

        [QualityCheckStep(1)]
        public async Task AddPerson()
        {
            _personServiceClient.AddPerson("Breen Draggledon");

            Step.Proceed();
        }

        [QualityCheckStep(2)]
        public async Task SearchForNewPerson()
        {
            var person = _personClient.GetPerson("Breen Draggledon");

            Step.ProceedIf(() => { person != null });
        }

        [QualityCheckStep(3)]
        public async Task DeletePerson()
        {
            _personClient.DeletePerson("Breen Draggledon");

            Step.Proceed();
        }

        [QualityCheckStep(4)]
        public async Task VerifyPersonWasDeleted()
        {
            var person = _personClient.GetPerson("Breen Draggledon");

            Step.ProceedIf(person is null);
        }

    }
}

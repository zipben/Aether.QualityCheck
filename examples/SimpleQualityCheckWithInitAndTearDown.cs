using Aether.QualityChecks.Attributes;
using Aether.QualityChecks.Helpers;
using Aether.QualityChecks.Interfaces;
using Aether.QualityChecks.Models;
using SmokeAndMirrors.TestDependencies;
using System.Threading.Tasks;

namespace SmokeAndMirrors.QualityChecks
{
    public class SimpleQualityCheckWithInitAndTearDown : IQualityCheck
    {
        private const string PERSON_NAME = "Breen Draggledon";

        private readonly IPersonServiceClient _personServiceClient;

        public SimpleQualityCheckWithInitAndTearDown(IPersonServiceClient personServiceClient)
        {
            _testDependency = testDependency;
        }

        [QualityCheckInitialize]
        public async Task Init()
        {
            _personClient.AddPerson(PERSON_NAME);
        }

        [QualityCheckStep(1)]
        public async Task EditPersonEmployment()
        {
            _personServiceClient.EditPersonEmployment(PERSON_NAME, false);

            Step.Proceed();
        }

        [QualityCheckStep(2)]
        public async Task VerifyEmploymentStatus()
        {
            var person = _personClient.GetPerson(PERSON_NAME);

            Step.ProceedIf(person.EmploymentStatus == false));
        }

        [QualityCheckStep(3)]
        public async Task EditPersonEmploymentAgain()
        {
            _personServiceClient.EditPersonEmployment(PERSON_NAME, true);

            Step.Proceed();
        }

        [QualityCheckStep(4)]
        public async Task VerifyEmploymentStatusAgain()
        {
            var person = _personClient.GetPerson(PERSON_NAME);

            Step.ProceedIf(person.EmploymentStatus == true));
        }

        [QualityCheckTearDown]
        public async Task TearDown()
        {
            _personClient.DeletePerson(PERSON_NAME);
        }

    }
}

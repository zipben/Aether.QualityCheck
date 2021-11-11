using Aether.QualityChecks.Attributes;
using Aether.QualityChecks.Helpers;
using Aether.QualityChecks.Interfaces;
using Aether.QualityChecks.Models;
using SmokeAndMirrors.TestDependencies;
using System.Threading.Tasks;

namespace SmokeAndMirrors.QualityChecks
{
    public class SimpleQualityCheckWithDataAttributes : IQualityCheck
    {
        private readonly IPersonServiceClient _personServiceClient;

        public SimpleQualityCheckWithDataAttributes(IPersonServiceClient personServiceClient)
        {
            _testDependency = testDependency;
        }


        [QualityCheckData("Breen")]
        [QualityCheckData("Burt")]
        [QualityCheckData("Joana")]
        [QualityCheckData("Tracy")]
        [QualityCheckStep(1)]
        public async Task AddPerson(string personName)
        {
            _personServiceClient.AddPerson(personName);

            Step.Proceed($"{personName} added successfully");
        }

        [QualityCheckData("Breen")]
        [QualityCheckData("Burt")]
        [QualityCheckStep(2)]
        public async Task DeletePerson(string personName)
        {
            var person = _personClient.DeletePerson(personName);

            Step.Proceed($"{personName} deleted successfully"));
        }

        [QualityCheckStep(3)]
        public async Task CheckPersonCount()
        {
            var count = _personServiceClient.GetPersonCount();

            Step.ProceedIf(count == 2);
        }

        [QualityCheckTearDown]
        public async Task TearDown()
        {
            _personClient.DeletePerson("Joana");
            _personClient.DeletePerson("Tracy");
        }

    }
}

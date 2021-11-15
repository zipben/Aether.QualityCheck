using Aether.QualityChecks.Attributes;
using Aether.QualityChecks.Helpers;
using Aether.QualityChecks.Interfaces;
using Aether.QualityChecks.Models;
using SmokeAndMirrors.TestDependencies;
using System.Threading.Tasks;

namespace SmokeAndMirrors.QualityChecks
{
    [QualityCheckFileDriven]
    public class SimpleQualityCheck : IQualityCheck
    {
        private List<Person> _people;

        private readonly IPersonServiceClient _personServiceClient;

        public SimpleFileDriveQualityCheck(IPersonServiceClient personServiceClient)
        {
            _testDependency = testDependency;
        }

        [QualityCheckInitialize("TestFile.csv")]
        public async Task Init(byte[] fileContents)
        {
            _people = CsvHelper.LoadFromFile<Person>(fileContents);
        }

        [QualityCheckStep(1)]
        public async Task AddAllPeople()
        {

            foreach(var person in _people)
            {
                _personServiceClient.AddPerson(person.Name);
            }

            Step.Proceed();
        }

        [QualityCheckStep(2)]
        public async Task ConfirmAllPeopleLoaded()
        {
            
            List<Person> missingPeople = new List<Person>();

            foreach(var person in _people)
            {
                var personFromDb = _personClient.GetPerson(person.Name);

                if(personFromDb is null){
                    missingPeople.Add(person);
                }

            }
            

            Step.ProceedIf(!missingPeople.Any(), failedMessage: $"Unable to locate {missingPeople.Count} test people in service");
        }

        [QualityCheckTearDown]
        public async Task DeleteTestPeople()
        {
            foreach(var person in _people)
            {
                _personClient.DeletePerson(person.Name);
            }
        }
    }
}

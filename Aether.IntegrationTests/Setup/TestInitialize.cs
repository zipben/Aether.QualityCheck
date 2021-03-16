using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Aether.IntegrationTests.Setup
{
    [TestClass]
    public class IntegrationTestInitialization
    {
        //This is used to pull the environment variables from your local launchsettings.  It allows your integration tests
        //to be run locally, assuming your launchsettings has all the creds you need
        //In the circle pipeline, those same environment variables will need to be provided through kraken
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            //If this value is null, then we have not set the env variable yet, and are running locally, if it is null,
            //then we are running locally, and still need them set. 

            string useRemoteCredentials = Environment.GetEnvironmentVariable("USE_REMOTE_CREDENTIAL");

            if (useRemoteCredentials == null)
            {
                Console.WriteLine("Overiding set env variable with values from launchSettings.  This shouldn't happen in CircleCI.  " +
                                  "If you see this log in Circle, its likely followed by an IO exception, because its trying to" +
                                  "pull a value from a folder that wasnt copied into this step");

                    ForceLoadEnvironmentFromLaunchSettings("..\\..\\..\\launchSettings.json");
            }
        }
        public static void ForceLoadEnvironmentFromLaunchSettings(string filePath)
        {
            using var file = File.OpenText(filePath);

            LoadEnvironmentVariables(file);
        }

        public static void LoadEnvironmentVariables(StreamReader streamReader)
        {
            var reader = new JsonTextReader(streamReader);
            var jObject = JObject.Load(reader);

            var variables = jObject.GetValue("profiles")
                                   //select a proper profile here
                                   .SelectMany(profiles => profiles.Children())
                                   .SelectMany(profile => profile.Children<JProperty>())
                                   .Where(prop => prop.Name == "environmentVariables")
                                   .SelectMany(prop => prop.Value.Children<JProperty>())
                                   .ToList();

            foreach (var variable in variables)
            {
                Environment.SetEnvironmentVariable(variable.Name, variable.Value.ToString());
            }
        }
    }
}


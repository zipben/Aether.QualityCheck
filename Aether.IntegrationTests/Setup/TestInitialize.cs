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
            ForceLoadEnvironmentFromLaunchSettings("..\\..\\..\\launchSettings.json");
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


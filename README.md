## Aether.QualityChecks


### Overview

Aether.QualityChecks is a shared library that contains the QualityCheck framework which PBMC and DPS use to drive our automated workflow tests.  The UseQualityChecks() extension configures an endpoint at /api/qualitycheck which runs all tests that impliment the IQualityCheck interface.  Custom URLS can be passed into the setup function.  

The typed middleware allows teams to define multiple URLs routed to different sub groups of tests.

Examples of all these use cases can be found here

Dummy Quality Check with Failure [Example](https://git.rockfin.com/DataServices/Aether.QualityChecks/blob/master/SmokeAndMirrors/QualityChecks/DummyQualityCheckFail.cs)

Dummy Quality Check with all Passing Steps [Example](https://git.rockfin.com/DataServices/Aether.QualityChecks/blob/master/SmokeAndMirrors/QualityChecks/DummyQualityCheckPass.cs)

Dummy Quality Check with all Passing Steps Using Data Attributes [Example](https://git.rockfin.com/DataServices/Aether.QualityChecks/blob/master/SmokeAndMirrors/QualityChecks/DummyQualityCheckPassWithDataSteps.cs)

Dummy Typed Quality Check with all Passing Steps [Exmaple](https://git.rockfin.com/DataServices/Aether.QualityChecks/blob/master/SmokeAndMirrors/QualityChecks/DummyTypedQualityCheckPass.cs)

Register Middleware [Example](https://git.rockfin.com/DataServices/Aether.QualityChecks/blob/6b5c8030ede1b58b7e007c2e71a48a6d8f676b4d/SmokeAndMirrors/Startup.cs#L48)

Register Typed Middleware [Example](https://git.rockfin.com/DataServices/Aether.QualityChecks/blob/6b5c8030ede1b58b7e007c2e71a48a6d8f676b4d/SmokeAndMirrors/Startup.cs#L49)


Both typed and untyped middleware can be assigned to a custom URL, or left on the default which is api/qualitycheck

Calling the untyped endpoint will run all tests, 

Calling the typed endpoint will run only those tests associated with a given type.  This can be used on a test by test basis, or you can create a filter type that you use accross a set of related tests. 

### Local Development
If you are working on a feature that requires an update to Aether, you can point your nuget management service at the local Aether folder and develop against the build you are working on locally.  This can be particularly useful when you are developing a more complex tool for Aether that you have an immediate usecase for

### Deployment
When you are ready to publish your new version of Aether and make it available to our other services, increment the version number on the project.  This is what tells our circle pipeline to publish a new release.  

Please reserve full version changes for large features, or breaking changes

![enter image description here](https://git.rockfin.com/DataServices/Aether/blob/master/Aether.png)

e9c4d7e5-8ffe-4385-840e-f4db144912b5

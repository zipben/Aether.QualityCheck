## Aether.QualityChecks


### Overview

Aether.QualityChecks is a shared library that contains the QualityCheck framework which PBMC and DPS use to drive our automated workflow tests.  The UseQualityChecks() extension configures an endpoint at /api/qualitycheck which runs all tests that impliment the IQualityCheck interface.  Custom URLS can be passed into the setup function.  

The typed middleware allows teams to define multiple URLs routed to different sub groups of tests.

Both typed and untyped middleware can be assigned to a custom URL, or left on the default which is api/qualitycheck

Calling the untyped endpoint will run all tests, 

Calling the typed endpoint will run only those tests associated with a given type.  This can be used on a test by test basis, or you can create a filter type that you use accross a set of related tests. 

### Using Quality Checks
When writting a Quality Check, you should build a class that implimented the IQualityCheck interface.  These classes are registered and run as individual tests by the middleware.  The interface should not be put on a base class.

Once you have you class, you can include several different function types, and these function types are identified using the QualityCheckAttributes

QualityCheckInitialize - These functions are run first, and are intended to be used as setup methods for your test.  

QualityCheckStep - These functions take an order param which dictates the order they are executed in.
  You can use the QualityCheckData attribute to pass params into your step function.  These inputs will also be included in the StepResponse output from the middleware
  You can use the static Step class to indicate success or failure in the step.  Step.Proceed() moves to the next step, Step.Fail() fails the step and the test, and Step.Warn() is a middle state.  It allows an exception to be captured, and output, but also allows the overall test to continue
  When using the Data Attribute, the response will treat each data input as a seperate Step Response, meaning that if you have 4 different data attributes on a step, you will see 4 steps in the response, with the same name, but different DataInputs
  
QualityCheckTearDown - These functions are run at the end of your QualityCheck and are intended to be used to tear down or delete any data that is left over from the test. 

You can see examples of all these features in the example links above

### Local Development
If you are working on a feature that requires an update to Aether, you can point your nuget management service at the local Aether folder and develop against the build you are working on locally.  This can be particularly useful when you are developing a more complex tool for Aether that you have an immediate usecase for

### Deployment
When you are ready to publish your new version of Aether and make it available to our other services, increment the version number on the project.  This is what tells our circle pipeline to publish a new release.  

Please reserve full version changes for large features, or breaking changes

![enter image description here](https://git.rockfin.com/DataServices/Aether/blob/master/Aether.png)

04d39c39-8024-484b-a334-f70d3188fe29

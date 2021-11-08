## Aether

[![CircleCI](https://circleci.foc.zone/gh/DataServices/Aether.svg?style=shield)](https://[circleci.foc.zone/gh/DataServices/Aether](https://circleci.foc.zone/gh/DataServices/Aether))
[![Quality Gate Status](https://sonarqube.rockfin.com/api/project_badges/measure?project=QL.207965&metric=alert_status)](https://sonarqube.rockfin.com/dashboard?id=QL.207965)

### Overview

Aether.QualityChecks is a shared library that contains the QualityCheck framework which PBMC and DPS use to drive our automated workflow tests.  The UseQualityChecks() extension configures an endpoint at /api/qualitycheck which runs all tests that impliment the IQualityCheck interface.  Custom URLS can be passed into the setup function.  

The typed middleware allows teams to define multiple URLs routed to different sub groups of tests.

Examples of all these use cases can be found in the SmokeAndMirrors test API

### Local Development
If you are working on a feature that requires an update to Aether, you can point your nuget management service at the local Aether folder and develop against the build you are working on locally.  This can be particularly useful when you are developing a more complex tool for Aether that you have an immediate usecase for

### Deployment
When you are ready to publish your new version of Aether and make it available to our other services, increment the version number on the project.  This is what tells our circle pipeline to publish a new release.  

Please reserve full version changes for large features, or breaking changes

![enter image description here](https://git.rockfin.com/DataServices/Aether/blob/master/Aether.png)

f17534b8-c9b9-443a-90b4-9396a20ff29b

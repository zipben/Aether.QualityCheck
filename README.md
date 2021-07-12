## Aether

[![CircleCI](https://circleci.foc.zone/gh/DataServices/Aether.svg?style=shield)](https://[circleci.foc.zone/gh/DataServices/Aether](https://circleci.foc.zone/gh/DataServices/Aether))
[![Quality Gate Status](https://sonarqube.rockfin.com/api/project_badges/measure?project=QL.207965&metric=alert_status)](https://sonarqube.rockfin.com/dashboard?id=QL.207965)

### Overview

Aether is a shared library that PBMC uses to store utilities and models that are shared across multiple projects.

### Local Development
If you are working on a feature that requires an update to Aether, you can point your nuget management service at the local Aether folder and develop against the build you are working on locally.  This can be particularly useful when you are developing a more complex tool for Aether that you have an immediate usecase for

### Deployment
When you are ready to publish your new version of Aether and make it available to our other services, increment the version number on the project.  This is what tells our circle pipeline to publish a new release.  

Please reserve full version changes for large features, or breaking changes

![enter image description here](https://git.rockfin.com/DataServices/Aether/blob/master/Aether.png)
0e653165-959f-4d23-8139-c03871300f08

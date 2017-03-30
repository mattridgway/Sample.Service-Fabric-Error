# TL;DR

In VS2015 when calling a stateful service fabric service from an ASP.NET Core project using a ServiceProxy, you would get an error "Referencing project does not build using the same or an equivalent Configuration or Platform". This is now appears resolved when using VS2017, this repo shows a working version.

# Service Fabric Error

(In Visual Studio 2015 Update 3 with .NET Core preview tools installed) When trying to build and deploy a service fabric solution with a ASP.NET Core frontend (stateless service) using a ServiceProxy to call a stateful service, 
by following the [sample code](https://azure.microsoft.com/en-us/documentation/samples/service-fabric-dotnet-web-reference-app/) and [service remoting](https://azure.microsoft.com/en-us/documentation/articles/service-fabric-reliable-services-communication-remoting/) documentation, it would provide the error: 

```
"C:\Program Files (x86)\MSBuild\14.0\bin\Microsoft.Common.CurrentVersion.targets(724,5): error : The OutputPath property is not set for project 'CountingService.csproj'.  Please check to make sure that you have specified a valid combination of Configuration and Platform for this project.  Configuration='Debug'  Platform='x64'.  This error may also appear if some other project is trying to follow a project-to-project reference to this project, this project has been unloaded or is not included in the solution, and the referencing project does not build using the same or an equivalent Configuration or Platform."
```

Although in the sln file i can set the platform for the ASP.NET Core project to use x64, whenever you open VS, this then resets to AnyCPU. This can also be seen inside visual studio by opening the service fabric properties and going to configuration manager. Further if you use the configuration manager to add or edit the platform options these are not saved and will immediately reset.

## Discussed Online

* [MSDN Forum](https://social.msdn.microsoft.com/Forums/vstudio/en-US/9b733a3b-2b65-416b-b2bd-15080b51f57b/referencing-project-does-not-build-using-the-same-or-an-equivalent-configuration-or-platform-error?forum=AzureServiceFabric)
* [GitHub Issue](https://github.com/aspnet/Home/issues/1804)

## Similar problems

Having searched online, there seems to be [another post](https://social.msdn.microsoft.com/Forums/vstudio/en-US/756b4a67-1904-4a77-90ec-a4358d5a7ee7/source-code-not-uptodate-with-deployment?forum=AzureServiceFabric) with similar issues, however this didnt appear to contain an ASP.NET Core project and the fix did not work.

# VS2017

Using VS2017 to recreate the project from scratch I was able to get this working, by roughly following these steps:

* Create a new service fabric project, adding the stateful service
* Added a new class library which **MUST** target the full .NET Framework
* Updated the stateful service to implement my interface
* Added remote service listener to the stateful service
* Added ASP.NET Core Web API stateless service to the service fabric project
* Updated CPU for class library in Configuration Manager to x64
* Updated the API controller to call the ServiceProxy
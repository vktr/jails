# Jails

Jails is a framework for .NET that simplifies the task of running code in an isolated environment. It comes bundled with an AppDomain isolator which lets you sandbox code in a separate AppDomain.

Currently, it supports two ways of interacting with your sandboxed class. Either through a `dynamic` proxy, or a typed proxy. The typed proxy lets you specify an interface that acts as a strongly typed proxy.

*It is currently in early alpha and may or may not support your specific use case. Please create an issue if you feel something is wrong or missing.*

## Getting started

### Install the latest version of Jails from NuGet.

```posh
Install-Package Jails -Pre
```

### Isolate external assemblies (using a dynamic proxy)

```csharp
// Create the AppDomain isolator. By default it only gives the "Execute"
// permission.
var isolator = new AppDomainIsolator();

using(var jail = Jail.Create(isolator))
{
    // Create a real dynamic proxy which will forward any calls to the
    // internally created instance of "Some.External.Class".
    dynamic proxy = jail.Load("Some.External.Class", @"C:\Temp\ExternalAssembly.dll");

    // Invoke the method "Run" passing two arguments. Both method name and
    // argument types must match "Some.External.Class".
    string result = proxy.Run("argument1", 2);

    Console.WriteLine(result);
}
```
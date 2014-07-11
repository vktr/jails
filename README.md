# Jails

Jails is a framework for .NET that simplifies the task of running code in an isolated environment. It comes bundled with an AppDomain isolator which lets you sandbox code in a separate AppDomain.

Currently, it supports two ways of interacting with your sandboxed class. Either through a `dynamic` proxy, or a typed proxy. The typed proxy lets you specify an interface that acts as a strongly typed proxy.

*It is currently in early alpha and may or may not support your specific use case. Please create an issue if you feel something is wrong or missing.*

## Getting started

### Install the latest version of Jails from NuGet.

```posh
Install-Package Jails
```

### Isolate external assemblies (using a dynamic proxy)

```csharp
// Create the AppDomain isolator. By default it only gives the "Execute"
// permission.
var isolator = new AppDomainIsolator();

// Create an environment and register an assembly name with it.
// This will load the assembly in our jailed domain.
var environment = new DefaultEnvironment();
environment.Register(@"C:\Temp\UntrustedAssembly.dll");

using(var jail = Jail.Create(isolator, environment))
{
    // Create a real dynamic proxy which will forward any calls to the
    // internally created instance of "Some.Untrusted.Class".
    dynamic proxy = jail.Resolve("Some.Untrusted.Class");

    // Invoke the method "Run" passing two arguments. Both method name and
    // argument types must match "Some.External.Class".
    string result = proxy.Run("argument1", 2);

    Console.WriteLine(result);
}
```

## FAQ

- **Why use Jails?** Sandboxing code can sometimes be tricky. It doesn't have to be, but Jails try to provide a layer of abstraction on top of sandboxes built with AppDomains and processes.
- **Why strong naming?** Strong naming Jails is done to be able to give it full trust in the jailed domain. I know of no other way to accomplish this.

using System.IO;
using System.Security.Permissions;
using Jails.Environments.Nancy.Sandbox;
using Jails.Extensions.Roslyn;
using Jails.Isolators.AppDomain;
using NUnit.Framework;

namespace Jails.Environments.Nancy.Tests
{
    public class NancyTests
    {
        [Test]
        public void Should_Compile_And_Run_Source_In_Sandbox()
        {
            var isolator = new AppDomainIsolator();
            isolator.Options.Permissions.AddPermission(new FileIOPermission(FileIOPermissionAccess.PathDiscovery, @"C:\Windows\Microsoft.NET\Framework\v4.0.30319\"));
            isolator.Options.Permissions.AddPermission(new FileIOPermission(FileIOPermissionAccess.PathDiscovery, @"C:\Windows\Microsoft.Net\assembly\GAC_MSIL\System.Core\v4.0_4.0.0.0__b77a5c561934e089\"));
            isolator.Options.Permissions.AddPermission(new FileIOPermission(FileIOPermissionAccess.PathDiscovery, Directory.GetCurrentDirectory()));
            isolator.Options.Permissions.AddPermission(new ReflectionPermission(PermissionState.Unrestricted));

            var environment = new NancyEnvironment();

            var code = @"using System;
using Nancy;
 
namespace Foo
{
    public class IndexModule : NancyModule
    {
        public IndexModule()
        {
            Get[""/""] = x => ""Hello from Nancy"";
        }
    }
}";

            var source = new CSharpSource(code);
            environment.Register(source);

            using (var jail = Jail.Create(isolator, environment))
            {
                dynamic bar = jail.Resolve("Jails.Environments.Nancy.Sandbox.CodeDrivenNancyHost");

                var request = new TestRequest {Method = "GET", Path = "/"};
                var response = (TestResponse) bar.Execute(request);

                Assert.AreEqual(200, response.StatusCode);
            }
        }
    }
}

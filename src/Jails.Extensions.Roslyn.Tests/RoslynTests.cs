using System.Reflection;
using System.Security;
using System.Security.Permissions;
using Jails.Isolators.AppDomain;
using NUnit.Framework;

namespace Jails.Extensions.Roslyn.Tests
{
    public class RoslynTests
    {
        [Test]
        public void Should_Compile_And_Run_Source_In_Sandbox()
        {
            var isolator = new AppDomainIsolator();
            var environment = new DefaultEnvironment();

            var source = new CSharpSource("using System; using System.IO; namespace Foo { public class Bar { public int Greet() { return 42; } } }");
            environment.Register(source);

            using (var jail = Jail.Create(isolator, environment))
            {
                dynamic bar = jail.Resolve("Foo.Bar");
                int result = bar.Greet();

                Assert.AreEqual(42, result);
            }
        }
    }
}

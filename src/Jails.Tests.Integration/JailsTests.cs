using System;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using Jails.Isolators.AppDomain;
using NUnit.Framework;

namespace Jails.Tests.Integration
{
    public class JailsTests
    {
        public class TheCalculatorAssembly
        {
            [Test]
            public void Loads_And_Calculates_With_Dynamic_Proxy_In_Separate_AppDomain()
            {
                var isolator = new AppDomainIsolator();
                
                var environment = new DefaultEnvironment();
                environment.Register("Ext/Calculator.dll");

                using (var jail = Jail.Create(isolator, environment))
                {
                    dynamic calculator = jail.Resolve("Calculator.SimpleCalculator");
                    int result = calculator.Sum(new[] {1, 2, 3, 4, 5});
                    calculator.Name = "simple calculator";

                    Assert.AreEqual(15, result);
                    Assert.AreEqual("simple calculator", calculator.Name);
                }

                Assert.IsFalse(AppDomain.CurrentDomain.GetAssemblies().Any(asm => asm.GetName().Name == "Calculator"));
            }
        }
    }
}

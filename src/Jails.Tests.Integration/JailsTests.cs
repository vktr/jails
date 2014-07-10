using System;
using System.Linq;
using Jails.Isolators.AppDomain;
using NUnit.Framework;

namespace Jails.Tests.Integration
{
    public class JailsTests
    {
        public class TheCalculatorAssembly
        {
            interface ICalculator
            {
                int Multiply(int x, int y);
            }

            [Test]
            public void Loads_And_Calculates_With_Dynamic_Proxy_In_Separate_AppDomain()
            {
                var provider = new AppDomainIsolator();

                using (var jail = Jail.Create(provider))
                {
                    dynamic calculator = jail.Load("Calculator.SimpleCalculator", "Ext/Calculator.dll");
                    int result = calculator.Sum(new[] {1, 2, 3, 4, 5});

                    Assert.AreEqual(15, result);
                }

                Assert.IsFalse(AppDomain.CurrentDomain.GetAssemblies().Any(asm => asm.GetName().Name == "Calculator"));
            }

            [Test]
            public void Loads_And_Calculates_With_Typed_Proxy_In_Separate_AppDomain()
            {
                var provider = new AppDomainIsolator();

                using (var jail = Jail.Create(provider))
                {
                    var calculator = jail.Load<ICalculator>("Calculator.SimpleCalculator", "Ext/Calculator.dll");
                    var result = calculator.Multiply(6, 6);

                    Assert.AreEqual(36, result);
                }

                Assert.IsFalse(AppDomain.CurrentDomain.GetAssemblies().Any(asm => asm.GetName().Name == "Calculator"));
            }
        }
    }
}

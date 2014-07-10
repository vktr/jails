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
                string Name { get; set; }

                int Multiply(int x, int y);
            }

            [Test]
            public void Loads_And_Calculates_With_Dynamic_Proxy_In_Separate_AppDomain()
            {
                var isolator = new AppDomainIsolator();
                
                using (var jail = Jail.Create(isolator))
                {
                    dynamic calculator = jail.Load("Calculator.SimpleCalculator", "Ext/Calculator.dll");
                    int result = calculator.Sum(new[] {1, 2, 3, 4, 5});
                    calculator.Name = "simple calculator";

                    Assert.AreEqual(15, result);
                    Assert.AreEqual("simple calculator", calculator.Name);
                }

                Assert.IsFalse(AppDomain.CurrentDomain.GetAssemblies().Any(asm => asm.GetName().Name == "Calculator"));
            }

            [Test]
            public void Loads_And_Calculates_With_Typed_Proxy_In_Separate_AppDomain()
            {
                var isolator = new AppDomainIsolator();

                using (var jail = Jail.Create(isolator))
                {
                    var calculator = jail.Load<ICalculator>("Calculator.SimpleCalculator", "Ext/Calculator.dll");
                    var result = calculator.Multiply(6, 6);
                    calculator.Name = "simple calculator";

                    Assert.AreEqual(36, result);
                    Assert.AreEqual("simple calculator", calculator.Name);
                }

                Assert.IsFalse(AppDomain.CurrentDomain.GetAssemblies().Any(asm => asm.GetName().Name == "Calculator"));
            }
        }
    }
}

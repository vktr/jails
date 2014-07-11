using System;
using Jails.Tests.Fixtures;
using NSubstitute;
using NUnit.Framework;

namespace Jails.Tests.Unit
{
    public class DynamicTargetProxyTests
    {
        public class TheConstructor
        {
            [Test]
            public void Should_Throw_Exception_If_Target_Is_Null()
            {
                // Given, When
                var exception = Assert.Throws<ArgumentNullException>(() => new DynamicTargetProxy(null));

                // Then
                Assert.AreEqual("target", exception.ParamName);
            }
        }

        public class TheDynamicMethodBinding
        {
            [Test]
            public void Should_Forward_Method_Calls_To_Target()
            {
                // Given
                var fixture = new DynamicTargetProxyFixture();
                dynamic proxy = fixture.CreateProxy();

                // When
                proxy.Test();

                // Then
                fixture.Target.Received(1).Invoke("Test");
            }
        }

        public class TheDynamicPropertyBinding
        {
            [Test]
            public void Should_Forward_Property_Assignment_To_Target()
            {
                // Given
                var fixture = new DynamicTargetProxyFixture();
                dynamic proxy = fixture.CreateProxy();

                // When
                proxy.Test = 1;

                // Then
                fixture.Target.Received(1).SetProperty("Test", 1);
            }

            [Test]
            public void Should_Forward_Property_Retrieval_To_Target()
            {
                // Given
                var fixture = new DynamicTargetProxyFixture();
                dynamic proxy = fixture.CreateProxy();

                // When
                var result = proxy.Test;

                // Then
                fixture.Target.Received(1).GetProperty("Test");
            }
        }
    }
}

using System;
using Jails.Tests.Fixtures;
using NSubstitute;
using NUnit.Framework;

namespace Jails.Tests.Unit
{
    public class JailTests
    {
        public class TheConstructor
        {
            [Test]
            public void Should_Throw_Exception_If_Host_Is_Null()
            {
                // Given, When
                var exception = Assert.Throws<ArgumentNullException>(() => new Jail(null));

                // Then
                Assert.AreEqual("host", exception.ParamName);
            }
        }

        public class TheResolveMethod
        {
            [Test]
            public void Should_Throw_Exception_If_TypeName_Is_Null()
            {
                // Given
                var fixture = new JailFixture();
                var jail = fixture.CreateJail();

                // When
                var exception = Assert.Throws<ArgumentNullException>(() => jail.Resolve(null));

                // Then
                Assert.AreEqual("typeName", exception.ParamName);
            }

            [Test]
            public void Should_Call_Host()
            {
                // Given
                var fixture = new JailFixture();
                var jail = fixture.CreateJail();

                // When
                jail.Resolve("Some.Type");

                // Then
                fixture.Host.Received(1).ResolveTarget("Some.Type");
            }
        }
    }
}

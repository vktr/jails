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
            public void Should_Throw_Exception_If_Isolator_Is_Null()
            {
                // Given, When
                var exception = Assert.Throws<ArgumentNullException>(() => new Jail(null));

                // Then
                Assert.AreEqual("isolator", exception.ParamName);
            }
        }

        public class TheLoadMethod
        {
            [Test]
            public void Should_Throw_Exception_If_TypeName_Is_Null()
            {
                // Given
                var fixture = new JailFixture();
                var jail = fixture.CreateJail();

                // When
                var exception = Assert.Throws<ArgumentNullException>(() => jail.Load(null, null));

                // Then
                Assert.AreEqual("typeName", exception.ParamName);
            }

            [Test]
            public void Should_Throw_Exception_If_AssemblyFile_Is_Null()
            {
                // Given
                var fixture = new JailFixture();
                var jail = fixture.CreateJail();

                // When
                var exception = Assert.Throws<ArgumentNullException>(() => jail.Load("Some.Type", null));

                // Then
                Assert.AreEqual("assemblyFile", exception.ParamName);
            }

            [Test]
            public void Should_Call_Isolator()
            {
                // Given
                var fixture = new JailFixture();
                var jail = fixture.CreateJail();

                // When
                jail.Load("Some.Type", "Assembly.dll");

                // Then
                fixture.Isolator.Received(1).CreateDynamicProxy("Some.Type", "Assembly.dll");
            }
        }
    }
}

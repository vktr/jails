using NSubstitute;

namespace Jails.Tests.Fixtures
{
    internal sealed class JailFixture
    {
        public JailFixture()
        {
            Isolator = Substitute.For<IIsolator>();
        }

        public IIsolator Isolator { get; set; }

        public Jail CreateJail()
        {
            return new Jail(Isolator);
        }
    }
}

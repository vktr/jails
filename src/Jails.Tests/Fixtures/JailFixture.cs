using NSubstitute;

namespace Jails.Tests.Fixtures
{
    internal sealed class JailFixture
    {
        public JailFixture()
        {
            Host = Substitute.For<IHost>();
        }

        public IHost Host { get; set; }

        public Jail CreateJail()
        {
            return new Jail(Host);
        }
    }
}

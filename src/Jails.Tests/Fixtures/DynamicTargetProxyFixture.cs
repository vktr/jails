using NSubstitute;

namespace Jails.Tests.Fixtures
{
    internal sealed class DynamicTargetProxyFixture
    {
        public DynamicTargetProxyFixture()
        {
            Target = Substitute.For<IInvocationTarget>();
        }

        public IInvocationTarget Target { get; set; }

        public DynamicTargetProxy CreateProxy()
        {
            return new DynamicTargetProxy(Target);
        }
    }
}

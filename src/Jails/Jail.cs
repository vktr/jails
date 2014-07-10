using System;

namespace Jails
{
    public sealed class Jail : IJail
    {
        private readonly IHost _host;

        internal Jail(IHost host)
        {
            if (host == null) throw new ArgumentNullException("host");
            _host = host;
        }

        public object Resolve(string typeName)
        {
            var target = _host.ResolveTarget(typeName);
            return new DynamicTargetProxy(target);
        }

        public T Resolve<T>(string typeName) where T : class
        {
            var target = _host.ResolveTarget(typeName);
            var proxy = new TypedTargetProxy(target, typeof (T));

            return proxy.GetTransparentProxy() as T;
        }

        public void Dispose()
        {
        }

        public static IJail Create(IIsolator isolator, IEnvironment environment = null)
        {
            if (isolator == null) throw new ArgumentNullException("isolator");

            var host = isolator.Build(environment ?? new DefaultEnvironment());

            // Register things from environment

            return new Jail(host);
        }
    }
}

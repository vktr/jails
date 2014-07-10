using System;

namespace Jails
{
    public sealed class Jail : IJail
    {
        private readonly IIsolator _isolator;

        private Jail(IIsolator isolator)
        {
            if (isolator == null) throw new ArgumentNullException("isolator");
            _isolator = isolator;
        }

        public object Load(string typeName, string assemblyFile)
        {
            return _isolator.CreateDynamicProxy(typeName, assemblyFile);
        }

        public T Load<T>(string typeName, string assemblyFile) where T : class
        {
            return _isolator.CreateTypedProxy<T>(typeName, assemblyFile);
        }

        public void Dispose()
        {
        }

        public static IJail Create(IIsolator isolator)
        {
            if (isolator == null) throw new ArgumentNullException("isolator");
            return new Jail(isolator);
        }
    }
}

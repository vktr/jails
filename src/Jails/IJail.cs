using System;

namespace Jails
{
    public interface IJail : IDisposable
    {
        object Resolve(string typeName, string assemblyName);

        T Resolve<T>(string typeName, string assemblyName) where T : class;
    }
}

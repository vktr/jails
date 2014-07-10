using System;

namespace Jails
{
    public interface IJail : IDisposable
    {
        object Resolve(string typeName);

        T Resolve<T>(string typeName) where T : class;
    }
}

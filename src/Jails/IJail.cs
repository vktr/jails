using System;

namespace Jails
{
    public interface IJail : IDisposable
    {
        object Load(string typeName, string assemblyFile);

        T Load<T>(string typeName, string assemblyFile) where T : class;
    }
}

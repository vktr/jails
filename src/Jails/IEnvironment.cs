using System.Collections.Generic;
using System.Reflection;

namespace Jails
{
    public interface IEnvironment
    {
        void Register(AssemblyName assemblyName);
        void Register(InMemoryAssembly assembly);

        IEnumerable<object> GetRegistrations();
    }
}

using System.Collections.Generic;
using System.Reflection;

namespace Jails
{
    public class DefaultEnvironment : IEnvironment
    {
        private readonly IList<object> _registrations;

        public DefaultEnvironment()
        {
            _registrations = new List<object>();
        }

        public void Register(AssemblyName assemblyName)
        {
            _registrations.Add(assemblyName);
        }

        public void Register(InMemoryAssembly assembly)
        {
            _registrations.Add(assembly);
        }

        IEnumerable<object> IEnvironment.GetRegistrations()
        {
            return _registrations;
        }
    }
}
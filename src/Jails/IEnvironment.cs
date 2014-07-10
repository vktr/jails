using System.Collections.Generic;
using System.Reflection;

namespace Jails
{
    public interface IEnvironment
    {
        IEnumerable<AssemblyName> GetAssemblyNames();
    }

    public class DefaultEnvironment : IEnvironment
    {
        private readonly IList<AssemblyName> _registrations;

        public DefaultEnvironment()
        {
            _registrations = new List<AssemblyName>();
        }

        public void Register(AssemblyName assemblyName)
        {
            _registrations.Add(assemblyName);
        }

        IEnumerable<AssemblyName> IEnvironment.GetAssemblyNames()
        {
            return _registrations;
        }
    }
}

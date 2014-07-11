using System.Reflection;

namespace Jails
{
    public static class EnvironmentExtensions
    {
        public static void Register(this IEnvironment environment, string assemblyFile)
        {
            environment.Register(AssemblyName.GetAssemblyName(assemblyFile));
        }
    }
}

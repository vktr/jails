using System;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Security.Permissions;

namespace Jails.Isolators.AppDomain
{
    internal sealed class AppDomainHost : MarshalByRefObject, IHost
    {
        public AppDomainHost()
        {
            System.AppDomain.CurrentDomain.AssemblyResolve += OnAssemblyResolve;
        }

        public void AddAssembly(AssemblyName assemblyName)
        {
            new FileIOPermission(PermissionState.Unrestricted).Assert();
            Assembly.Load(assemblyName);
            CodeAccessPermission.RevertAll();
        }

        private Assembly OnAssemblyResolve(object sender, ResolveEventArgs args)
        {
            var assembly = (from asm in System.AppDomain.CurrentDomain.GetAssemblies()
                where asm.FullName == args.Name
                select asm).FirstOrDefault();

            return assembly;
        }

        public IInvocationTarget ResolveTarget(string typeName, string assemblyName)
        {
            return new InvocationTarget(typeName, assemblyName);
        }
    }
}

using System;
using System.IO;
using System.Reflection;
using System.Security;
using System.Security.Permissions;

namespace Jails.Isolators.AppDomain
{
    public class AppDomainIsolator : IIsolator
    {
        public AppDomainIsolator()
        {
            Permissions = new PermissionSet(PermissionState.None);
            Permissions.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
        }

        public PermissionSet Permissions { get; set; }

        object IIsolator.CreateDynamicProxy(string typeName, string assemblyFile)
        {
            var target = CreateIsolatedTargetHost(typeName, assemblyFile);
            return new DynamicTargetProxy(target);
        }

        T IIsolator.CreateTypedProxy<T>(string typeName, string assemblyFile)
        {
            var target = CreateIsolatedTargetHost(typeName, assemblyFile);
            var proxy = new TypedTargetProxy(target, typeof (T));

            return (T) proxy.GetTransparentProxy();
        }

        private IIsolatedTargetHost CreateIsolatedTargetHost(string typeName, string assemblyFile)
        {
            var setup = new AppDomainSetup
            {
                ApplicationBase = Path.GetTempPath()
            };

            var permissionSet = new PermissionSet(Permissions);

            // Make sure the new domain can read and discover the assembly file
            permissionSet.AddPermission(
                new FileIOPermission(FileIOPermissionAccess.PathDiscovery
                                     | FileIOPermissionAccess.Read,
                    Path.GetFullPath(assemblyFile)));

            var domain = System.AppDomain.CreateDomain(string.Format("Proxy:{0}", typeName), null, setup, permissionSet);
            return (IsolatedTargetHost) Activator.CreateInstanceFrom(
                domain,
                typeof (IsolatedTargetHost).Assembly.CodeBase,
                typeof (IsolatedTargetHost).FullName,
                false,
                BindingFlags.Default,
                null,
                new object[] {typeName, assemblyFile},
                null,
                null).Unwrap();
        }
    }
}

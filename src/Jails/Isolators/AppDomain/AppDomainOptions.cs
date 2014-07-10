using System;
using System.IO;
using System.Security;
using System.Security.Permissions;

namespace Jails.Isolators.AppDomain
{
    public sealed class AppDomainOptions
    {
        public AppDomainOptions()
        {
            Setup = new AppDomainSetup
            {
                ApplicationBase = Path.GetTempPath()
            };

            Permissions = new PermissionSet(PermissionState.None);
            Permissions.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
        }

        public string DomainName { get; set; }

        public AppDomainSetup Setup { get; private set; }

        public PermissionSet Permissions { get; set; }

        public static AppDomainOptions GetDefault()
        {
            return new AppDomainOptions();
        }
    }
}

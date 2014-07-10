using System;
using System.IO;
using System.Reflection;

namespace Jails.Isolators.AppDomain
{
    public class IsolatedTargetHost : MarshalByRefObject, IIsolatedTargetHost
    {
        private readonly string _typeName;
        private readonly string _assemblyFile;
        private readonly Lazy<object> _target; 

        public IsolatedTargetHost(string typeName, string assemblyFile)
        {
            if (typeName == null) throw new ArgumentNullException("typeName");
            if (assemblyFile == null) throw new ArgumentNullException("assemblyFile");
            _typeName = typeName;
            _assemblyFile = assemblyFile;
            _target = new Lazy<object>(LoadTarget);
        }

        private object LoadTarget()
        {
            var assembly = Assembly.LoadFile(Path.GetFullPath(_assemblyFile));
            var type = assembly.GetType(_typeName);

            return Activator.CreateInstance(type);
        }

        public object Invoke(string methodName, params object[] arguments)
        {
            var target = _target.Value;
            var method = target.GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public);

            return method.Invoke(target, arguments);
        }
    }
}

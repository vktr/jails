using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Permissions;

namespace Jails.Isolators.AppDomain
{
    public class InvocationTarget : MarshalByRefObject, IInvocationTarget
    {
        private readonly string _typeName;
        private readonly string _assemblyFile;
        private readonly Lazy<object> _target; 

        public InvocationTarget(string typeName, string assemblyFile)
        {
            if (typeName == null) throw new ArgumentNullException("typeName");
            if (assemblyFile == null) throw new ArgumentNullException("assemblyFile");
            _typeName = typeName;
            _assemblyFile = assemblyFile;
            _target = new Lazy<object>(LoadTarget);
        }

        [FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
        private object LoadTarget()
        {
            var assembly = Assembly.LoadFile(Path.GetFullPath(_assemblyFile));
            var type = assembly.GetType(_typeName);

            return Activator.CreateInstance(type);
        }

        public object GetProperty(string propertyName)
        {
            var target = _target.Value;
            var prop = target.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public);

            return prop.GetValue(target);
        }

        public void SetProperty(string propertyName, object value)
        {
            var target = _target.Value;
            var prop = target.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public);

            prop.SetValue(target, value);
        }

        public object Invoke(string methodName, params object[] arguments)
        {
            var target = _target.Value;
            var argumentTypes = arguments == null ? Type.EmptyTypes : arguments.Select(a => a.GetType()).ToArray();
            var method = target
                .GetType()
                .GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public, null, argumentTypes, null);

            return method.Invoke(target, arguments);
        }
    }
}

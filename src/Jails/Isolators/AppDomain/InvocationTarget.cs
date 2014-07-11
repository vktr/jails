using System;
using System.Linq;
using System.Reflection;

namespace Jails.Isolators.AppDomain
{
    public class InvocationTarget : MarshalByRefObject, IInvocationTarget
    {
        private readonly string _typeName;
        private readonly Lazy<object> _target; 

        public InvocationTarget(string typeName)
        {
            if (typeName == null) throw new ArgumentNullException("typeName");

            _typeName = typeName;
            _target = new Lazy<object>(LoadTarget);
        }

        private object LoadTarget()
        {
            var type = (from asm in System.AppDomain.CurrentDomain.GetAssemblies()
                let t = asm.GetType(_typeName)
                where t != null
                select t).Single();

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

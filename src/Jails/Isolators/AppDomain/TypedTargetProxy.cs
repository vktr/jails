using System;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;

namespace Jails.Isolators.AppDomain
{
    public class TypedTargetProxy : RealProxy
    {
        private readonly IIsolatedTargetHost _target;

        public TypedTargetProxy(IIsolatedTargetHost target, Type proxyType)
            : base(proxyType)
        {
            if (target == null) throw new ArgumentNullException("target");
            _target = target;
        }

        public override IMessage Invoke(IMessage msg)
        {
            var methodCall = msg as IMethodCallMessage;
            return methodCall != null ? Handle(methodCall) : null;
        }

        private IMessage Handle(IMethodCallMessage msg)
        {
            try
            {
                var result = _target.Invoke(msg.MethodName, msg.InArgs);
                return new ReturnMessage(result, null, 0, msg.LogicalCallContext, msg);
            }
            catch (TargetInvocationException invocationException)
            {
                var exception = invocationException.InnerException;
                return new ReturnMessage(exception, msg);
            }
        }
    }
}

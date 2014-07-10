using System;
using System.Dynamic;

namespace Jails
{
    public class DynamicTargetProxy : DynamicObject
    {
        private readonly IInvocationTarget _target;

        public DynamicTargetProxy(IInvocationTarget target)
        {
            _target = target;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = null;

            try
            {
                result = _target.GetProperty(binder.Name);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            try
            {
                _target.SetProperty(binder.Name, value);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            result = null;

            try
            {
                result = _target.Invoke(binder.Name, args);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
namespace Jails.Isolators.AppDomain
{
    public interface IIsolatedTargetHost
    {
        object Invoke(string methodName, params object[] arguments);
    }
}
namespace Jails.Isolators.AppDomain
{
    public interface IIsolatedTargetHost
    {
        object GetProperty(string propertyName);

        void SetProperty(string propertyName, object value);

        object Invoke(string methodName, params object[] arguments);
    }
}
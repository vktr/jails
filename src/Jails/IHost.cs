namespace Jails
{
    public interface IHost
    {
        IInvocationTarget ResolveTarget(string typeName);
    }
}

namespace Jails
{
    public interface IIsolator
    {
        IHost Build(IEnvironment environment);
    }
}

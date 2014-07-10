namespace Jails
{
    public interface IIsolator
    {
        object CreateDynamicProxy(string typeName, string assemblyFile);
        T CreateTypedProxy<T>(string typeName, string assemblyFile) where T : class;
    }
}

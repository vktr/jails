using Jails.Environments.Nancy.Sandbox;
using Nancy;

namespace Jails.Environments.Nancy
{
    public class NancyEnvironment : DefaultEnvironment
    {
        public NancyEnvironment()
        {
            Register(typeof (INancyEngine).Assembly.GetName());
            Register(typeof (CodeDrivenNancyHost).Assembly.GetName());
        }
    }
}

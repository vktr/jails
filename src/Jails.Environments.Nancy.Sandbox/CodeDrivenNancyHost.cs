using System;
using Nancy;
using Nancy.Bootstrapper;

namespace Jails.Environments.Nancy.Sandbox
{
    public class CodeDrivenNancyHost : IDisposable
    {
        private readonly INancyBootstrapper _bootstrapper;
        private readonly INancyEngine _engine;

        public CodeDrivenNancyHost()
        {
            this._bootstrapper = NancyBootstrapperLocator.Bootstrapper;
            this._bootstrapper.Initialise();
            this._engine = this._bootstrapper.GetEngine();
        }

        public void Dispose()
        {
            this._bootstrapper.Dispose();
        }

        public TestResponse Execute(TestRequest request)
        {
            var actualRequest =
                new Request(request.Method, request.Path, "http");

            using (var nancyContext = this._engine.HandleRequest(actualRequest))
            {
                return new TestResponse { StatusCode = (int)nancyContext.Response.StatusCode };
            }
        }
    }

    [Serializable]
    public class TestRequest
    {
        public string Method { get; set; }

        public string Path { get; set; }
    }

    [Serializable]
    public class TestResponse
    {
        public int StatusCode { get; set; }
    }
}


using FleetManagement.Api;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.IO;
using System.Net.Http;
using System.Reflection;

namespace FleetManagement.Test.ControllerTests
{
    public class TestClientProvider : IDisposable
    {
        private TestServer server;
        public HttpClient Client { get; private set; }

        public TestClientProvider()
        {
            var appsettingsPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var server = new TestServer(WebHost.CreateDefaultBuilder()
                    .UseContentRoot(appsettingsPath)
                    .UseStartup<Startup>());

            Client = server.CreateClient();
        }

        public void Dispose()
        {
            server?.Dispose();
            Client?.Dispose();
        }
    }
}

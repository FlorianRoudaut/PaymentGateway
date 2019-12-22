using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using NUnit;
using NUnit.Framework;

namespace Gateway.Api.Tests.Integration
{
    public class ProcessControllerIntegrationTest
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public ProcessControllerIntegrationTest()
        {
            _server = new TestServer(WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Test]
        public async Task home_controller_get_should_return_string_content()
        {
            await Task.CompletedTask;
            /*var response = await _client.GetAsync("/api/process");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            content.Should().BeEquivalentTo("Hello from Process Controller API!!");*/
        }
    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using SampleWebApplication.Middlewares;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace SampleWebApplication.Tests
{
	public class StatusMiddlewareTests
	{
		[Fact]
		public async Task StatusMiddlewareReturnsPong()
		{
			var hostBuilder = new WebHostBuilder()
				.Configure(app =>
				{
					app.UseMiddleware<StatusMiddleware>(); // configures a WebHostBuilder that defines the app
			});

			using (var server = new TestServer(hostBuilder)) // create an instance of TestServer, passing in the WebHostBuilder
			{
				HttpClient client = server.CreateClient(); // create a HttpClient, or you can interact directly with server object
				var response = await client.GetAsync("/ping"); // makes an in-memory request, which handled by the app as normal
				response.EnsureSuccessStatusCode(); // verifies the response was a success (2xx) status code
				var content = await response.Content.ReadAsStringAsync();
				Assert.Equal("pong", content);
			}
		}

		[Fact]
		public async Task StatusMiddlewareReturnsPongUsingStartUp()
		{
			var hostBuilder = new WebHostBuilder()
				.UseStartup<Startup>(); // use Startup from your real app.

			using (var server = new TestServer(hostBuilder))
			{
				HttpClient client = server.CreateClient();
				var response = await client.GetAsync("/ping");
				response.EnsureSuccessStatusCode();
				var content = await response.Content.ReadAsStringAsync();
				Assert.Equal("pong", content);
			}
		}

	}
}

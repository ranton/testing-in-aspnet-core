using Microsoft.AspNetCore.Http;
using SampleWebApplication.Middlewares;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SampleWebApplication.Tests
{
    public class CustomMiddlewareTests
    {
        [Fact]
        public async Task ForNonMatchingRequest_CallsNextDelegate()
        {
            var context = new DefaultHttpContext();
            context.Request.Path = "/somethingelse"; // creates a DefaultHttpContext and sets the path for the request

            var wasExecuted = false;  // this will track whether RequestDelegate was executed
            RequestDelegate next = (ctx) =>
            {
                wasExecuted = true;
                return Task.CompletedTask;
            };

            var middleware = new StatusMiddleware(next: next); // creates an instance of the middleware passing the the next Request Delegate
            await middleware.Invoke(context); // should invoke the RequestDelegate
            Assert.True(wasExecuted); // verification
        }

		[Fact]
		public async Task ReturnsPongBodyContent()
		{
			var bodyStream = new MemoryStream();
			var context = new DefaultHttpContext();
			context.Response.Body = bodyStream;
			context.Request.Path = "/ping"; // creates a DefaultHttpContext and initializes the body with a MemoryStream to capture the response

			RequestDelegate next = (ctx) => Task.CompletedTask;
			var middleware = new StatusMiddleware(next: next); // creates an instance of the middleware and passes in a simple RequestDelegate

			await middleware.Invoke(context); // invokes the middleware

			string response;
			bodyStream.Seek(0, SeekOrigin.Begin);
			using (var stringReader = new StreamReader(bodyStream))
			{
				response = await stringReader.ReadToEndAsync();
			} // rewins the MemoryStream and reads the response body into a string

			Assert.Equal("pong", response); // verify the response value if correct
			Assert.Equal("text/plain", context.Response.ContentType); // check the content-type
			Assert.Equal(200, context.Response.StatusCode); // check status code
		}


	}
}

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWebApplication.Middlewares
{
	public class StatusMiddleware
	{
		private readonly RequestDelegate _next; // RequestDelegate representing the rest of the middleware pipeline
		public StatusMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context) // Called when the middleware is executed
		{
			if (context.Request.Path.StartsWithSegments("/ping"))
			{
				context.Response.ContentType = "text/plain";
				await context.Response.WriteAsync("pong");
				return; // if the path starts with "/ping", a "pong" response is returned
			}
			await _next(context); // otherwise the next middleware in the pipeline is invoked
		}
	}

	public static class StatusMiddlewareExtensions
	{
		public static IApplicationBuilder UseStatusMiddleware(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<StatusMiddleware>();
		}
	}

}

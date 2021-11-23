using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace HelloMVCWorld.CustomMiddleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class MyMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;

        public MyMiddleware(RequestDelegate next  , ILoggerFactory logFactory)
        {
            _logger = logFactory.CreateLogger("MyMiddleware");
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            _logger.LogInformation("Request Start " + httpContext.Request.Path);

            await _next(httpContext); // calling next middleware

            _logger.LogInformation("Request Executed " + httpContext.Request.Path);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MyMiddlewareExtensions
    {
        public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MyMiddleware>();
        }
    }
}

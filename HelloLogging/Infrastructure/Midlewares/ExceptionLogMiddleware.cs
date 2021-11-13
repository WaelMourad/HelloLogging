
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace HelloLogging.Infrastructure.Midlewares
{
    public class ExceptionLogMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionLogMiddleware> logger;

        public ExceptionLogMiddleware(RequestDelegate next, ILogger<ExceptionLogMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}

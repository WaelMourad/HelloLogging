using HelloLogging.Infrastructure.Midlewares;
using Microsoft.AspNetCore.Builder;

namespace HelloLogging.IOC.ExceptionLog
{
    public static class ExceptionLogStartupHelper
    {
        public static IApplicationBuilder UseExceptionLog(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionLogMiddleware>();
        }
    }
}

using HelloLogging.Constants;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace HelloLogging.IOC.Swagger
{
    public static class SwaggerStartupHelper
    {
        public static IServiceCollection AddSwaggerGen(this IServiceCollection services)
        {
            return services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(ApiConstants.ApiName, new OpenApiInfo
                {
                    Title = ApiConstants.ApiTitle,
                    Version = ApiConstants.ApiVersion,
                    Description = ApiConstants.ApiDescription
                });             
            });
        }

        public static IApplicationBuilder UseSwaggerUI(this IApplicationBuilder app)
        {
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "Swagger/{documentName}.json";
            });

            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "logging";
                c.SwaggerEndpoint($"../Swagger/{ApiConstants.ApiName}.json", $"{ApiConstants.ApiTitle} {ApiConstants.ApiVersion}");
            });
            return app;
        }


    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace netCryptWebApi
{
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1.0", new Info 
                { 
                    Title = "netCoreCrypt v0.5", 
                    Version = "v0.5",
                    Description = "A Web API developed to Encrypt password, if has user token to consume methods.",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "Lucas Monteiro", Email = "lmsupport@outlook.com", Url = "https://twitter.com/spboyer" }
                });
 
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
            });
 
            return services;
        }
 
        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "Versioned API v0.5");
 
                c.DocExpansion("none");
            });
 
            return app;
        }
    }
}
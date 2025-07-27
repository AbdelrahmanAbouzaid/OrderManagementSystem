using OrderManagementSystem.API.Middlewares;
using Persistence;
using System.Threading.Tasks;
using Services;

namespace OrderManagementSystem.API.Extentions
{
    public static class Extentions
    {
        public static IServiceCollection RegisterAllServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add services to the container.

            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddInfrastructureServices(configuration);
            services.AddApplicationServices(configuration);

            return services;
        }   



        public static WebApplication UseAllMiddlewares(this WebApplication app)
        {
            // Configure the HTTP request pipeline.
            app.UseMiddleware<ErrorHandlingMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            return app;
        }
    }
}

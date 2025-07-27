
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data;

namespace Persistence
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDbInitializer, DbInitializer>();
            // Register DbContext
            services.AddDbContext<OrderManagementDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
          
            return services;
        }
    }
}

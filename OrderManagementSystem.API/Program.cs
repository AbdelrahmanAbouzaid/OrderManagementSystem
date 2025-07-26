
using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.API.Extentions;
using Persistence.Data;

namespace OrderManagementSystem.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.RegisterAllServices(builder.Configuration);

            var app = builder.Build();

            app.UseAllMiddlewares();

            app.Run();
        }
    }
}

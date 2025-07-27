
using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.API.Extentions;
using Persistence.Data;
using Shared;

namespace OrderManagementSystem.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.RegisterAllServices(builder.Configuration);
            builder.Services.Configure<MailOptions>(builder.Configuration.GetSection("MailOptions"));
            

            var app = builder.Build();

            app.UseAllMiddlewares();

            app.Run();
        }
    }
}

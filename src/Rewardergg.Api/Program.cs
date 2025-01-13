using MediatR;
using Microsoft.EntityFrameworkCore;
using Rewardergg.Application.Commands;
using Rewardergg.Infrastructure.Extensions;
using Rewardergg.Infrastructure.Persitence;
using System.Reflection;

namespace Rewardergg.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
                       
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddInfrastructureServices(builder.Configuration);

            // Database registration
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add services to the container.         
            builder.Services.AddMediatR(cfg =>
                                            cfg.RegisterServicesFromAssemblies(
                                                Assembly.GetExecutingAssembly(),                      
                                                typeof(CreateUserCommandHandler).Assembly             
                                            )
);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}

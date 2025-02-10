using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Rewardergg.Api.Extensions;
using Rewardergg.Application.Configurations;
using Rewardergg.Application.Interfaces;
using Rewardergg.Application.Models;
using Rewardergg.Application.Validators;
using Rewardergg.Infrastructure.Extensions;
using Rewardergg.Infrastructure.Persitence;
using Rewardergg.Infrastructure.Services;
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

            builder.Services.AddHttpClient<IAuthService, AuthService>(client =>
            {
                var baseUrl = builder.Configuration["StartggSettings:BaseUrl"] ?? throw new MissingFieldException("Missing startgg BaseUrl property");
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            // Add FluentValidation Validators
            builder.Services.AddSingleton<IValidator<StartggSettings>, StartggSettingsValidator>();
            builder.Services.AddSingleton<IValidator<JwtSettings>, JwtSettingsValidator>();

            // Bind Configuration & Validate on Startup
            builder.Services.AddOptions<StartggSettings>().BindConfiguration(nameof(StartggSettings))
                    .ValidateFluentValidation().ValidateOnStart();
            builder.Services.AddOptions<JwtSettings>().BindConfiguration(nameof(JwtSettings))
                    .ValidateFluentValidation().ValidateOnStart();

            // Jwt config 
            builder.Services.AddHttpContextAccessor();
            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(nameof(JwtSettings)));

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

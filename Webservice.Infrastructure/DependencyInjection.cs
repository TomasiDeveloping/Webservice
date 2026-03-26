using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Webservice.Infrastructure.Identity;
using Webservice.Infrastructure.Logging.Configuration;
using Webservice.Infrastructure.Logging.Middleware;
using Webservice.Infrastructure.Persistence;
using Webservice.Infrastructure.Services;
using Webservice.Infrastructure.Settings;

namespace Webservice.Infrastructure;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddInfrastructureDependencies(IConfiguration configuration)
        {
            services.AddTransient<LoggingMiddleware>();
            services.AddTransient<MetricsMiddleware>();
            services.AddTransient<TelemetryMiddleware>();


            services.Configure<DatabaseSettings>(configuration.GetSection("DatabaseSettings"));
            services.AddScoped<DatabaseSeeder>();
            // DbContext
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            //Identity
            services.AddDataProtection();
            services.AddIdentityCore<ApplicationUser>(options =>
                {
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireNonAlphanumeric = true;

                    options.User.RequireUniqueEmail = true;
                })
                .AddRoles<IdentityRole<Guid>>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();


            // Serilog
            SerilogConfig.ConfigureSerilog(configuration);

            return services;
        }
    }
}
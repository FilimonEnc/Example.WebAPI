using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;

using Example.Application.Interfaces;
using Example.Infrastructure.Services;

namespace Example.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DbConnection");

            services.AddDbContext<CrmContext>(option =>
            {
                option.UseSqlServer(connectionString);
            });

            services.AddScoped<ICrmContext, CrmContext>();
            return services;
        }

        public static IServiceCollection AddEmailService(this IServiceCollection services, Action<EmailSettings> configuration)
        {
            services.Configure(configuration);
            services.AddScoped<IEmailSender, EmailSender>();
            return services;
        }
    }
}

using Example.Application.AccessValidation;
using Example.Application.Behaviors;

using FluentValidation;

using Mapster;

using MapsterMapper;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

using System.Reflection;

using Example.Application.Interfaces;
using Example.Application.Mapping;
using Example.Application.Notification;

namespace Example.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });
            services.AddAccessValidator(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AccessBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

            var config = new TypeAdapterConfig();
            new MapRegister().Register(config);
            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();
            services.AddScoped<INotificationManager, NotificationManager>();

            return services;
        }
    }
}

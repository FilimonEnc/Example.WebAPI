using Microsoft.Extensions.DependencyInjection;

using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Example.Application.AccessValidation
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAccessValidator(this IServiceCollection services, IEnumerable<Assembly> assemblies, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            foreach (var assembly in assemblies)
            {
                services.AddAccessValidator(assembly, lifetime);
            }
            return services;
        }

        public static IServiceCollection AddAccessValidator(this IServiceCollection services, Assembly assembly, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            var types = assembly.GetExportedTypes();
            var findType = typeof(IAccessValidator<>);

            var scanResults = from type in types
                              where !type.IsAbstract && !type.IsGenericTypeDefinition
                              let interfaces = type.GetInterfaces()
                              let genericInterfaces = interfaces.Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == findType)
                              let matchingInterface = genericInterfaces.FirstOrDefault()
                              where matchingInterface != null
                              select (matchingInterface, type);

            foreach (var (matchingInterface, type) in scanResults)
            {
                services.Add(
                   new ServiceDescriptor(
                       serviceType: matchingInterface,
                       implementationType: type,
                       lifetime: lifetime));
            }

            return services;
        }

    }
}

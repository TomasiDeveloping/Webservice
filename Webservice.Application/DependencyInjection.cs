using Microsoft.Extensions.DependencyInjection;

namespace Webservice.Application;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddApplicationDependencies()
        {
            return services;
        }
    }
}
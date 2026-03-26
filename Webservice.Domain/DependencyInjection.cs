using Microsoft.Extensions.DependencyInjection;

namespace Webservice.Domain;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddDomainDependencies()
        {
            return services;
        }
    }
}
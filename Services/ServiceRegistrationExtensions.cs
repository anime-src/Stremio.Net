using Microsoft.Extensions.DependencyInjection;

namespace Stremio.Net.Services;

public static class ServiceRegistrationExtensions
{
    public static void AddStremioServices(this IServiceCollection services)
    {
        services.AddTransient<IStremioApplicationService, StremioApplicationService>();
    }
}
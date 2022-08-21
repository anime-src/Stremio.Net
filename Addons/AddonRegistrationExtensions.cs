using System;
using Microsoft.Extensions.DependencyInjection;
using Stremio.Net.Addons.Resolvers;

namespace Stremio.Net.Addons;

public static class AddonRegistrationExtensions
{
    public static void AddStremioAddons(this IServiceCollection services, Action<IAddonProviderRegistrationOptions> registrationAction)
    {
        var registrationOptions = new AddonProviderRegistrationOptions();
        registrationAction.Invoke(registrationOptions);

        services.AddHttpClient();
        services.AddHttpContextAccessor();
        services.AddSingleton(registrationOptions);
        services.AddSingleton<IAddonProviderOptions>(provider => provider.GetRequiredService<AddonProviderRegistrationOptions>());
        services.AddTransient<IAddonProviderFactory, AddonProviderFactory>();
        services.AddTransient<IAddonProviderNameResolver, SubdomainAddonProviderNameResolver>();
        services.AddTransient<IAddonProviderNameResolver, QueryStringAddonProviderNameResolver>();
        services.AddTransient<IAddonProviderNameResolverService, AddonProviderNameResolverService>();
        
        foreach (var providerType in registrationOptions.GetAllProviderTypes())
            services.AddTransient(providerType);
    }
}
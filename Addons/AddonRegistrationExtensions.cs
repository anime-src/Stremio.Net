using System;
using Microsoft.Extensions.DependencyInjection;

namespace Stremio.Net.Addons;

public static class AddonRegistrationExtensions
{
    public static void AddAddons(this IServiceCollection services, Action<IAddonProviderRegistrationOptions> registrationAction)
    {
        var registrationOptions = new AddonProviderRegistrationOptions();
        registrationAction.Invoke(registrationOptions);

        services.AddHttpClient();
        services.AddHttpContextAccessor();
        services.AddSingleton(registrationOptions);
        services.AddSingleton<IAddonProviderOptions>(provider => provider.GetRequiredService<AddonProviderRegistrationOptions>());
        services.AddTransient<IAddonProviderFactory, AddonProviderFactory>();
        services.AddTransient<IAddonProviderNameResolver, SubdomainAddonProviderNameResolver>();
        
        foreach (var providerType in registrationOptions.GetAllProviderTypes())
            services.AddTransient(providerType);
    }
}
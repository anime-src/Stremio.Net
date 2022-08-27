using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Stremio.Net.Addons;
using Stremio.Net.Addons.Resolvers;
using Stremio.Net.Pages;
using Stremio.Net.Routing;
using Stremio.Net.Services;

namespace Stremio.Net
{
    public static class ServiceRegistration
    {
        public static void AddStremio(this IServiceCollection services, Action<IAddonProviderRegistrationOptions> registrationAction)
        {
            services.AddStremioAddons(registrationAction);
            services.AddStremioPageBuilders();
            services.AddStremioServices();
        }
       
        private static void AddStremioAddons(this IServiceCollection services, Action<IAddonProviderRegistrationOptions> registrationAction)
        {
            var registrationOptions = new AddonProviderRegistrationOptions();
            registrationAction.Invoke(registrationOptions);

            services.AddHttpClient();
            services.AddHttpContextAccessor();
            services.AddSingleton(registrationOptions);
            services.AddSingleton<IAddonProviderOptions>(provider => provider.GetRequiredService<AddonProviderRegistrationOptions>());
            services.AddTransient<IAddonProviderFactory, AddonProviderFactory>();
            services.AddTransient<IAddonProviderNameResolver, SubdomainAddonProviderNameResolver>();
            services.AddTransient<IAddonProviderNameResolver, PathAddonProviderNameResolver>();
            services.AddTransient<IAddonProviderNameResolverService, AddonProviderNameResolverService>();
        
            services.AddScoped<AddonProviderNameContext>();
            services.AddScoped<IAddonProviderNameContext>(provider => provider.GetRequiredService<AddonProviderNameContext>());
            services.AddScoped<IAddonProviderNameSetter>(provider => provider.GetRequiredService<AddonProviderNameContext>());
            services.AddSingleton<IAddonProviderNameStore, AddonProviderNameStore>();
        
            foreach (Type providerType in registrationOptions.GetAllProviderTypes())
                services.AddTransient(providerType);
        }
        
        private static void AddStremioPageBuilders(this IServiceCollection services)
        {
            services.AddSingleton<IAddonPageBuilder, AddonPageBuilder>();
            services.AddSingleton<ILandingPageBuilder, LandingPageBuilder>();
        }
        private static void AddStremioServices(this IServiceCollection services)
        {
            services.AddTransient<IStremioApplicationService, StremioApplicationService>();
        }

        public static void UseStremio(this WebApplication webApplication)
        {
            webApplication.UseMiddleware<StremioMiddleware>();
        }
    }
}
using System;
using Microsoft.Extensions.DependencyInjection;

namespace Stremio.Net.Addons
{
    public interface IAddonProviderFactory
    {
        IAddonProvider? ResolveAddon(string value);
    }

    public class AddonProviderFactory : IAddonProviderFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IAddonProviderOptions _addonProviderOptions;

        public AddonProviderFactory(IServiceProvider serviceProvider, IAddonProviderOptions addonProviderOptions)
        {
            _serviceProvider = serviceProvider;
            _addonProviderOptions = addonProviderOptions;
        }

        public IAddonProvider? ResolveAddon(string value)
        {
            var providerType = _addonProviderOptions.GetProviderType(value);
            if (providerType != null && _serviceProvider.GetRequiredService(providerType) is IAddonProvider addonProvider)
                return addonProvider;
            return default;
        }
    }
}
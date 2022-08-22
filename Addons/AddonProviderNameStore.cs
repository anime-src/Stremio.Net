using System;
using System.Collections.Generic;
using System.Linq;

namespace Stremio.Net.Addons
{
    public interface IAddonProviderNameStore
    {
        AddonProviderName? GetProviderName(string? name);
    }

    public class AddonProviderNameStore : IAddonProviderNameStore
    {
        private readonly IAddonProviderOptions _addonProviderOptions;
        private readonly Lazy<IReadOnlyDictionary<string, AddonProviderName>> _cachedProviderNames;
      
        public AddonProviderNameStore(IAddonProviderOptions addonProviderOptions)
        {
            _addonProviderOptions = addonProviderOptions;
            _cachedProviderNames = new Lazy<IReadOnlyDictionary<string, AddonProviderName>>(CreateCacheProviders);
        }
        
        public AddonProviderName? GetProviderName(string? name)
        {
            if (string.IsNullOrEmpty(name)) return null;
            return _cachedProviderNames.Value.TryGetValue(name, out AddonProviderName? addonProviderName) ? addonProviderName : null;
        }
        
        private IReadOnlyDictionary<string, AddonProviderName> CreateCacheProviders()
        {
            return _addonProviderOptions.GetAllProviderNames().ToDictionary(providerName => providerName, providerName => new AddonProviderName(providerName));
        }
    }
}
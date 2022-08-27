using System;
using System.Collections.Generic;
using System.Linq;

namespace Stremio.Net.Addons
{
    public interface IAddonProviderRegistrationOptions
    {
        void Register<TAddonProvider>(string name) where TAddonProvider : IAddonProvider;
    }

    public interface IAddonProviderOptions
    {
        Type? GetProviderType(string value);

        IReadOnlyList<string> GetAllProviderNames();
    }

    public class AddonProviderRegistrationOptions : IAddonProviderRegistrationOptions, IAddonProviderOptions
    {
        private readonly Dictionary<string, Type> _addonProviders = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase);

        public void Register<TAddonProvider>(string name) where TAddonProvider : IAddonProvider
        {
            _addonProviders.TryAdd(name, typeof(TAddonProvider));
        }

        public IReadOnlyList<Type> GetAllProviderTypes() => _addonProviders.Values.ToList();

        public IReadOnlyList<string> GetAllProviderNames() => _addonProviders.Keys.ToList();

        public Type? GetProviderType(string value) => _addonProviders.TryGetValue(value, out Type? providerType) ? providerType : null;
    }
}
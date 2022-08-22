using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Stremio.Net.Extensions;
using Stremio.Net.Models;

namespace Stremio.Net.Addons;

public interface IAddonPageBuilder
{
    string BuildPage(Manifest manifest);
}

public class AddonPageBuilder : IAddonPageBuilder
{
    private static readonly Lazy<string> LazyPageTemplate = new Lazy<string>(() => typeof(AddonPageBuilder).Assembly.GetManifestResourceFile("LandingTemplate.html"));

    private const string AddonName = "{AddonName}";
    private const string AddonBackgroundImage = "{AddonBackgroundImage}";
    private const string AddonLogo = "{AddonLogo}";
    private const string AddonVersion = "{AddonVersion}";
    private const string AddonDescription = "{AddonDescription}";
    private const string AddonTypes = "{AddonTypes}";
    
    private readonly Dictionary<string, Func<Manifest, string>> _pageTemplateReplaceFunctions = new Dictionary<string, Func<Manifest, string>>()
    {
        [AddonName] = manifest => manifest.Name,
        [AddonBackgroundImage] = manifest => string.IsNullOrEmpty(manifest.Background) ? "https://dl.strem.io/addon-background.jpg" : manifest.Background,
        [AddonLogo] = manifest => string.IsNullOrEmpty(manifest.Logo) ? "https://dl.strem.io/addon-logo.png" : manifest.Logo,
        [AddonVersion] = manifest => string.IsNullOrEmpty(manifest.Version) ? "0.0.0": manifest.Version,
        [AddonDescription] = manifest => manifest.Description,
        [AddonTypes] = manifest => BuildManifestTypes(manifest) 
    };

    private readonly ConcurrentDictionary<string, string> _cachedPages = new ConcurrentDictionary<string, string>();
    
    public string BuildPage(Manifest manifest)
    {
        return _cachedPages.GetOrAdd(manifest.Id, _ =>
        {
            string template = LazyPageTemplate.Value;
            foreach (var (parameter, valueGetter) in _pageTemplateReplaceFunctions)
                template = template.Replace(parameter, valueGetter(manifest));
            return template;
        });
    }
    
    
    private static string BuildManifestTypes(Manifest manifest)
    {
        if (!manifest.Types.Any()) return string.Empty;
        
        var normalizedTypes = manifest.Types.Select(type => char.ToUpper(type[0]) + type.Substring(1) + (type != "series" ? "s" : "")).ToList();
        return string.Join("", normalizedTypes.Select(t => $"<li>{t}</li>"));
    }

}
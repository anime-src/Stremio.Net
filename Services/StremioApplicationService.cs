using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stremio.Net.Addons;
using Stremio.Net.Models;
using Stremio.Net.Models.Catalogs;
using Stremio.Net.Models.Metadata;

namespace Stremio.Net.Services;

public class StremioApplicationService : IStremioApplicationService
{
    private readonly IAddonProviderFactory _addonProviderFactory;
    private readonly IAddonProviderNameContext _addonProviderNameContext;

    public StremioApplicationService(IAddonProviderFactory addonProviderFactory, IAddonProviderNameContext addonProviderNameContext)
    {
        _addonProviderFactory = addonProviderFactory;
        _addonProviderNameContext = addonProviderNameContext;
    }
    
    public async ValueTask<IActionResult> GetManifestAsync(CancellationToken cancellationToken = default)
    {
        return await ExecuteProviderAsync(async  (provider, providerName) =>
        {
            Manifest manifest = await provider.GetManifestAsync(providerName, cancellationToken);
            return new OkObjectResult(manifest);
        });
    }

    public async ValueTask<IActionResult> GetCatalogMetaAsync(string type, string id, string? extraProps, CancellationToken cancellationToken = default)
    {
        return await ExecuteProviderAsync(async (provider, providerName) =>
        {
            Meta[] metas = await provider.GetCatalogMetaAsync(type, id, ExtraValue.Parse(extraProps), providerName, cancellationToken);
            return new OkObjectResult(new { metas });
        });
    }

    private async ValueTask<IActionResult> ExecuteProviderAsync(Func<IAddonProvider, AddonProviderName, ValueTask<IActionResult>> func)
    {
        AddonProviderName? providerName = _addonProviderNameContext.CurrentProvider;

        if (providerName is null)
            return new BadRequestObjectResult("Failed to resolve stremio addon!");

        IAddonProvider? addonProvider = _addonProviderFactory.ResolveAddon(providerName.Name);

        if (addonProvider is null)
            return new BadRequestObjectResult($"Failed to resolve '{providerName.Name}' stremio addon provider!");

        return await func(addonProvider, providerName);
    }
}
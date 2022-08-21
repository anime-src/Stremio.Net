using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stremio.Net.Addons;
using Stremio.Net.Models.Catalogs;
using Stremio.Net.Models.Metadata;

namespace Stremio.Net.Services;

public class StremioApplicationService : IStremioApplicationService
{
    private readonly IAddonProviderFactory _addonProviderFactory;
    private readonly IAddonProviderNameResolverService _addonProviderNameResolvers;

    public StremioApplicationService(IAddonProviderFactory addonProviderFactory, IAddonProviderNameResolverService addonProviderNameResolvers)
    {
        _addonProviderFactory = addonProviderFactory;
        _addonProviderNameResolvers = addonProviderNameResolvers;
    }
    
    public async ValueTask<IActionResult> GetManifestAsync(CancellationToken cancellationToken = default)
    {
        return await ExecuteProviderAsync(async provider =>
        {
            var manifest = await provider.GetManifestAsync(cancellationToken);
            return new OkObjectResult(manifest);
        }, cancellationToken);
    }

    public async ValueTask<IActionResult> GetCatalogMetaAsync(string type, string id, string? extraProps, CancellationToken cancellationToken = default)
    {
        return await ExecuteProviderAsync(async provider =>
        {
            Meta[] metas = await provider.GetCatalogMetaAsync(type, id, ExtraValue.Parse(extraProps), cancellationToken);
            return new OkObjectResult(new { metas });
        }, cancellationToken);
    }

    private async ValueTask<IActionResult> ExecuteProviderAsync(Func<IAddonProvider, ValueTask<IActionResult>> func, CancellationToken cancellationToken)
    {
        string? resolvedAddonName = await _addonProviderNameResolvers.ResolveAsync(cancellationToken);

        if (string.IsNullOrEmpty(resolvedAddonName))
            return new BadRequestObjectResult("Failed to resolve stremio addon!");

        IAddonProvider? provider = _addonProviderFactory.ResolveAddon(resolvedAddonName);

        if (provider is null)
            return new BadRequestObjectResult($"Failed to resolve '{resolvedAddonName}' stremio addon provider!");

        return await func(provider);
    }
}
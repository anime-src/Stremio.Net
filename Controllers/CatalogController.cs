using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Stremio.Net.Addons;
using Stremio.Net.Models.Catalogs;
using Stremio.Net.Models.Metadata;

namespace Stremio.Net.Controllers;

[ApiController]
[Route("[controller]")]
public class CatalogController : ControllerBase
{
    private readonly IAddonProviderNameResolver _addonProviderNameResolver;
    private readonly IAddonProviderFactory _addonProviderFactory;

    public CatalogController(IAddonProviderNameResolver addonProviderNameResolver, IAddonProviderFactory addonProviderFactory)
    {
        _addonProviderNameResolver = addonProviderNameResolver;
        _addonProviderFactory = addonProviderFactory;
    }

    // GET /catalog
    [HttpGet("{type}/{id}.json")]
    public async Task<IActionResult> Get(string type, string id)
    {
        return await GetMetasResponseAsync(type, id, null);
    }
    
    [HttpGet("{type}/{id}/{extraProps}.json")]
    public async Task<IActionResult> GetWithExtras(string type, string id, string? extraProps, CancellationToken cancellationToken)
    {
        return await GetMetasResponseAsync(type, id, extraProps, cancellationToken);
    }

    private async Task<IActionResult> GetMetasResponseAsync(string type, string id, string? extraProps, CancellationToken cancellationToken = default)
    {
        string? resolvedAddonName = await _addonProviderNameResolver.ResolveAsync();
       
        if (string.IsNullOrEmpty(resolvedAddonName))
            return BadRequest();

        IAddonProvider? provider = _addonProviderFactory.ResolveAddon(resolvedAddonName);
        if (provider is null)
            return NotFound("Provider not found.");

        Meta[] metas = await provider.GetCatalogMetaAsync(type, id, ExtraValue.Parse(extraProps), cancellationToken);
        
        return Ok(new { metas });
    }
}
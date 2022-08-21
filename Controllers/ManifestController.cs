using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stremio.Net.Addons;

namespace Stremio.Net.Controllers;

[ApiController]
public class ManifestController : ControllerBase
{
    private readonly IAddonProviderNameResolver _addonProviderNameResolver;
    private readonly IAddonProviderFactory _addonProviderFactory;

    public ManifestController(IAddonProviderNameResolver addonProviderNameResolver, IAddonProviderFactory addonProviderFactory)
    {
        _addonProviderNameResolver = addonProviderNameResolver;
        _addonProviderFactory = addonProviderFactory;
    }

    [Route("manifest.json")]
    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken = default)
    {
        string? resolvedAddonName = await _addonProviderNameResolver.ResolveAsync();
       
        if (string.IsNullOrEmpty(resolvedAddonName))
            return BadRequest();

        IAddonProvider? provider = _addonProviderFactory.ResolveAddon(resolvedAddonName);
        if (provider is null)
            return NotFound("Provider not found.");

        var manifest = await provider.GetManifestAsync(cancellationToken);
        return Ok(manifest);
    }
}
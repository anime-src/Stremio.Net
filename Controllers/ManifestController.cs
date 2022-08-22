using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stremio.Net.Services;

namespace Stremio.Net.Controllers;

[ApiController]
public class ManifestController : ControllerBase
{
    private readonly IStremioApplicationService _stremioApplicationService;

    public ManifestController(IStremioApplicationService stremioApplicationService)
    {
        _stremioApplicationService = stremioApplicationService;
    }

    // R: {*addon}.domain.com/manifest.json
    // R: domain.com/{*addon}/manifest.json
    [HttpGet]
    [Route("manifest.json")]
    [Route("{any:regex(^.*$)}/manifest.json")]
    public async Task<IActionResult> GetManifest(CancellationToken cancellationToken = default)
    {
        return await _stremioApplicationService.GetManifestAsync(cancellationToken);
    }
    
    // R: {*addon}.domain.com/
    // R: domain.com/{*addon}/
    [HttpGet]
    [Route("/")]
    [Route("{any:regex(^.*$)}")]
    public async Task<IActionResult> GetPage(CancellationToken cancellationToken = default)
    {
        return await _stremioApplicationService.GetAddonPageAsync(cancellationToken);
    }
}
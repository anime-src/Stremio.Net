using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stremio.Net.Services;

namespace Stremio.Net.Controllers;

[ApiController]
[Route("manifest.json")]
[Route("{any:regex(^.*$)}/manifest.json")]
public class ManifestController : ControllerBase
{
    private readonly IStremioApplicationService _stremioApplicationService;

    public ManifestController(IStremioApplicationService stremioApplicationService)
    {
        _stremioApplicationService = stremioApplicationService;
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken = default)
    {
        return await _stremioApplicationService.GetManifestAsync(cancellationToken);
    }
}
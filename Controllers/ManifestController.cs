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

    [Route("manifest.json")]
    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken = default)
    {
        return await _stremioApplicationService.GetManifestAsync(cancellationToken);
    }
}
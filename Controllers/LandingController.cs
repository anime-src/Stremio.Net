using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stremio.Net.Services;

namespace Stremio.Net.Controllers;

[ApiController]
[Route("{any:regex(^.*$)}")]
public class LandingController : ControllerBase
{
    private readonly IStremioApplicationService _stremioApplicationService;

    public LandingController(IStremioApplicationService stremioApplicationService)
    {
        _stremioApplicationService = stremioApplicationService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken = default)
    {
        return await _stremioApplicationService.GetAddonPageAsync(cancellationToken);
    }
}
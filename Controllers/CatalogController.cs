using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stremio.Net.Services;

namespace Stremio.Net.Controllers;

[ApiController]
[Route("[controller]")]
public class CatalogController : ControllerBase
{
    private readonly IStremioApplicationService _stremioApplicationService;

    public CatalogController(IStremioApplicationService stremioApplicationService)
    {
        _stremioApplicationService = stremioApplicationService;
    }

    // GET /catalog/type/id.json
    [HttpGet("{type}/{id}.json")]
    public async Task<IActionResult> Get(string type, string id, CancellationToken cancellationToken)
    {
        return await _stremioApplicationService.GetCatalogMetaAsync(type, id, null, cancellationToken);
    }
    
    // GET /catalog/type/id/prop1=val&prop2=val.json
    [HttpGet("{type}/{id}/{extraProps}.json")]
    public async Task<IActionResult> GetWithExtras(string type, string id, string? extraProps, CancellationToken cancellationToken)
    {
        return await _stremioApplicationService.GetCatalogMetaAsync(type, id, extraProps, cancellationToken);
    }
}
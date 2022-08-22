using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stremio.Net.Services;

namespace Stremio.Net.Controllers;

// R: {*addon}.domain.com/catalog
// R: domain.com/{*addon}/catalog
[ApiController]
[Route("[controller]")]
[Route("{any:regex(^.*$)}/[controller]")]
public class CatalogController : ControllerBase
{
    private readonly IStremioApplicationService _stremioApplicationService;

    public CatalogController(IStremioApplicationService stremioApplicationService)
    {
        _stremioApplicationService = stremioApplicationService;
    }

    // GET /catalog/type/id.json
    // GET /catalog/type/id/extra.name=value.json
    // GET /catalog/type/id/extra.name1=value1&extra.name2=value2.json
    [HttpGet("{type}/{id}.json")]
    [HttpGet("{type}/{id}/{extraProps}.json")]
    public async Task<IActionResult> Get(string type, string id, string? extraProps, CancellationToken cancellationToken)
    {
        return await _stremioApplicationService.GetCatalogMetaAsync(type, id, extraProps, cancellationToken);
    }
}
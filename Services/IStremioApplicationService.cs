using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Stremio.Net.Services;

public interface IStremioApplicationService
{
    ValueTask<IActionResult> GetManifestAsync(CancellationToken cancellationToken = default);

    ValueTask<IActionResult> GetCatalogMetaAsync(string type, string id, string? extraProps, CancellationToken cancellationToken = default);
}
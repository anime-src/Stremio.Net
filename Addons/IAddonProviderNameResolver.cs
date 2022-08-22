using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Stremio.Net.Addons;

public interface IAddonProviderNameResolver
{
    ValueTask<string?> ResolveAsync(HttpContext? context = null, CancellationToken cancellationToken = default);
}
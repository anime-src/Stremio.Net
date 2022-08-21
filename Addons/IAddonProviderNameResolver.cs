using System.Threading;
using System.Threading.Tasks;

namespace Stremio.Net.Addons;

public interface IAddonProviderNameResolver
{
    ValueTask<string?> ResolveAsync(CancellationToken cancellationToken = default);
}
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Stremio.Net.Addons;

public interface IAddonProviderNameResolverService
{
    ValueTask<string?> ResolveAsync(CancellationToken cancellationToken = default);
}

public class AddonProviderNameResolverService : IAddonProviderNameResolverService
{
    private readonly IEnumerable<IAddonProviderNameResolver> _resolvers;

    public AddonProviderNameResolverService(IEnumerable<IAddonProviderNameResolver> resolvers)
    {
        _resolvers = resolvers;
    }
    
    public async ValueTask<string?> ResolveAsync(CancellationToken cancellationToken = default)
    {
        foreach (var resolver in _resolvers)
        {
            string? name = await resolver.ResolveAsync(cancellationToken);

            if (!string.IsNullOrEmpty(name))
                return name;
            if(cancellationToken.IsCancellationRequested)
                break;
        }
        return null;
    }
}
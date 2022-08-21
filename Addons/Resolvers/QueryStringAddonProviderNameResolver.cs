using System.Threading;
using System.Threading.Tasks;

namespace Stremio.Net.Addons.Resolvers
{
    public class QueryStringAddonProviderNameResolver : IAddonProviderNameResolver
    {
        public const string QueryStringName = "provider";
        
        public ValueTask<string?> ResolveAsync(CancellationToken cancellationToken = default)
        {
            return ValueTask.FromResult<string?>(null);
        }
    }
}
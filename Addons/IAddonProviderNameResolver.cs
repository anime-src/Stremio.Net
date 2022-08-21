using System.Threading.Tasks;

namespace Stremio.Net.Addons;

public interface IAddonProviderNameResolver
{
    ValueTask<string> ResolveAsync();
}
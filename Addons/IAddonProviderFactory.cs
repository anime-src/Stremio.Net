namespace Stremio.Net.Addons;

public interface IAddonProviderFactory
{
    IAddonProvider? ResolveAddon(string value);
}
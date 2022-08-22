namespace Stremio.Net.Addons
{
    public interface IAddonProviderNameContext
    {
        AddonProviderName? CurrentProvider { get; }
    }

    public interface IAddonProviderNameSetter
    {
        AddonProviderName? CurrentProvider { set; }
    }

    public class AddonProviderNameContext : IAddonProviderNameContext, IAddonProviderNameSetter
    {
        public AddonProviderName? CurrentProvider { get; set; }
    }

    public class AddonProviderName
    {
        public string Name { get; }

        public AddonProviderName(string name)
        {
            Name = name;
        }
    }
}
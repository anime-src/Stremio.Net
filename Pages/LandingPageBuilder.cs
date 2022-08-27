using System;
using System.Threading.Tasks;
using Stremio.Net.Extensions;

namespace Stremio.Net.Pages
{
    public interface ILandingPageBuilder
    {
        ValueTask<string> BuildPageAsync();
    }

    public class LandingPageBuilder : ILandingPageBuilder
    {
        private static readonly Lazy<string> PageTemplate = new Lazy<string>(() => typeof(LandingPageBuilder).Assembly.GetManifestResourceFile("StremioNetPageTemplate.html"));
        
        public ValueTask<string> BuildPageAsync()
        {
            return ValueTask.FromResult(PageTemplate.Value);
        }
    }
}
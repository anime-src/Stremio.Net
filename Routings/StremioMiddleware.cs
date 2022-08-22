using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Stremio.Net.Addons;

namespace Stremio.Net.Routings
{
    public class StremioMiddleware
    {
        private readonly RequestDelegate _next;

        public StremioMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IAddonProviderNameResolverService nameResolverService, IAddonProviderNameSetter addonProviderNameSetter, IAddonProviderNameStore addonProviderNameStore)
        {
            string? resolvedProviderName = await nameResolverService.ResolveAsync(context);

            AddonProviderName? providerName = addonProviderNameStore.GetProviderName(resolvedProviderName);

            if (providerName == null)
            {
                string? path = context.Request.Path.Value;
         
                if (string.IsNullOrEmpty(path) || path == "/")
                {
                    await context.Response.WriteAsync("Hello From Stremio.Net!");
                    return;
                }

                context.Response.StatusCode = 404;
                await context.Response.WriteAsJsonAsync(new { Message = "Unknown stremio addon!" }).ConfigureAwait(false);
                return;
            }

            addonProviderNameSetter.CurrentProvider = providerName;

            await _next(context);
        }
    }
}
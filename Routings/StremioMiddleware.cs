using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Stremio.Net.Addons;
using Stremio.Net.Extensions;

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
                    context.Response.ContentType = "text/html; charset=UTF-8";
                    context.Response.StatusCode = StatusCodes.Status200OK;
                    await context.Response.WriteAsync(GetType().Assembly.GetManifestResourceFile("StremioNetPageTemplate.html"));
                    return;
                }

                context.Response.StatusCode = 404;
                await context.Response.WriteAsJsonAsync(new { Message = "Unknown Stremio Addon!" }).ConfigureAwait(false);
                return;
            }

            addonProviderNameSetter.CurrentProvider = providerName;

            await _next(context);
        }
    }
}
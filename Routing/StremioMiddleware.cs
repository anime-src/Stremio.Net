using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Stremio.Net.Addons;
using Stremio.Net.Pages;

namespace Stremio.Net.Routing
{
    public class StremioMiddleware
    {
        private readonly RequestDelegate _next;

        public StremioMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, 
                                 IAddonProviderNameResolverService nameResolverService, 
                                 IAddonProviderNameSetter addonProviderNameSetter, 
                                 IAddonProviderNameStore addonProviderNameStore,
                                 ILandingPageBuilder landingPageBuilder)
        {
            string? resolvedProviderName = await nameResolverService.ResolveAsync(context);

            AddonProviderName? providerName = addonProviderNameStore.GetProviderName(resolvedProviderName);

            if (providerName == null)
            {
                string? path = context.Request.Path.Value;
         
                if (string.IsNullOrEmpty(path) || path == "/")
                {
                    await HandleLandingPageAsync(landingPageBuilder, context);
                 
                    return;
                }

                context.Response.StatusCode = 404;
                await context.Response.WriteAsJsonAsync(new { Message = "Unknown Stremio Addon!" }).ConfigureAwait(false);
                return;
            }

            addonProviderNameSetter.CurrentProvider = providerName;

            await _next(context);
        }

        private static async Task HandleLandingPageAsync(ILandingPageBuilder landingPageBuilder, HttpContext context)
        {
            context.Response.ContentType = "text/html; charset=UTF-8";
            context.Response.StatusCode = StatusCodes.Status200OK;
            await context.Response.WriteAsync(await landingPageBuilder.BuildPageAsync());
        }
    }
}
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace Stremio.Net.Addons.Resolvers
{
    public class SubdomainAddonProviderNameResolver : IAddonProviderNameResolver
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private static readonly ValueTask<string?> EmptyResult = ValueTask.FromResult<string?>(null);

        public SubdomainAddonProviderNameResolver(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ValueTask<string?> ResolveAsync(HttpContext? context = null, CancellationToken cancellationToken = default)
        {
            context ??= _httpContextAccessor.HttpContext;
            if (context is null) return EmptyResult;

            // example: https://addon1.domain.com/ -> gives addon1
            // example: https://addon2.domain.com/manifest.json -> gives addon2
            string? subDomain = GetSubDomain(new Uri(context.Request.GetDisplayUrl()));

            return ValueTask.FromResult(subDomain);
        }

        private static string? GetSubDomain(Uri url)
        {
            if (url.HostNameType == UriHostNameType.Dns)
            {
                string host = url.Host;

                if (host.Split('.').Length > 2)
                {
                    int lastIndex = host.LastIndexOf(".", StringComparison.Ordinal);
                    int index = host.LastIndexOf(".", lastIndex - 1, StringComparison.Ordinal);
                    return host.Substring(0, index);
                }
            }
            return null;
        }
    }
}
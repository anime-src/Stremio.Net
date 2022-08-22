using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace Stremio.Net.Addons.Resolvers
{
    public class PathAddonProviderNameResolver : IAddonProviderNameResolver
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private static readonly ValueTask<string?> EmptyResult = ValueTask.FromResult<string?>(null);
        private const string SegmentName = "provider";
        
        public PathAddonProviderNameResolver(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        
        public ValueTask<string?> ResolveAsync(HttpContext? context = null, CancellationToken cancellationToken = default)
        {
            context ??= _httpContextAccessor.HttpContext;
            if (context is null) return EmptyResult;
            
            // example: https://domain.com/addon1 -> gives addon1
            // example: https://domain.com/addon2/manifest.json -> gives addon2
            var addonName = new Uri(context.Request.GetDisplayUrl()).Segments.FirstOrDefault(x => x != "/")?.TrimEnd('/');
            
            if (!string.IsNullOrWhiteSpace(addonName) && context.Request.Path.StartsWithSegments($"/{addonName}", out _))
            {
                return ValueTask.FromResult<string?>(addonName);
            }
            return EmptyResult;
        }
    }
}
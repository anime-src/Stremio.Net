using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Stremio.Net.Extensions;

namespace Stremio.Net.Addons;

public class SubdomainAddonProviderNameResolver : IAddonProviderNameResolver
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private static readonly ValueTask<string> EmptyResult = ValueTask.FromResult(string.Empty);
    
    public SubdomainAddonProviderNameResolver(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public ValueTask<string> ResolveAsync()
    {
        HttpContext? context = _httpContextAccessor.HttpContext;
        if (context is null) return EmptyResult;

        HttpRequest contextRequest = context.Request;

        var url = new Uri($"{contextRequest.Scheme}://{contextRequest.Host}{contextRequest.PathBase}{contextRequest.Path}{contextRequest.QueryString}");
        
        var result = url.HostNameType == UriHostNameType.Dns ? url.Host.Split('.')[0] : string.Empty;

        if (string.IsNullOrEmpty(result) && context.IsLocalRequest())
        {
            return ValueTask.FromResult(AddonProviders.Dummy);
        }
        return ValueTask.FromResult(result);
    }
}
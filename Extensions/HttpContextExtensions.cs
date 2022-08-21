using System.Net;
using Microsoft.AspNetCore.Http;

namespace Stremio.Net.Extensions;

public static class HttpContextExtensions
{
    public static bool IsLocalRequest(this HttpContext context)
    {
        if (context.Connection.RemoteIpAddress == null && context.Connection.LocalIpAddress == null)
        {
            return true;
        }

        if (context.Request.Host.Host is "localhost" or "127.0.0.1")
        {
            return true;
        }
        
        if (context.Connection.RemoteIpAddress is null)
            return false;
        
        if (context.Connection.RemoteIpAddress.Equals(context.Connection.LocalIpAddress))
        {
            return true;
        }
        
        return IPAddress.IsLoopback(context.Connection.RemoteIpAddress);
    }
}
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Serilog;
using Stremio.Net;
using Stremio.Net.Addons;
using Stremio.Net.Addons.Providers;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console());

builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
builder.Services.AddCors();
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
    options.SerializerSettings.Converters.Add(new StringEnumConverter());
});
builder.Services.AddStremio(options =>
{
    options.Register<DummyAddonProvider>(AddonProviderNames.Demo);
});

WebApplication app = builder.Build();

app.UseCors(corsPolicyBuilder =>
    corsPolicyBuilder
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()
);

app.UseStremio();
app.MapControllers();
app.Run();
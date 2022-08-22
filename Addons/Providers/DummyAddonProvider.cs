using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Stremio.Net.Models;
using Stremio.Net.Models.Catalogs;
using Stremio.Net.Models.Metadata;

namespace Stremio.Net.Addons.Providers;

public class DummyAddonProvider : IAddonProvider
{
    const string MetahubUrl = "https://images.metahub.space/poster/medium/{0}/img";

    private static readonly string[] Genres = { "Drama", "Horror", "Mystery", "Music", "War", "Western", "Animation", "Short", "Comedy", "Sci-Fi" };

    private static readonly Extra[] Extras =
    {
        new Extra { Name = "search", IsRequired = false },
        new Extra { Name = "skip", IsRequired = false },
        new Extra { Name = "genre", IsRequired = false, Options = Genres }
    };

    private static readonly Manifest Manifest = CreateManifest("Stremio.Net.DummyProvider");

    private static Manifest CreateManifest(string id)
    {
       return new Manifest
        {
            Id = $"stremio.net.{id.ToLower()}",
            Version = "1.0.0",
            Name = "Stremio.Net",
            Description = "Sample addon made C# ASP.NET CORE 6 providing a few public domain movies",
            Resources = new[]
            {
                "catalog",
                "meta",
                "stream"
            },
            IdPrefixes = new[] { "tt" },
            Types = new[] { "movie", "series" },
            Catalogs = new[]
            {
                new Catalog { Type = "movie", Id = "stremio-net-movies", Name = "Stremio.Net Movies", Extra = Extras },
                new Catalog { Type = "series", Id = "stremio-net-series", Name = "Stremio.Net Series", Extra = Extras }
            }
        };
    }

    private static readonly Meta[] Metas = BuildMetas();

    private static Meta[] BuildMetas()
    {
        var metas = new[]
        {
            new Meta
            {
                Id = "tt0032138", Name = "The Wizard of Oz",
                Genres = new[] { "Adventure", "Family", "Fantasy", "Musical" }
            },
            new Meta { Id = "tt0017136", Name = "Metropolis", Genres = new[] { "Drama", "Sci-Fi" } },
            new Meta { Id = "tt0051744", Name = "House on Haunted Hill", Genres = new[] { "Horror", "Mystery" } },
            new Meta { Id = "tt1254207", Name = "Big Buck Bunny", Genres = new[] { "Animation", "Short", "Comedy" } },
            new Meta { Id = "tt0031051", Name = "The Arizona Kid", Genres = new[] { "Music", "War", "Western" } },
            new Meta { Id = "tt0137523", Name = "Fight Club", Genres = new[] { "Drama" } },
            new Meta
            {
                Id = "tt1748166",
                Name = "Pioneer One",
                Genres = new[] { "Drama" },
                Videos = new[] { new Video { Id = "tt1748166:1:1", Season = 1, Episode = 1, Title = "Earthfall", Released = "2010-06-16" } }
            },
            new Meta
            {
                Id = "tt0147753",
                Name = "Captain Z-Ro",
                Description = "From his secret laboratory, Captain Z-Ro and his associates use their time machine, the ZX-99, to learn from the past and plan for the future.",
                ReleaseInfo = "1955-1956",
                Genres = new[] { "Sci-Fi" },
                Videos = new[]
                {
                    new Video { Id = "tt0147753:1:1", Season = 1, Episode = 1, Title = "Christopher Columbus", Released = "1955-12-18" },
                    new Video { Id = "tt0147753:1:1", Season = 1, Episode = 2, Title = "Daniel Boone", Released = "2010-06-25" },
                }
            }
        };
        
        return metas;
    }

    private static readonly Meta[] Catalog = Metas
        .Select(x =>
        {
            x.Type = x.Videos != null ? MetaType.Series : MetaType.Movie;
            if(string.IsNullOrEmpty(x.Poster))
                x.Poster = string.Format(MetahubUrl, x.Id);
            return x;
        })
        .ToArray();


    public ValueTask<Manifest> GetManifestAsync(AddonProviderName? providerName = null, CancellationToken cancellationToken = default)
    {
        return ValueTask.FromResult(providerName != null ? CreateManifest(providerName.Name) : Manifest);
    }

    public ValueTask<Meta[]> GetCatalogMetaAsync(string type, string id, IEnumerable<ExtraValue> extras, AddonProviderName? providerName = null, CancellationToken cancellationToken = default)
    {
        return ValueTask.FromResult(Catalog.Where(x => string.Equals(x.Type.ToString(), type, StringComparison.OrdinalIgnoreCase)).ToArray());
    }
}
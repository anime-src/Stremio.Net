using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Stremio.Net.Models;
using Stremio.Net.Models.Catalogs;
using Stremio.Net.Models.Metadata;

namespace Stremio.Net.Addons;

public interface IAddonProvider
{
    ValueTask<Manifest> GetManifestAsync(AddonProviderName? providerName = null, CancellationToken cancellationToken = default);

    ValueTask<Meta[]> GetCatalogMetaAsync(string type, string id, IEnumerable<ExtraValue> extras, AddonProviderName? providerName = null, CancellationToken cancellationToken = default);
}
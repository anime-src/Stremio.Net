using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Stremio.Net.Extensions;

public static class ResourceExtensions
{
    public static string GetManifestResourceFile(this Assembly assembly, string fileName)
    {
        string resourceName = assembly.GetManifestResourceName(fileName);
        using Stream? stream = assembly.GetManifestResourceStream(resourceName);
        if(stream is null)
            throw new FileNotFoundException($"Embedded file '{fileName}' could not be found in assembly '{assembly.FullName}'.", fileName);
        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }

    private static string GetManifestResourceName(this Assembly assembly, string fileName)
    {
        string? name = assembly.GetManifestResourceNames().SingleOrDefault(n => n.EndsWith(fileName, StringComparison.InvariantCultureIgnoreCase));

        if (string.IsNullOrEmpty(name))
        {
            throw new FileNotFoundException($"Embedded file '{fileName}' could not be found in assembly '{assembly.FullName}'.", fileName);
        }

        return name;
    }
}
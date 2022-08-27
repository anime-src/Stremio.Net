using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Stremio.Net.Extensions
{
    public static class ResourceExtensions
    {
        public static string GetManifestResourceFile(this Assembly assembly, string fileName)
        {
            using Stream stream = GetManifestResourceFileStream(assembly, fileName);
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
    
        public static async Task<string> GetManifestResourceFileAsync(this Assembly assembly, string fileName)
        {
            await using Stream stream = GetManifestResourceFileStream(assembly, fileName);
            using var reader = new StreamReader(stream);
            return await reader.ReadToEndAsync();
        }

        public static Stream GetManifestResourceFileStream(this Assembly assembly, string fileName)
        {
            string resourceName = assembly.GetManifestResourceName(fileName);
            Stream? stream = assembly.GetManifestResourceStream(resourceName);
            if(stream is null)
                throw new FileNotFoundException($"Embedded file '{fileName}' could not be found in assembly '{assembly.FullName}'.", fileName);
            if (stream.CanSeek) stream.Seek(0, SeekOrigin.Begin);
            return stream;
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
}
using System;
using System.Collections.Generic;

namespace Stremio.Net.Models.Catalogs
{
    public class ExtraValue
    {
        public ExtraValue(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }
        public string Value { get; }

        public static IEnumerable<ExtraValue> Parse(string? extraProps)
        {
            if (string.IsNullOrEmpty(extraProps))
                yield break;

            string[] propPair = extraProps.Split('&', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            foreach (var pair in propPair)
            {
                var pairValueArray = pair.Split('=', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
               
                if (pairValueArray.Length <= 1) 
                    continue;
                
                string name = pairValueArray[0];
                string value = pairValueArray[1];
                
                if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(value))
                {
                    yield return new ExtraValue(name, value);
                }
            }
        }
    }
}
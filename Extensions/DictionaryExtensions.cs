using System;
using System.Collections.Concurrent;

namespace Stremio.Net.Extensions
{
    public static class DictionaryExtensions
    {
        public static TValue GetOrAddIfNotNull<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dictionary, object locker, TKey key, Func<TKey, TValue> valueFactory) where TValue : class where TKey : notnull
        {
            if (!dictionary.TryGetValue(key, out TValue? value))
            {
                lock (locker)
                {
                    value = dictionary.GetOrAdd(key, valueFactory);
                    if (value == null) dictionary.TryRemove(key, out value);
                } 
            }
            return value;
        }
    }
}
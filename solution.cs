using System;
using System.Collections.Concurrent;

public class Cache<TKey, TValue>
{
    private ConcurrentDictionary<TKey, CacheItem> _cache = new ConcurrentDictionary<TKey, CacheItem>();
}
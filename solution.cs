Here is a simple implementation of a cache with a TTL (time to live) per key in C#:

```C#
using System;
using System.Collections.Concurrent;
using System.Threading;

public class Cache<TKey, TValue>
{
    private ConcurrentDictionary<TKey, CacheItem> _cache = new ConcurrentDictionary<TKey, CacheItem>();

    public void Add(TKey key, TValue value, TimeSpan ttl)
    {
        if (_cache.ContainsKey(key))
        {
            _cache[key].CancelToken();
        }

        var item = new CacheItem(value, ttl, () => _cache.TryRemove(key, out _));
        _cache[key] = item;
    }

    public TValue Get(TKey key)
    {
        if (_cache.TryGetValue(key, out var item))
        {
            return item.Value;
        }

        throw new ArgumentException("No item with the provided key found in the cache.");
    }

    private class CacheItem
    {
        public TValue Value { get; }
        private Timer _timer;

        public CacheItem(TValue value, TimeSpan ttl, Action expiryAction)
        {
            Value = value;
            _timer = new Timer(_ => expiryAction(), null, ttl, TimeSpan.FromMilliseconds(-1));
        }

        public void CancelToken()
        {
            _timer.Dispose();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        var cache = new Cache<string, int>();
        cache.Add("one", 1, TimeSpan.FromSeconds(5));
        Console.WriteLine(cache.Get("one"));
        Thread.Sleep(6000);
        try
        {
            Console.WriteLine(cache.Get("one"));
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
```

This program creates a cache where each key-value pair has a TTL. After the TTL expires, the key-value pair is automatically removed from the cache. If you try to access a key that has been removed, it throws an exception.
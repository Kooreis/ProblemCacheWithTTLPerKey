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
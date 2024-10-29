Here is a simple implementation of a cache with a TTL (time to live) per key in Java:

```java
import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.Executors;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.TimeUnit;

public class TTLCache<K, V> {
    private final Map<K, V> cache = new ConcurrentHashMap<>();
    private final Map<K, Long> expiryMap = new ConcurrentHashMap<>();
    private final ScheduledExecutorService executorService = Executors.newScheduledThreadPool(1);

    public TTLCache() {
        executorService.scheduleAtFixedRate(() -> {
            long currentTimeMillis = System.currentTimeMillis();
            expiryMap.forEach((k, v) -> {
                if (currentTimeMillis > v) {
                    remove(k);
                }
            });
        }, 1, 1, TimeUnit.SECONDS);
    }

    public void put(K key, V value, long ttl) {
        cache.put(key, value);
        expiryMap.put(key, System.currentTimeMillis() + ttl);
    }

    public V get(K key) {
        return cache.get(key);
    }

    public void remove(K key) {
        cache.remove(key);
        expiryMap.remove(key);
    }

    public static void main(String[] args) throws InterruptedException {
        TTLCache<String, String> cache = new TTLCache<>();
        cache.put("key1", "value1", 5000);
        cache.put("key2", "value2", 10000);
        System.out.println(cache.get("key1"));
        System.out.println(cache.get("key2"));
        Thread.sleep(6000);
        System.out.println(cache.get("key1"));
        System.out.println(cache.get("key2"));
    }
}
```

In this code, we have a `TTLCache` class that uses two `ConcurrentHashMap` instances: one for storing the actual key-value pairs (`cache`) and another for storing the expiry times for each key (`expiryMap`). The `ScheduledExecutorService` is used to schedule a task that runs every second and removes any expired entries from the cache. The `put` method is used to add entries to the cache and set their expiry times. The `get` method is used to retrieve entries from the cache. The `remove` method is used to remove entries from the cache. The `main` method demonstrates how to use the `TTLCache`.
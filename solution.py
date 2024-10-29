Here is a Python console application that implements a cache with a TTL (time to live) per key using the built-in `time` and `threading` modules.

```python
import time
import threading

class Cache:
    def __init__(self):
        self.cache = {}
        self.ttl = {}

    def set(self, key, value, ttl):
        self.cache[key] = value
        self.ttl[key] = ttl
        timer = threading.Timer(ttl, self.delete, args=[key])
        timer.start()

    def get(self, key):
        if key in self.cache:
            return self.cache[key]
        return None

    def delete(self, key):
        if key in self.cache:
            del self.cache[key]
            del self.ttl[key]

def main():
    cache = Cache()
    cache.set('key1', 'value1', 5)
    print(cache.get('key1'))  # prints: value1
    time.sleep(6)
    print(cache.get('key1'))  # prints: None

if __name__ == "__main__":
    main()
```

In this application, the `Cache` class has a `set` method that sets a key-value pair in the cache and starts a timer with the given TTL. When the timer expires, the key-value pair is automatically deleted from the cache. The `get` method retrieves the value for a key from the cache, and the `delete` method manually deletes a key-value pair from the cache. The `main` function demonstrates how to use the `Cache` class.
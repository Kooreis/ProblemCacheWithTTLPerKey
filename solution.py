def set(self, key, value, ttl):
        self.cache[key] = value
        self.ttl[key] = ttl
        timer = threading.Timer(ttl, self.delete, args=[key])
        timer.start()
import time
import threading

class Cache:
    def __init__(self):
        self.cache = {}
        self.ttl = {}
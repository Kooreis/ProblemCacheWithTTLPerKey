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
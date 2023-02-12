namespace FMFT.Web.Client.Services.Pages
{
    public abstract class PageServiceBase
    {
        private Dictionary<string, CacheItem> cache { get; } = new();

        protected T GetOrSetInCache<T>(string key, Func<T> setter, TimeSpan expireTime)
        {
            CacheItem cacheItem;

            if (cache.TryGetValue(key, out cacheItem))
            {
                if (cacheItem.ExpireDate > DateTime.Now)
                {
                    return (T)cacheItem.Object;
                }
            }

            cacheItem = new();
            cacheItem.Object = setter.Invoke();
            cacheItem.ExpireDate = DateTime.Now.Add(expireTime);

            cache[key] = cacheItem;

            return (T)cacheItem.Object;
        }

        protected async Task<T> GetOrSetInCacheAsync<T>(string key, Func<Task<T>> setter, TimeSpan expireTime)
        {
            CacheItem cacheItem;

            if (cache.TryGetValue(key, out cacheItem))
            {
                if (cacheItem.ExpireDate > DateTime.Now)
                {
                    return (T)cacheItem.Object;
                }
            }

            cacheItem = new();
            cacheItem.Object = await setter.Invoke();
            cacheItem.ExpireDate = DateTime.Now.Add(expireTime);

            cache[key] = cacheItem;

            return (T)cacheItem.Object;
        }

        private record CacheItem
        {
            public object Object { get; set; }
            public DateTime ExpireDate { get; set; }
        }
    }
}

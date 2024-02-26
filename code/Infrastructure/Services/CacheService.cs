using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text;

namespace Infrastructure.Services;

public class CacheService : ICacheService
{
    private readonly IDistributedCache _cache;

    public CacheService(IDistributedCache cache)
    {
        _cache = cache;
    }

    public T Get<T>(string key)
    {
        var value = _cache.GetString(key);

        if (value != null)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

        return default;
    }

    public T Set<T>(string key, T value)
    {
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(10),
            SlidingExpiration = TimeSpan.FromHours(1)


        };
        _cache.Remove(key);
        //  var res = JsonConvert.SerializeObject(value);
        _cache.SetString(key, JsonConvert.SerializeObject(value), options);

        return value;
    }
}

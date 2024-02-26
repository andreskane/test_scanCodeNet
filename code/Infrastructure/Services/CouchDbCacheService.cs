using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services;

public interface ICouchDbCacheService
{
    Task<T> GetAsync<T>(string key);
    Task SetAsync<T>(string key, T value, TimeSpan? expiry = null);
    
}
public class CouchDbCacheService : ICouchDbCacheService
{
    private readonly IMemoryCache _memoryCache;
    // HttpClient u otra forma de acceder a CouchDB

    public CouchDbCacheService(IMemoryCache memoryCache /*, HttpClient o dependencias de CouchDB */)
    {
        _memoryCache = memoryCache;
        // Inicializa el cliente de CouchDB
    }

    public async Task<T> GetAsync<T>(string key)
    {
        // Primero verifica el caché en memoria, luego CouchDB si es necesario

    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
    {
        // Establece el valor en el caché en memoria y opcionalmente en CouchDB
    
    }
}
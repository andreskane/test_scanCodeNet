using Autofac;
using ConnectureOS.Framework.Caching.MemCache;
using Couchbase.Extensions.Caching;
using Infrastructure.AutofacModules;
using Microsoft.Extensions.Caching.Memory;

namespace ApiOS;

public static class ServicesExtensions
{
    public static IServiceCollection AddCache(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMemoryCache();
        var serviceProvider = services.BuildServiceProvider();
        var children = configuration.GetSection("Caching").GetChildren();
        var cachingConfiguration = children.ToDictionary(child => child.Key, child => TimeSpan.Parse(child.Value));
        var memoryCache = serviceProvider.GetService<IMemoryCache>();
        ICacheStore cacheStore = new MemoryCacheStore(memoryCache, cachingConfiguration);

        services.AddSingleton(cacheStore);
        return services;
    }
    public static IServiceCollection AddCacheCouchDB(this IServiceCollection services, IConfiguration configuration)
    {


        services.AddDistributedMemoryCache();
        services.AddDistributedCouchbaseCache(
            configuration["Caching:bucket"].Trim()
            , opt => { });


        return services;

    }

    public static void AddConfiguration(this IServiceCollection services, IConfiguration configuration)
    {

    }
    public static void ConfigureContainer(ContainerBuilder builder)
    {

        builder.RegisterModule(new MediatorModule());
        builder.RegisterModule(new ApplicationModule());
    }
}

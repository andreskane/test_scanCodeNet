using Application.Dto.Params.ListValue;
using Application.Interfaces.Repositories;
using ConnectureOS.Framework.Caching.MemCache;
using Domain.Entities.ListAggregate;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class ListValueRepository : GenericRepositoryAsync<ListValue>, IListValueRepository
{
    public readonly ApplicationDbContext _dataContext;
    private readonly ICacheStore _cacheStore;



    public ListValueRepository(ApplicationDbContext dbContext, ICacheStore cacheStore


        ) : base(dbContext)
    {
        _dataContext = dbContext;
        _cacheStore = cacheStore;


    }
    private IQueryable<ListValue> UntrackedSet() => _dataContext.Set<ListValue>().AsNoTracking();
    public IEnumerable<ListValue> GetAll()
    {

        var result = UntrackedSet();


        return (IEnumerable<ListValue>)result;


    }

    public async Task<IList<ListValue>> GetFilteredAsync(ListValueFilter filter, CancellationToken cancellationToken)
    {




        var KeyCache = string.Format("{0}{1}", "GetFilteredAsync", filter.ListId);
        var result = new List<ListValue>();
        var StoreCache = this._cacheStore.Get(new AllListValueByListCacheKey(KeyCache));

        if (StoreCache != null)
        {
            return (IList<ListValue>)StoreCache;
        }
        else
        {

            var query = _dataContext.Set<ListValue>().Include(x => x.ListDefinition)
                                                .ThenInclude(x => x.ListsTenantsWorkflows)
                                                .AsQueryable()
                                                .AsNoTracking();

            if (filter.ListId != null)
            {
                query = query.Where(x => x.ListDefinition.Id == filter.ListId);
            }

            if (filter.DynamicFormId != null)
            {
                query = query.Where(x => x.ListDefinition.ListsTenantsWorkflows.Any(x => x.WorkflowId == filter.DynamicFormId));
            }

            if (filter.TenantId != null)
            {
                query = query.Where(x => x.ListDefinition.ListsTenantsWorkflows.Any(x => x.TenantId == filter.TenantId));
            }

            var res = await query.AsNoTracking().ToListAsync(cancellationToken);
            _cacheStore.Add(res, new AllListValueByListCacheKey(KeyCache), "default");

            return res;

        }

    }

    public async Task<IList<ListValue>> GetByListIdAsync(Int64 listId, CancellationToken cancellationToken)
    {
        return await _dataContext.Set<ListValue>().Where(x => x.ListId == listId).AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<IList<ListValue>> GetByListNameAsync(string ListName, CancellationToken cancellationToken)
    {
        var query = await UntrackedSet().Where(x => x.ListDefinition.ListName == ListName).ToListAsync();
        return query;

    }


    public async Task<IList<ListValue>> GetByListNameAsync(string ListName, string Key, CancellationToken cancellationToken)
    {
        var KeyCache = string.Format("{0}{1}", "GetByListNameAsync", ListName);


        var StoreCache = this._cacheStore.Get(new AllListValueByListCacheKey(KeyCache));
        if (StoreCache != null)
        {

            var query = StoreCache.Where(x => x.Key == Key).ToList();
            return query;
        }
        else
        {
            var query = UntrackedSet().Where(x => x.ListDefinition.ListName == ListName).ToList();

            _cacheStore.Add(query, new AllListValueByListCacheKey(KeyCache), "default");

            var result = query.Where(x => x.Key == Key).ToList();
            return result;
        }

    }

    public async Task<IList<ListValue>> GetKeyByListNameAsync(string ListName, string Value, CancellationToken cancellationToken)
    {
        var KeyCache = string.Format("{0}{1}", "GetKeyByListNameAsync", ListName);



        var StoreCache = this._cacheStore.Get(new AllListValueByListCacheKey(KeyCache));
        if (StoreCache != null)
        {
            if (Value == null)
            {
                var query = StoreCache.Where(x => x.Key == null).ToList();
                return query;
            }
            else
            {
                var query = StoreCache.Where(x => x.Key == Value).ToList();
                return query;
            }

        }
        else
        {
            var query = UntrackedSet().Where(x => x.ListDefinition.ListName == ListName).ToList();


            _cacheStore.Add(query, new AllListValueByListCacheKey(KeyCache), "default");
            var result = query.Where(x => x.Value.Contains(Value)).ToList();
            return result;
        }

    }

    public async Task<IEnumerable<ListDefinition>> GetAllListDefinitionAsync()
    {
        var result = await _dataContext.Set<ListDefinition>().AsNoTracking().ToListAsync();
        return result;
    }

    public async Task<int> DeleteListValueAsync(ListValue entity, CancellationToken cancellationToken)
    {
        entity.IsDeleted = true;
        entity.Deleted = DateTime.UtcNow;
        entity.DeletedBy = "User";

        _dataContext.Entry(entity).State = EntityState.Modified;


        _cacheStore.RemoveByPartialKey<AllListValueByListCacheKey>("ListValue_");

        return await _dataContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<ListValue> UpdateListValueAsync(ListValue entity, CancellationToken cancellationToken)
    {
        _dataContext.Entry(entity).State = EntityState.Modified;
        await _dataContext.SaveChangesAsync(cancellationToken);
        _cacheStore.RemoveByPartialKey<AllListValueByListCacheKey>("ListValue_");
        return entity;
    }

    public async Task<List<ListValue>> AddRangeListValueAsync(List<ListValue> entity, CancellationToken cancellationToken = default)
    {



        await _dataContext.Set<ListValue>().AddRangeAsync(entity);
        await _dataContext.SaveChangesAsync(cancellationToken);
        _cacheStore.RemoveByPartialKey<AllListValueByListCacheKey>("ListValue_");

        return entity;
    }
}
public class AllListValueByListCacheKey : ICacheKey<List<ListValue>>
{
    public AllListValueByListCacheKey(string param)
    {
        CacheKey = string.Format("{0}{1}", "ListValue_", param);
    }

    public string CacheKey { get; private set; }
}
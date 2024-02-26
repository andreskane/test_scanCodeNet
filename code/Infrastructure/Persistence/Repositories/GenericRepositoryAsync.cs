using Application.Interfaces.Repositories;
using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class GenericRepositoryAsync<T> : IGenericRepositoryAsync<T> where T : BaseAuditableEntity
{
    private readonly ApplicationDbContext _dbContext;

    public GenericRepositoryAsync(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public virtual async Task<T> GetByIdAsync(Int64 id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }

    public async Task<IReadOnlyList<T>> GetPagedReponseAsync(int pageNumber, int pageSize)
    {
        return await _dbContext
            .Set<T>()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync();
    }



    public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _dbContext.Set<T>().AddAsync(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<List<T>> AddRangeAsync(List<T> entity, CancellationToken cancellationToken = default)
    {
        await _dbContext.Set<T>().AddRangeAsync(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<int> DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        entity.IsDeleted = true;
        entity.Deleted = DateTime.UtcNow;
        entity.DeletedBy = "User";

        _dbContext.Entry(entity).State = EntityState.Modified;
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
    public async Task<int> RealDeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        entity.IsDeleted = true;
        entity.Deleted = DateTime.UtcNow;
        entity.DeletedBy = "User";

        _dbContext.Entry(entity).State = EntityState.Deleted;
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await _dbContext
             .Set<T>()
             .ToListAsync();
    }
}
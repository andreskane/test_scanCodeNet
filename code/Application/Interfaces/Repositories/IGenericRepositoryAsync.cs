namespace Application.Interfaces.Repositories;

public interface IGenericRepositoryAsync<T> where T : class
{
    Task<T> GetByIdAsync(long id);
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<IReadOnlyList<T>> GetPagedReponseAsync(int pageNumber, int pageSize);
    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
    Task<List<T>> AddRangeAsync(List<T> entity, CancellationToken cancellationToken = default);
    Task<T> UpdateAsync(T entity, CancellationToken cancellationToken);
    Task<int> DeleteAsync(T entity, CancellationToken cancellationToken = default);
    Task<int> RealDeleteAsync(T entity, CancellationToken cancellationToken = default);
}

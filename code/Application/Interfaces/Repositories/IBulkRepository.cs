using Domain.Entities;
using Domain.Enums;


namespace Application.Interfaces.Repositories
{
    public interface IBulkRepository : IGenericRepositoryAsync<BulkProcess>
    {
        IEnumerable<BulkProcess> GetAll();
        Task AsignDynamicForms(long BulckId, List<Int64> ListIds, CancellationToken cancellationToken);

        Task<IList<BulkProcess>> GetBulkProcessesAsyncByStatus(ProcessStatusEnum Status, CancellationToken cancellationToken);
        Task<IList<BulkProcess>> GetBulkProcessesAsyncExcludeDraft(CancellationToken cancellationToken);
        Task<BulkProcess> GetBulkProcessByIdAsync(long BulkProcessId, CancellationToken cancellationToken);

        Task<IList<BulckComponent>> GetBulkComponentsByBulkProcessId(Int64 BulkProcessId, CancellationToken cancellationToken);
    }
}

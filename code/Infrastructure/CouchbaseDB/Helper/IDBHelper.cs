namespace Infrastructure.CouchbaseDB.Helper
{
    public interface IDBHelper
    {
        Task<IList<EntityType>> ExecutePreparedQueryAsync<EntityType>(string QueryName,
                                                   string Query,
                                                   KeyValuePair<string, object>[] parameters);
    }
}
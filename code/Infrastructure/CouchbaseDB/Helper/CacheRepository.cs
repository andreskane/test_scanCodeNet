using Couchbase.Query;

namespace Infrastructure.CouchbaseDB.Helper
{
    public interface ICacheRepository
    {

        Task<List<String>> GetCacheKeys(string KeyDocument);
        Task<QueryStatus> DeleteCacheKeys(string KeyDocument);
    }
    public class CacheRepository : ICacheRepository
    {
        private readonly ICouchbaseService _couchbaseService;
        public CacheRepository(
                        ICouchbaseService couchbaseService
                      )
        {
            _couchbaseService = couchbaseService;
        }

        public async Task<List<string>> GetCacheKeys(string KeyDocument)
        {

            var result = new List<string>();

            var res = await _couchbaseService.Cluster.QueryAsync<dynamic>($"select meta().id from `drx-cos-cache`.`_default`.`_default` data where meta().id like '{KeyDocument}%' order by meta().id");

            var tempRes = await res.Rows.ToListAsync();

            foreach (var item in tempRes)
            {
                result.Add(item.ToString());
            }

            return result;


        }
        public async Task<QueryStatus> DeleteCacheKeys(string KeyDocument)
        {


            var res = await _couchbaseService.Cluster.QueryAsync<dynamic>($"delete from `drx-cos-cache`.`_default`.`_default` data where meta().id like '{KeyDocument}%' ");
            return res.MetaData.Status;

        }



    }
}

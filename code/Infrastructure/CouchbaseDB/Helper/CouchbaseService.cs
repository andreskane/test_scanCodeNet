using Couchbase;
using Couchbase.Extensions.DependencyInjection;
using Couchbase.KeyValue;

namespace Infrastructure.CouchbaseDB.Helper
{
    public interface ICouchbaseService
    {
        ICluster Cluster { get; }
        IBucket SuscriptionsBucket { get; }
        IBucket RulesBucket { get; }
        ICouchbaseCollection DefaultCollection { get; }


        public Task<ICouchbaseCollection> TenantCollection(string tenant, string collection);
    }

    public static class StringExtension
    {
        public static string DefaultIfEmpty(this string str, string defaultValue)
            => string.IsNullOrWhiteSpace(str) ? defaultValue : str;
    }

    public class CouchbaseService : ICouchbaseService
    {

        public ICluster Cluster { get; private set; }
        public IBucket SuscriptionsBucket { get; private set; }
        public IBucket RulesBucket { get; private set; }
        public ICouchbaseCollection DefaultCollection { get; private set; }

        public CouchbaseService(IClusterProvider clusterProvider)
        {

            try
            {
                var task = Task.Run(async () =>
                {
                    var cluster = await clusterProvider.GetClusterAsync();
                    //drx-cos-rules
                    Cluster = cluster;
                    SuscriptionsBucket = await Cluster.BucketAsync("drx-cos-workflow");
                    RulesBucket = await Cluster.BucketAsync("drx-cos-rules");

                    var SuscriptionsScope = await SuscriptionsBucket.ScopeAsync("_default");


                    DefaultCollection = await SuscriptionsScope.CollectionAsync("_default");
                });
                task.Wait();
            }
            catch (AggregateException ae)
            {

                ae.Handle((x) => throw x);
            }
        }

        public async Task<ICouchbaseCollection> TenantCollection(string tenant, string collection)
        {
            var tenantScope = await SuscriptionsBucket.ScopeAsync(tenant);
            var tenantCollection = await tenantScope.CollectionAsync(collection);
            return tenantCollection;
        }


    }





}

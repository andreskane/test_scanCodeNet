using Couchbase;
using Couchbase.Extensions.DependencyInjection;

namespace Infrastructure.CouchbaseDB.Helper;

public interface IRulesBucketProvider : INamedBucketProvider
{
}
public interface IDynamicFormBucketProvider : INamedBucketProvider
{
   
}

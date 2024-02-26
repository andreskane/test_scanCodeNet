using Application.Interfaces.Documental;
using Domain.Entities.RulesAggregate;
using Infrastructure.CouchbaseDB.Helper;

namespace Infrastructure.CouchbaseDB.Repositories
{
    public class DocRulesRepository : IDocRulesRepository
    {

        private readonly ICouchbaseService _couchbaseService;
        private readonly IRulesBucketProvider _rulesBucketProvider;
        public DocRulesRepository(
             IRulesBucketProvider rulesBucketProvider,
                        ICouchbaseService couchbaseService
                       )
        {
            _rulesBucketProvider = rulesBucketProvider;
            _couchbaseService = couchbaseService;
        }

        public async Task<string> InsertDynamicForm(RootRules rootRules)
        {


            var collection = _couchbaseService.RulesBucket.Collection("_default");
            var KeyDocument = Guid.NewGuid().ToString();

            await collection.InsertAsync(KeyDocument, rootRules);
            return KeyDocument;
        }
        public async Task<string> UpdateDynamicForm(RootRules rootRules, String KeyDocument)
        {
            var collection = _couchbaseService.RulesBucket.Collection("_default");
            await collection.UpsertAsync(KeyDocument, rootRules);


            return KeyDocument;
        }

        public async Task<RootRules> GetDynamicFormByKey(string KeyDocument)
        {
            var collection = _couchbaseService.RulesBucket.Collection("_default");
            var res = await collection.GetAsync(KeyDocument);
            var result = res.ContentAs<RootRules>();
            return (RootRules)result;
        }

    }
}

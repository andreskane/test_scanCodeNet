
using Application.Interfaces.Documental;
using Couchbase;
using Couchbase.Extensions.DependencyInjection;
using Domain.Entities.Layout;
using Infrastructure.CouchbaseDB.Helper;
using Newtonsoft.Json;

namespace Infrastructure.CouchbaseDB.Repositories
{
    public class DocDynamicFormRepository : IDocDynamicFormRepository
    {
        private readonly IDynamicFormBucketProvider _bucketProvider;


        private readonly ICouchbaseService _couchbaseService;

        public DocDynamicFormRepository(
                  IDynamicFormBucketProvider  bucketProvider ,

        ICouchbaseService couchbaseService
            )
        {
            _bucketProvider = bucketProvider;
            _couchbaseService = couchbaseService;
        }

        public Task<string> DuplicateDynamicForm(string KeyDocument)
        {
            var data = GetDynamicFormByKey(KeyDocument).Result;

            var key = InsertDynamicForm(data);
            return key;
        }

        public async Task<RootObject> GetDynamicFormByKey(string KeyDocument)
        {
            var collection = _couchbaseService.DefaultCollection;
            var res = await collection.GetAsync(KeyDocument);
            var result = res.ContentAs<RootObject>();
            return (RootObject)result;
        }

        public async Task<DocLayoutTemplate> GetTemplateByKey(string KeyDocument)
        {
            var collection = _couchbaseService.DefaultCollection;
            var res = await collection.GetAsync(KeyDocument);
            var result = res.ContentAs<DocLayoutTemplate>();
            return (DocLayoutTemplate)result;
        }

        public async Task<string> InsertDynamicForm(RootObject layout)
        {

            var collection = _couchbaseService.DefaultCollection;
            var key = Guid.NewGuid().ToString();
            await collection.InsertAsync(key, layout);
            return key;
        }

        public async Task<string> InsertTemplate(DocLayoutTemplate layout)
        {
            var collection = _couchbaseService.DefaultCollection;
            var key = Guid.NewGuid().ToString();
            await collection.InsertAsync(key, layout);
            return key;
        }

        public async Task<string> UpdateDynamicForm(RootObject layout, String KeyDocument)
        {
            if (KeyDocument == null)
            {
                KeyDocument = "";

            }

            if (KeyDocument == "")
            {
                KeyDocument = await InsertDynamicForm(layout);

            }
            else
            {
                var collection = _couchbaseService.DefaultCollection;

                await collection.UpsertAsync(KeyDocument, layout);

            }

            return KeyDocument;

        }

        public async Task<string> UpdateTemplate(DocLayoutTemplate layout, string KeyDocument)
        {
            if (KeyDocument == null)
            {
                KeyDocument = "";

            }

            if (KeyDocument == "")
            {
                KeyDocument = await InsertTemplate(layout);

            }
            else
            {
                var collection = _couchbaseService.DefaultCollection;

                await collection.UpsertAsync(KeyDocument, layout);

            }

            return KeyDocument;
        }

        public async Task<List<RootObject>> GetIDDocumentsByIDTemplate(Int64 templateID)
        {

            var rootObjects = new List<RootObject>();
 
          
            try
            {
                var bucket = await _bucketProvider.GetBucketAsync();
                var query = $"SELECT d.* FROM `drx-cos-workflow`.`_default`.`_default` AS d WHERE ANY p IN d.pages SATISFIES  (ANY wt IN p.workflowTable SATISFIES wt.templateId = {templateID} END)  END";

                var result = await bucket.Cluster.QueryAsync<RootObject>(query);
               
                await foreach (var row in result.Rows  )
                {
                    rootObjects.Add(row);
                }
            }
            catch (CouchbaseException ex)
            {
                
              //CouchBase Error
            }
            catch (JsonException ex)
            {
                //   deserialización JSON
            }



            return rootObjects;


        }


    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Newtonsoft.Json;
using WebApplication1.Properties;

namespace WebApplication1.Repository
{
    public class AzureNoSqlRepository<TKey, TObject> : IRepository<TKey, TObject>
    {
        private static DocumentClient _client;

        private static readonly string EndpointUrl = Settings.Default.endpointUrl;
        private static readonly string AuthorizationKey = Settings.Default.authorizationKey;

        private static readonly string DatabaseId = Settings.Default.databaseId;
        private static readonly string CollectionId = Settings.Default.collectionId;

        public AzureNoSqlRepository()
        {
            _client = new DocumentClient(new Uri(EndpointUrl), AuthorizationKey);

            Init();
        }

        public void Add(TKey id, TObject aGuitar)
        {
            var collectionLink = UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId);
              _client.CreateDocumentAsync(collectionLink, aGuitar).Wait();
        }

        public void Delete(TKey id)
        {
            _client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, id as string)).Wait();

        }

        public  void Update(TKey id, TObject aGuitar)
        {
            var collectionLink = UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId);
           _client.UpsertDocumentAsync(collectionLink, aGuitar).Wait();
           
        }

        public Maybe<TObject> Get(TKey id)
        {
            var collectionLink = UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId);
            var query = _client.CreateDocumentQuery<TObject>(collectionLink)
                .Where(so => ((IStorable)so).id == id as string)
                .AsEnumerable()
                .FirstOrDefault();
            if (query == null) return new Maybe<TObject>();
            return new Maybe<TObject>(query);
        }


        public async Task Clear()
        {
            var col = await GetOrCreateCollectionAsync(DatabaseId, CollectionId);
            try
            {
                var docs = _client.CreateDocumentQuery(col.DocumentsLink);

                foreach (var doc in docs)
                {
                   _client.DeleteDocumentAsync(doc.SelfLink).Wait();
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                throw;
            }
        }

        public IEnumerator<TObject> GetEnumerator()
        {
            var list = new List<TObject>();
            var result = GetDocs();

            var enu = result.Result;


            foreach (var re in enu)
            { 
                var tobject = JsonConvert.DeserializeObject<TObject>(re.ToString());
                list.Add(tobject);
            }
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        public async Task<FeedResponse<object>> GetDocs()
        {
            var col = await GetOrCreateCollectionAsync(DatabaseId, CollectionId);
            var docs = _client.ReadDocumentFeedAsync(col.DocumentsLink, new FeedOptions { MaxItemCount = 1000, });

            return docs.Result;
        }


        private static void Init()
        {
            GetOrCreateDatabaseAsync(DatabaseId).Wait();
            GetOrCreateCollectionAsync(DatabaseId, CollectionId).Wait();
        }

        private static async Task<Database> GetOrCreateDatabaseAsync(string databaseId)
        {
            var databaseUri = UriFactory.CreateDatabaseUri(databaseId);

            var database = _client.CreateDatabaseQuery()
                .Where(db => db.Id == databaseId)
                .ToArray()
                .FirstOrDefault();

            if (database == null)
            {
                database = await _client.CreateDatabaseAsync(new Database { Id = databaseId });
            }

            return database;
        }

        private static async Task<DocumentCollection> GetOrCreateCollectionAsync(string databaseId, string collectionId)
        {
            var databaseUri = UriFactory.CreateDatabaseUri(databaseId);

            var collection = _client.CreateDocumentCollectionQuery(databaseUri)
                .Where(c => c.Id == collectionId)
                .AsEnumerable()
                .FirstOrDefault();

            if (collection == null)
            {
                collection =
                    await _client.CreateDocumentCollectionAsync(databaseUri, new DocumentCollection { Id = collectionId });
            }

            return collection;
        }

        

        void IRepository<TKey, TObject>.ClearAll()
        {
            throw new NotImplementedException();
        }
    }
}
using LoopApi.Interfaz;
using LoopApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LoopApi.Services
{
    public class StoreService : IStore
    {
        private readonly IMongoCollection<Store> _storeCollection;

        public StoreService(IOptions<StoreDatabaseSettings> storeDatabaseSettings)
        {
            MongoClient mongoClient = new MongoClient(
            storeDatabaseSettings.Value.ConnectionString);

            IMongoDatabase mongoDatabase = mongoClient.GetDatabase(
                storeDatabaseSettings.Value.DatabaseName);

            _storeCollection = mongoDatabase.GetCollection<Store>(
                storeDatabaseSettings.Value.StoreCollectionName);
        }
               
        public List<Store> Get()
        {
            return _storeCollection.Find(p => true).ToList();
        }

        public Store Get(long id)
        {
            return _storeCollection.Find(p => p.id == id).FirstOrDefault();
        }

        public void Post(Store store)
        {
            _storeCollection.InsertOne(store);
        }

        public void Put(Store store)
        {
            _storeCollection.ReplaceOne(p => p.id == store.id, store);
        }

        public void Delete(long id)
        {
            _storeCollection.DeleteOne(p => p.id == id);
        }
    }
}

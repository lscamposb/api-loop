using LoopApi.Interfaz;
using LoopApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LoopApi.Services
{
    public class StoreCategorieService : IStoreCategorie
    {
        private readonly IMongoCollection<StoreCategories> _storeCategorieCollection;

        public StoreCategorieService(IOptions<StoreCategoriesDatabaseSettings> storeCategorieDatabaseSettings)
        {
            MongoClient mongoClient = new MongoClient(
            storeCategorieDatabaseSettings.Value.ConnectionString);

            IMongoDatabase mongoDatabase = mongoClient.GetDatabase(
                storeCategorieDatabaseSettings.Value.DatabaseName);

            _storeCategorieCollection = mongoDatabase.GetCollection<StoreCategories>(
                storeCategorieDatabaseSettings.Value.StoreCategoriesCollectionName);
        }

        public int ObtenerUltimoRegistro()
        {
            List<StoreCategories> lStore = _storeCategorieCollection.Find(p => true).ToList();

            if (lStore.Count == 0)
                return 1;

            return lStore.OrderByDescending(p => p.id).First().id + 1;
        }

        public List<StoreCategories> Get()
        {
            return _storeCategorieCollection.Find(p => true).ToList().OrderBy(p => p.id).ToList();
        }

        public StoreCategories Get(long id)
        {
            return _storeCategorieCollection.Find(p => p.id == id).FirstOrDefault();
        }

        public void Post(StoreCategories storeCategories)
        {
            _storeCategorieCollection.InsertOne(storeCategories);
        }

        public void Update(StoreCategories storeCategories)
        {
            _storeCategorieCollection.ReplaceOne(p => p.id == p.id, storeCategories);
        }

        public void Delete(long id)
        {
            _storeCategorieCollection.DeleteOne(p => p.id == id);
        }
    }
}

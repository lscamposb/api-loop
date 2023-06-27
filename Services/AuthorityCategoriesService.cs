using LoopApi.DTO;
using LoopApi.Interfaz;
using LoopApi.Models;
using LoopApi.Util;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LoopApi.Services
{
    public class AuthorityCategoriesService : IAuthorityCategories
    {
        private readonly IMongoCollection<AuthorityCategories> _authorityCollection;

        public AuthorityCategoriesService(IOptions<AuthorityCategoriesDatabaseSettings> authorityDatabaseSettings)
        {
            MongoClient mongoClient = new MongoClient(
            authorityDatabaseSettings.Value.ConnectionString);

            IMongoDatabase mongoDatabase = mongoClient.GetDatabase(
                authorityDatabaseSettings.Value.DatabaseName);

            _authorityCollection = mongoDatabase.GetCollection<AuthorityCategories>(
                authorityDatabaseSettings.Value.AuthorityCollectionName);
        }

        public List<AuthorityCategories> Get()
        {
            return _authorityCollection.Find(p => true).ToList().OrderBy(p => p.id).ToList();
        }

        public AuthorityCategories Get(long id)
        {
            return _authorityCollection.Find(p => p.id == id).FirstOrDefault();
        }

        public void Post(AuthorityCategories authority)
        {
            _authorityCollection.InsertOne(authority);
        }

        public void Update(AuthorityCategories authority)
        {
            _authorityCollection.ReplaceOne(p => p.id == authority.id, authority);
        }

        public void Delete(long id)
        {
            _authorityCollection.DeleteOne(p => p.id == id);
        }

        public long ObtenerUltimoRegistro()
        {
            List<AuthorityCategories> lAuthority = _authorityCollection.Find(p => true).ToList();

            if (lAuthority.Count == 0)
                return 1;

            return lAuthority.OrderByDescending(p => p.id).First().id + 1;
        }
    }
}

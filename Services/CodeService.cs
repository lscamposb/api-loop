using LoopApi.Interfaz;
using LoopApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LoopApi.Services
{
    public class CodeService : ICode
    {
        private readonly IMongoCollection<Code> _codeCollection;

        public CodeService(IOptions<CodeDatabaseSettings> codeDatabaseSettings)
        {
            MongoClient mongoClient = new MongoClient(
            codeDatabaseSettings.Value.ConnectionString);

            IMongoDatabase mongoDatabase = mongoClient.GetDatabase(
                codeDatabaseSettings.Value.DatabaseName);

            _codeCollection = mongoDatabase.GetCollection<Code>(
                codeDatabaseSettings.Value.CodesCollectionName);
        }

        public List<Code> Get()
        {
            return _codeCollection.Find(p => true).ToList().OrderBy(p => p.id).ToList();
        }

        public Code Get(long id)
        {
            return _codeCollection.Find(p => p.id == id).FirstOrDefault();
        }

        public void Create(Code code)
        {
            _codeCollection.InsertOne(code);
        }

        public void Update(Code code)
        {
            _codeCollection.ReplaceOne(p => p.id == code.id, code);
        }

        public void Delete(long id)
        {
            _codeCollection.DeleteOne(p => p.id == id);
        }

        public long ObtenerUltimoRegistro()
        {
            List<Code> lCode = _codeCollection.Find(p => true).ToList();

            if (lCode.Count == 0)
                return 1;

            return lCode.OrderByDescending(p => p.id).First().id + 1;
        }
    }
}

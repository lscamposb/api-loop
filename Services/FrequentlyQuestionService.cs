using LoopApi.Interfaz;
using LoopApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LoopApi.Services
{
    public class FrequentlyQuestionService : IFrequentlyQuestion
    {
        private readonly IMongoCollection<FrequentlyQuestion> _frequentlyQuestionServiceCollection;

        public FrequentlyQuestionService(IOptions<FrequentlyQuestionDatabaseSettings> frequentlyQuestionDatabaseSettings)
        {
            MongoClient mongoClient = new MongoClient(
            frequentlyQuestionDatabaseSettings.Value.ConnectionString);

            IMongoDatabase mongoDatabase = mongoClient.GetDatabase(
                frequentlyQuestionDatabaseSettings.Value.DatabaseName);

            _frequentlyQuestionServiceCollection = mongoDatabase.GetCollection<FrequentlyQuestion>(
                frequentlyQuestionDatabaseSettings.Value.FrequentlyQuestionCategoriesCollectionName);
        }

        public List<FrequentlyQuestion> Get()
        {
            return _frequentlyQuestionServiceCollection.Find(p=> true).ToList();
        }
    }
}

using LoopApi.Interfaz;
using LoopApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LoopApi.Services
{
    public class NotificationService : INotification
    {
        private readonly IMongoCollection<Notification> _notificationCollection;

        public NotificationService(IOptions<NotificationDatabaseSettings> notificationCollection)
        {
            MongoClient mongoClient = new MongoClient(
            notificationCollection.Value.ConnectionString);

            IMongoDatabase mongoDatabase = mongoClient.GetDatabase(
                notificationCollection.Value.DatabaseName);

            _notificationCollection = mongoDatabase.GetCollection<Notification>(
                notificationCollection.Value.NotificationCollectionName);
        }

        public List<Notification> Get(int isRead)
        {
            try
            {
                return _notificationCollection.Find(p => p.read == isRead).ToList().OrderBy(p => p.id).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

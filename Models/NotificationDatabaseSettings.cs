using LoopApi.Interfaz;

namespace LoopApi.Models
{
    public class NotificationDatabaseSettings : INotificationDBSettings
    {
        public string NotificationCollectionName { get; set; } = String.Empty;
        public string ConnectionString { get; set; } = String.Empty;
        public string DatabaseName { get; set; } = String.Empty;
    }
}

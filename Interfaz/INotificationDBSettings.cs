namespace LoopApi.Interfaz
{
    public interface INotificationDBSettings
    {
        string NotificationCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}

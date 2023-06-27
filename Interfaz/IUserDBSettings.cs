namespace LoopApi.Interfaz
{
    public interface IUserDBSettings
    {
        string UsersCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}

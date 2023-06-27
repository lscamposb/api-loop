namespace LoopApi.Interfaz
{
    public interface IStoreDBSettings
    {
        string StoreCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}

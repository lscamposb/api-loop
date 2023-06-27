namespace LoopApi.Interfaz
{
    public interface StoreCategoriesDBSettings
    {
        string StoreCategoriesCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}

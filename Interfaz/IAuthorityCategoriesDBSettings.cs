namespace LoopApi.Interfaz
{
    public interface IAuthorityCategoriesDBSettings
    {
        string AuthorityCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}

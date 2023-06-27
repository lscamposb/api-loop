namespace LoopApi.Interfaz
{
    public interface ICodeDBSettings
    {
        string CodeCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}

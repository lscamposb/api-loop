using LoopApi.Interfaz;

namespace LoopApi.Models
{
    public class StoreDatabaseSettings : IStoreDBSettings
    {
        public string StoreCollectionName { get; set; } = String.Empty;
        public string ConnectionString { get; set; } = String.Empty;
        public string DatabaseName { get; set; } = String.Empty;
    }
}

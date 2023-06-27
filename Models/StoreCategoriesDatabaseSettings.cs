using LoopApi.Interfaz;

namespace LoopApi.Models
{
    public class StoreCategoriesDatabaseSettings : StoreCategoriesDBSettings
    {
        public string StoreCategoriesCollectionName { get; set; } = String.Empty;
        public string ConnectionString { get; set; } = String.Empty;
        public string DatabaseName { get; set; } = String.Empty;
    }
}

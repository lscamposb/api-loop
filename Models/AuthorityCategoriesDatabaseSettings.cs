using LoopApi.Interfaz;

namespace LoopApi.Models
{
    public class AuthorityCategoriesDatabaseSettings : IAuthorityCategoriesDBSettings
    {
        public string AuthorityCollectionName { get; set; } = String.Empty;
        public string ConnectionString { get; set; } = String.Empty;
        public string DatabaseName { get; set; } = String.Empty;
    }
}
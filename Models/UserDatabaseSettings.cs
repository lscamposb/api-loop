using LoopApi.Interfaz;

namespace LoopApi.Models
{
    public class UserDatabaseSettings : IUserDBSettings
    {
        public string UsersCollectionName { get; set; } = String.Empty;
        public string ConnectionString { get; set; } = String.Empty;
        public string DatabaseName { get; set; } = String.Empty;
    }
}

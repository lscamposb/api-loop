using LoopApi.Interfaz;

namespace LoopApi.Models
{
    public class CouponDatabaseSettings : ICouponDBSettings
    {
        public string CouponCollectionName { get; set; } = String.Empty;
        public string ConnectionString { get; set; } = String.Empty;
        public string DatabaseName { get; set; } = String.Empty;
    }
}

namespace LoopApi.Interfaz
{
    public interface ICouponDBSettings
    {
        string CouponCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}

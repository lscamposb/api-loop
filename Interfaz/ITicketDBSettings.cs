namespace LoopApi.Interfaz
{
    public interface ITicketDBSettings
    {
        string TicketsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}

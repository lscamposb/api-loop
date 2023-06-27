using LoopApi.Interfaz;

namespace LoopApi.Models
{
    public class TicketDatabaseSettings : ITicketDBSettings
    {
        public string TicketsCollectionName { get; set; } = String.Empty;
        public string ConnectionString { get; set; } = String.Empty;
        public string DatabaseName { get; set; } = String.Empty;
    }
}

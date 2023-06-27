using LoopApi.DTO;
using LoopApi.Interfaz;
using LoopApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace LoopApi.Services
{
    public class TicketService : ITicket
    {
        private readonly IMongoCollection<Ticket> _ticketCollection;

        public TicketService(IOptions<TicketDatabaseSettings> ticketStoreDatabaseSettings)
        {
            MongoClient mongoClient = new MongoClient(
            ticketStoreDatabaseSettings.Value.ConnectionString);

            IMongoDatabase mongoDatabase = mongoClient.GetDatabase(
                ticketStoreDatabaseSettings.Value.DatabaseName);

            _ticketCollection = mongoDatabase.GetCollection<Ticket>(
                ticketStoreDatabaseSettings.Value.TicketsCollectionName);
        }

        public Ticket Create(Ticket ticket)
        {
            _ticketCollection.InsertOne(ticket);
            return ticket;
        }

        public List<Ticket> Get()
        {
            return _ticketCollection.Find(p => true).ToList().OrderBy(p => p.id).ToList();
        }

        public Ticket Get(long id)
        {
            return _ticketCollection.Find(p => p.id == id).FirstOrDefault();
        }

        public void Update(Ticket ticket)
        {
            _ticketCollection.ReplaceOne(p => p.id == ticket.id, ticket);
        }

        public void Delete(long id)
        {
            _ticketCollection.DeleteOne(p => p.id == id);
        }

        public long ObtenerUltimoRegistro()
        {
            List<Ticket> lTicket = _ticketCollection.Find(p => true).ToList();

            if (lTicket.Count == 0)
                return 1;

            return lTicket.OrderByDescending(p => p.id).First().id + 1;
        }
    }
}

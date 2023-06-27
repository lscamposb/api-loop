using LoopApi.DTO;
using LoopApi.Models;
using MongoDB.Bson;

namespace LoopApi.Interfaz
{
    public interface ITicket
    {
        public List<Ticket> Get();
        public Ticket Get(long id);
        public Ticket Create(Ticket ticket);
        public void Update(Ticket ticket);
        public void Delete(long id);
    }
}

using LoopApi.Models;

namespace LoopApi.Interfaz
{
    public interface INotification
    {
        public List<Notification> Get(int isRead);        
    }
}

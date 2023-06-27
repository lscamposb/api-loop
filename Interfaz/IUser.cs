using LoopApi.Models;

namespace LoopApi.Interfaz
{
    public interface IUser
    {        
        public List<User> Get();
        public User Get(long id);
        public void Post(User user);
        public void Update(User user);
        public void Delete(long id);
    }
}

using LoopApi.Models;

namespace LoopApi.Interfaz
{
    public interface IStore
    {
        public List<Store> Get();
        public Store Get(long id);
        public void Post(Store store);
        public void Put(Store store);
        public void Delete(long id);
    }
}

using LoopApi.Models;

namespace LoopApi.Interfaz
{
    public interface IStoreCategorie
    {
        public List<StoreCategories> Get();
        public StoreCategories Get(long id);
        public void Post(StoreCategories storeCategories);
        public void Update(StoreCategories storeCategories);
        public void Delete(long id);
    }
}

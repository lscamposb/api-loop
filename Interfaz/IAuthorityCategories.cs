using LoopApi.Models;

namespace LoopApi.Interfaz
{
    public interface IAuthorityCategories
    {
        public List<AuthorityCategories> Get();
        public AuthorityCategories Get(long id);
        public void Post(AuthorityCategories authority);
        public void Update(AuthorityCategories authority);
        public void Delete(long id);
    }
}

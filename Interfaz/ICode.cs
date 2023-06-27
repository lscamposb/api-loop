using LoopApi.Models;

namespace LoopApi.Interfaz
{
    public interface ICode
    {
        public List<Code> Get();
        public Code Get(long id);
        public void Create(Code code);
        public void Update(Code code);
        public void Delete(long id);
    }
}

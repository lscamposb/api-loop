using LoopApi.Models;

namespace LoopApi.Interfaz
{
    public interface ICoupon
    {
        public List<Coupon> Get();
        public Coupon Get(long id);
        public void Create(Coupon code);
        public void Update(Coupon code);
        public void Delete(long id);
    }
}

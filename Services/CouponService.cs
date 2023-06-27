using LoopApi.Interfaz;
using LoopApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LoopApi.Services
{
    public class CouponService : ICoupon
    {
        private readonly IMongoCollection<Coupon> _couponCollection;

        public CouponService(IOptions<CouponDatabaseSettings> couponDatabaseSettings)
        {
            MongoClient mongoClient = new MongoClient(
            couponDatabaseSettings.Value.ConnectionString);

            IMongoDatabase mongoDatabase = mongoClient.GetDatabase(
                couponDatabaseSettings.Value.DatabaseName);

            _couponCollection = mongoDatabase.GetCollection<Coupon>(
                couponDatabaseSettings.Value.CouponCollectionName);
        }

        public List<Coupon> Get()
        {
            return _couponCollection.Find(p => true).ToList().OrderBy(p => p.id).ToList();
        }

        public Coupon Get(long id)
        {
            return _couponCollection.Find(p => p.id == id).FirstOrDefault();
        }

        public List<Coupon> GetStore(long idStore)
        {
            return _couponCollection.Find(p => p.store == idStore).ToList();
        }

        public void Create(Coupon coupon)
        {
            _couponCollection.InsertOne(coupon);
        }

        public void Update(Coupon coupon)
        {
            _couponCollection.ReplaceOne(p => p.id == coupon.id, coupon);
        }

        public void Delete(long id)
        {
            _couponCollection.DeleteOne(p => p.id == id);
        }

        public long ObtenerUltimoRegistro()
        {
            List<Coupon> lCoupon = _couponCollection.Find(p => true).ToList();

            if (lCoupon.Count == 0)
                return 1;

            return lCoupon.OrderByDescending(p => p.id).First().id + 1;
        }        
    }
}

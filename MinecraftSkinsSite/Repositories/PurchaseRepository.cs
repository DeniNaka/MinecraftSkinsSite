using MinecraftSkinsSite.Data;
using MinecraftSkinsSite.Models;

namespace MinecraftSkinsSite.Repositories
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly IInMemoryDatabase db;

        public PurchaseRepository(IInMemoryDatabase db)
        {
            this.db = db;
        }

        public List<Purchase> GetAll()
        {
            return db.Purchases;
        }

        public void Add(Purchase purchase)
        {
            db.Purchases.Add(purchase);
        }
    }
}

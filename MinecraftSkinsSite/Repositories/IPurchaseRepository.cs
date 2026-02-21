using MinecraftSkinsSite.Models;

namespace MinecraftSkinsSite.Repositories
{
    public interface IPurchaseRepository
    {
        List<Purchase> GetAll();
        void Add(Purchase purchase);
    }
}

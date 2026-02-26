using MinecraftSkinsSite.Models;

namespace MinecraftSkinsSite.Data
{
    public interface IInMemoryDatabase
    {
        List<Skin> Skins { get; set; }
        List<Purchase> Purchases { get; set; }
    }
}

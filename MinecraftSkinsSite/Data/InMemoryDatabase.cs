using MinecraftSkinsSite.Models;

namespace MinecraftSkinsSite.Data
{
    public class InMemoryDatabase
    {
        public List<Skin> Skins { get; set; } = new()
        {
        new Skin { Id = 1, Name = "Dragon", BasePriceUsd = 10, IsAvailable = true },
        new Skin { Id = 2, Name = "Knight", BasePriceUsd = 8, IsAvailable = true },
        new Skin { Id = 3, Name = "Zombie", BasePriceUsd = 5, IsAvailable = false }
        };

        public List<Purchase> Purchases { get; set; } = new();
    }
}

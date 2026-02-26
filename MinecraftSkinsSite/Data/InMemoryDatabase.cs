using MinecraftSkinsSite.Models;

namespace MinecraftSkinsSite.Data
{
    public class InMemoryDatabase : IInMemoryDatabase
    {
        public List<Skin> Skins { get; set; } = new()
        {
        new Skin { Id = 1, Name = "Dragon", BasePriceUsd = 10, IsAvailable = true },
        new Skin { Id = 2, Name = "Knight", BasePriceUsd = 8, IsAvailable = true },
        new Skin { Id = 3, Name = "VIP", BasePriceUsd = 12, IsAvailable = false },
        new Skin { Id = 4, Name = "Barbarian", BasePriceUsd = 7, IsAvailable = true },
        new Skin { Id = 5, Name = "Zombie", BasePriceUsd = 5, IsAvailable = false }
        };

        public List<Purchase> Purchases { get; set; } = new();
    }
}

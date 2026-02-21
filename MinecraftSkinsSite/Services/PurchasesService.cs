using Microsoft.AspNetCore.Mvc;
using MinecraftSkinsSite.Interfaces;
using MinecraftSkinsSite.Models;
using MinecraftSkinsSite.Repositories;

namespace MinecraftSkinsSite.Services
{
    public class PurchasesService : IPurchasesService
    {
        private readonly ISkinRepository skinRepository;
        private readonly IPurchaseRepository purchaseRepository;
        private readonly IPriceService priceService;

        public PurchasesService(ISkinRepository skinRepository, IPurchaseRepository purchaseRepository, IPriceService priceService)
        {
            this.skinRepository = skinRepository;
            this.purchaseRepository = purchaseRepository;
            this.priceService = priceService;
        }

        [HttpPost("{skinId}")]
        public async Task<(bool Success, string? Error, Purchase? Purchase)> BuyAsync(int skinId, CancellationToken ct)
        {
            var skin = skinRepository.GetById(skinId);

            if (skin == null)
                return (false, "Skin not found!", null);

            if (!skin.IsAvailable)
                return (false, "Skin is not available!", null);

            var finalPrice = await priceService.CalculateFinalPriceAsync(skin.BasePriceUsd, ct);

            var purchase = new Purchase
            {
                Id = purchaseRepository.GetAll().Count + 1,
                SkinId = skin.Id,
                SkinName = skin.Name,
                FinalPriceUsd = finalPrice,
                CreatedAt = DateTime.UtcNow
            };

            purchaseRepository.Add(purchase);

            skin.IsAvailable = false;

            return (true, null, purchase);
        }

        [HttpGet]
        public IEnumerable<Purchase> GetAllAsync()
        {
            return purchaseRepository.GetAll();
        }
    }
}

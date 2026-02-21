using Microsoft.AspNetCore.Mvc;
using MinecraftSkinsSite.Interfaces;
using MinecraftSkinsSite.Models;
using MinecraftSkinsSite.Repositories;

namespace MinecraftSkinsSite.Services
{
    public class SkinsService : ISkinsService
    {
        private readonly ISkinRepository skinRepository;
        private readonly IPriceService priceService;

        public SkinsService(ISkinRepository skinRepository, IPriceService priceService)
        {
            this.skinRepository = skinRepository;
            this.priceService = priceService;
        }

        [HttpGet]
        public async Task<IEnumerable<object>> GetAllAsync(CancellationToken ct)
        {
            var skins = skinRepository.GetAll();

            var result = new List<object>();

            foreach (var skin in skins)
            {
                var finalPrice = await priceService.CalculateFinalPriceAsync(skin.BasePriceUsd, ct);

                result.Add(new
                {
                    skin.Id,
                    skin.Name,
                    skin.BasePriceUsd,
                    skin.IsAvailable,
                    FinalPriceUsd = finalPrice
                });
            }
            return result;
        }

        [HttpGet("{id}")]
        public async Task<object?> GetByIdAsync(int id, CancellationToken ct)
        {
            var skin = skinRepository.GetById(id);

            if (skin == null)
                return null;

            var finalPrice = await priceService.CalculateFinalPriceAsync(skin.BasePriceUsd, ct);

            return new
            {
                skin.Id,
                skin.Name,
                skin.BasePriceUsd,
                skin.IsAvailable,
                FinalPriceUsd = finalPrice
            };
        }
    }
}

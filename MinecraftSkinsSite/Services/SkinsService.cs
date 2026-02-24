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

        public async Task<IEnumerable<Skin>> GetAllAsync(CancellationToken ct)
        {
            var skins = skinRepository.GetAll();

            var result = new List<Skin>();

            foreach (var skin in skins)
            {
                var finalPrice = await priceService.CalculateFinalPriceAsync(skin.BasePriceUsd, ct);

                result.Add(new Skin()
                {
                    Id = skin.Id,
                    Name = skin.Name,
                    BasePriceUsd = skin.BasePriceUsd,
                    IsAvailable = skin.IsAvailable,
                    FinalPriceUsd = finalPrice,
                });
            }
            return result;
        }

        public async Task<Skin?> GetByIdAsync(int id, CancellationToken ct)
        {
            var skin = skinRepository.GetById(id);

            if (skin == null)
                return null;

            var finalPrice = await priceService.CalculateFinalPriceAsync(skin.BasePriceUsd, ct);

            return new Skin()
            {
                Id = skin.Id,
                Name = skin.Name,
                BasePriceUsd = skin.BasePriceUsd,
                IsAvailable = skin.IsAvailable,
                FinalPriceUsd = finalPrice,
            };
        }
    }
}

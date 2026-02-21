using MinecraftSkinsSite.Interfaces;

namespace MinecraftSkinsSite.Services
{
    public class PriceService : IPriceService
    {
        private readonly IBtcRateService btcRateService;

        public PriceService(IBtcRateService btcRateService)
        {
            this.btcRateService = btcRateService;
        }

        public async Task<decimal> CalculateFinalPriceAsync(decimal basePrice, CancellationToken ct)
        {
            var rate = await btcRateService.GetBtcUsdRateAsync(ct);

            var extra = rate / 10000;

            return Math.Round(basePrice + extra, 2);
        }
    }
}

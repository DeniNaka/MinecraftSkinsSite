using Microsoft.Extensions.Caching.Memory;
using MinecraftSkinsSite.Interfaces;
using System.Text.Json;

namespace MinecraftSkinsSite.Services
{
    public class BtcRateService : IBtcRateService
    {
        private readonly HttpClient httpClient;

        private IMemoryCache memoryCache;
        private const string Key = "btc-rate";

        public BtcRateService(HttpClient httpClient, IMemoryCache memoryCache)
        {
            this.httpClient = httpClient;
            this.memoryCache = memoryCache;
            this.httpClient.DefaultRequestHeaders.Add("User-Agent", "MinecraftSkinsSiteApp");
        }

        public async Task<decimal> GetBtcUsdRateAsync(CancellationToken ct)
        {
            if (memoryCache.TryGetValue(Key, out decimal cached))
                return cached;

            try
            {
                Console.WriteLine("Calling external BTC API");

                var url = "https://api.coingecko.com/api/v3/simple/price?ids=bitcoin&vs_currencies=usd";

                var response = await httpClient.GetAsync(url, ct);

                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync(ct);

                using var doc = JsonDocument.Parse(json);

                var rate = doc.RootElement
                    .GetProperty("bitcoin")
                    .GetProperty("usd")
                    .GetDecimal();

                memoryCache.Set(Key, rate, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));

                return rate;
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Request was cancelled");
                throw;
            }
            catch (Exception ex)
            {
                if (memoryCache.TryGetValue(Key, out decimal oldRate))
                    return oldRate;

                throw new Exception("Cannot get BTC/USD rate", ex);
            }
        }
    }
}

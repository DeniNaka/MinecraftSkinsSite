using System.Text.Json;

namespace MinecraftSkinsSite.Services
{
    public class BtcRateService
    {
        private readonly HttpClient httpClient;

        private decimal cachedRate;
        private DateTime lastUpdated;

        public BtcRateService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.httpClient.DefaultRequestHeaders.Add("User-Agent", "MinecraftSkinsSiteApp");
        }

        public async Task<decimal> GetBtcUsdRateAsync(CancellationToken ct)
        {
            if ((DateTime.UtcNow - lastUpdated).TotalSeconds < 300 && cachedRate > 0)
            {
                return cachedRate;
            }

            try
            {
                var url = "https://api.coingecko.com/api/v3/simple/price?ids=bitcoin&vs_currencies=usd";

                var response = await httpClient.GetAsync(url, ct);

                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync(ct);

                using var doc = JsonDocument.Parse(json);

                var rate = doc.RootElement
                    .GetProperty("bitcoin")
                    .GetProperty("usd")
                    .GetDecimal();

                cachedRate = rate;
                lastUpdated = DateTime.UtcNow;

                return rate;
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Request was cancelled");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("BTC API ERROR: " + ex.Message);
                return cachedRate;
            }
        }
    }
}

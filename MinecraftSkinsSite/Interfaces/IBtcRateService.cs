namespace MinecraftSkinsSite.Interfaces
{
    public interface IBtcRateService
    {
        Task<decimal> GetBtcUsdRateAsync(CancellationToken ct);
    }
}

namespace MinecraftSkinsSite.Interfaces
{
    public interface IPriceService
    {
        Task<decimal> CalculateFinalPriceAsync(decimal basePrice, CancellationToken ct);
    }
}

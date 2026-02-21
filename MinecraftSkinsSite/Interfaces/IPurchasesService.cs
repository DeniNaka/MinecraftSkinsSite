using Microsoft.AspNetCore.Mvc;
using MinecraftSkinsSite.Models;

namespace MinecraftSkinsSite.Interfaces
{
    public interface IPurchasesService
    {
        Task<(bool Success, string? Error, Purchase? Purchase)> BuyAsync(int skinId, CancellationToken ct);
        IEnumerable<Purchase> GetAllAsync();
    }
}

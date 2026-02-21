using Microsoft.AspNetCore.Mvc;
using MinecraftSkinsSite.Models;

namespace MinecraftSkinsSite.Interfaces
{
    public interface ISkinsService
    {
        Task<IEnumerable<object>> GetAllAsync(CancellationToken ct);
        Task<object?> GetByIdAsync(int id, CancellationToken ct);
    }
}

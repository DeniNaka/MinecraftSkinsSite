using Microsoft.AspNetCore.Mvc;
using MinecraftSkinsSite.Models;

namespace MinecraftSkinsSite.Interfaces
{
    public interface ISkinsService
    {
        Task<IEnumerable<Skin>> GetAllAsync(CancellationToken ct);
        Task<Skin?> GetByIdAsync(int id, CancellationToken ct);
    }
}

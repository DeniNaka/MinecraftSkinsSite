using Microsoft.AspNetCore.Mvc;
using MinecraftSkinsSite.Data;
using MinecraftSkinsSite.Interfaces;
using MinecraftSkinsSite.Repositories;
using MinecraftSkinsSite.Services;

namespace MinecraftSkinsSite.Controllers;

[ApiController]
[Route("api/skins")]
public class SkinsController : ControllerBase
{
    private readonly ISkinsService skinsService;

    public SkinsController(ISkinsService skinsService)
    {
        this.skinsService = skinsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var result = await skinsService.GetAllAsync(ct);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var result = await skinsService.GetByIdAsync(id, ct);

        if (result == null)
            return NotFound();

        return Ok(result);
    }
}

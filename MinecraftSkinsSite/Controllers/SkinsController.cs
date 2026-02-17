using Microsoft.AspNetCore.Mvc;
using MinecraftSkinsSite.Data;
using MinecraftSkinsSite.Services;

namespace MinecraftSkinsSite.Controllers;

[ApiController]
[Route("api/skins")]
public class SkinsController : ControllerBase
{
    private readonly InMemoryDatabase db;
    private readonly PriceService priceService;

    public SkinsController(InMemoryDatabase db, PriceService priceService)
    {
        this.db = db;
        this.priceService = priceService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var result = new List<object>();

        foreach (var skin in db.Skins)
        {
            var finalPrice = await priceService.CalculateFinalPriceAsync(skin.BasePriceUsd, ct);

            result.Add(new
            {
                skin.Id,
                skin.Name,
                skin.BasePriceUsd,
                skin.IsAvailable,
                FinalPriceUsd = finalPrice
            });
        }
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var skin = db.Skins.FirstOrDefault(x => x.Id == id);

        if (skin == null)
            return NotFound();

        var finalPrice = await priceService.CalculateFinalPriceAsync(skin.BasePriceUsd, ct);

        return Ok(new
        {
            skin.Id,
            skin.Name,
            skin.BasePriceUsd,
            skin.IsAvailable,
            FinalPriceUsd = finalPrice
        });
    }
}

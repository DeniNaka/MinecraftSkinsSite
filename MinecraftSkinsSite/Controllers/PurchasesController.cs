using Microsoft.AspNetCore.Mvc;
using MinecraftSkinsSite.Data;
using MinecraftSkinsSite.Models;
using MinecraftSkinsSite.Services;

namespace MinecraftSkinsSite.Controllers;

[ApiController]
[Route("api/purchases")]
public class PurchasesController : ControllerBase
{
    private readonly InMemoryDatabase db;
    private readonly PriceService priceService;

    public PurchasesController(InMemoryDatabase db, PriceService priceService)
    {
        this.db = db;
        this.priceService = priceService;
    }

    [HttpPost("{skinId}")]
    public async Task<IActionResult> Buy(int skinId, CancellationToken ct)
    {
        var skin = db.Skins.FirstOrDefault(x => x.Id == skinId);

        if (skin == null)
            return NotFound("Skin not found");

        if (!skin.IsAvailable)
            return BadRequest("Skin is not available");

        var finalPrice = await priceService.CalculateFinalPriceAsync(skin.BasePriceUsd, ct);

        var purchase = new Purchase
        {
            Id = db.Purchases.Count + 1,
            SkinId = skin.Id,
            SkinName = skin.Name,
            FinalPriceUsd = finalPrice,
            CreatedAt = DateTime.UtcNow
        };

        db.Purchases.Add(purchase);

        return Ok(purchase);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(db.Purchases);
    }
}

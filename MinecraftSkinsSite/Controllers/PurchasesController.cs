using Microsoft.AspNetCore.Mvc;
using MinecraftSkinsSite.Data;
using MinecraftSkinsSite.Interfaces;
using MinecraftSkinsSite.Models;
using MinecraftSkinsSite.Repositories;
using MinecraftSkinsSite.Services;

namespace MinecraftSkinsSite.Controllers;

[ApiController]
[Route("api/purchases")]
public class PurchasesController : ControllerBase
{
    private readonly IPurchasesService purchasesService;

    public PurchasesController(IPurchasesService purchasesService)
    {
        this.purchasesService = purchasesService;
    }

    [HttpPost("{skinId}")]
    public async Task<IActionResult> Buy(int skinId, CancellationToken ct)
    {
        var purchase = await purchasesService.BuyAsync(skinId, ct);

        if (!purchase.Success)
            return BadRequest("Skin is not available");

        return Ok(purchase);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(purchasesService.GetAllAsync());
    }
}

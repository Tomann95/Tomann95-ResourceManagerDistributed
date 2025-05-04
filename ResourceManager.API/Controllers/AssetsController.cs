using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using ResourceManager.API.Data;
using ResourceManager.API.Hubs;
using ResourceManager.API.Models;

namespace ResourceManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AssetsController : ControllerBase
{
    private readonly AppDbContext _context;

    public AssetsController(AppDbContext context)
    {
        _context = context;
    }


    private readonly IHubContext<SyncHub> _hub;

    public AssetsController(AppDbContext context, IHubContext<SyncHub> hub)
    {
        _context = context;
        _hub = hub;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var assets = await _context.Assets.ToListAsync();
        return Ok(assets);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var asset = await _context.Assets.FindAsync(id);
        if (asset == null) return NotFound();
        return Ok(asset);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Asset asset)
    {
        _context.Assets.Add(asset);
        await _context.SaveChangesAsync();
        await _hub.Clients.All.SendAsync("AssetUpdated", $"Dodano: {asset.Name}");

        return CreatedAtAction(nameof(Get), new { id = asset.Id }, asset);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Asset updated)
    {
        var asset = await _context.Assets.FindAsync(id);
        if (asset == null) return NotFound();

        asset.Name = updated.Name;
        asset.Type = updated.Type;
        asset.Status = updated.Status;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var asset = await _context.Assets.FindAsync(id);
        if (asset == null) return NotFound();

        _context.Assets.Remove(asset);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}


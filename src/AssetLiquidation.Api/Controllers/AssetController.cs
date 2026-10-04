using AssetLiquidation.Api.DTOs;
using AssetLiquidation.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AssetLiquidation.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AssetController : ControllerBase
{
    private readonly LiquidateDbContext _context;

    public AssetController(LiquidateDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Cadastra um novo Ativo
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAssetRequest request)
    {
        try
        {
            var asset = new Asset(request.AssetId, request.InitialAmount);
            //estudar
            _context.Assets.Add(asset);
            await _context.SaveChangesAsync();

            var response = new AssetResponse(
                asset.Id,
                asset.AssetId,
                asset.InitialAmount,
                asset.CurrentAmount,
                asset.Status.ToString(),
                asset.CreatedAt
            );
            //estudar
            return CreatedAtAction(nameof(GetById), new {id = asset.Id}, response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message});
        }
    }

    /// <summary>
    /// Consulta um Ativo pelo GUID
    /// </summary>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var asset = await _context.Assets.FindAsync(id);

        if (asset == null)
            return NotFound(new { message = "Ativo não encontrado." });

        var response = new AssetResponse(
            asset.Id,
            asset.AssetId,
            asset.InitialAmount,
            asset.CurrentAmount,
            asset.Status.ToString(),
            asset.CreatedAt
        );

        return Ok(response);
    }

    /// <summary>
    /// Lista todos os Ativos cadastrados
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        //Estudar
        var assets = await _context.Assets
            .AsNoTracking()
            .Select(asset => new AssetResponse(
                asset.Id,
                asset.AssetId,
                asset.InitialAmount,
                asset.CurrentAmount,
                asset.Status.ToString(),
                asset.CreatedAt
            ))
            .ToListAsync();
    
        return Ok(assets);
    }
}
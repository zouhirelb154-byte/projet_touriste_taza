using backend_touriste_taza.Data;
using backend_touriste_taza.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend_touriste_taza.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TipsController : ControllerBase
{
    private readonly AppDbContext _context;

    public TipsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<TravelTip>>> GetTips()
    {
        return Ok(await _context.TravelTips.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TravelTip>> GetTipById(int id)
    {
        var tip = await _context.TravelTips.FindAsync(id);

        if (tip == null)
        {
            return NotFound("Conseil introuvable");
        }

        return Ok(tip);
    }

    [HttpPost]
    public async Task<ActionResult<TravelTip>> AddTip(TravelTip newTip)
    {
        _context.TravelTips.Add(newTip);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTipById), new { id = newTip.Id }, newTip);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TravelTip>> UpdateTip(int id, TravelTip updatedTip)
    {
        var tip = await _context.TravelTips.FindAsync(id);

        if (tip == null)
        {
            return NotFound("Conseil introuvable");
        }

        tip.Title = updatedTip.Title;
        tip.Description = updatedTip.Description;
        tip.Icon = updatedTip.Icon;

        await _context.SaveChangesAsync();

        return Ok(tip);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTip(int id)
    {
        var tip = await _context.TravelTips.FindAsync(id);

        if (tip == null)
        {
            return NotFound("Conseil introuvable");
        }

        _context.TravelTips.Remove(tip);
        await _context.SaveChangesAsync();

        return Ok("Conseil supprimé avec succès");
    }
}
using backend_touriste_taza.Data;
using backend_touriste_taza.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend_touriste_taza.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HotelsController : ControllerBase
{
    private readonly AppDbContext _context;

    public HotelsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Hotel>>> GetHotels()
    {
        return Ok(await _context.Hotels.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Hotel>> GetHotelById(int id)
    {
        var hotel = await _context.Hotels.FindAsync(id);

        if (hotel == null)
        {
            return NotFound("Hotel introuvable");
        }

        return Ok(hotel);
    }

    [HttpPost]
    public async Task<ActionResult<Hotel>> AddHotel(Hotel newHotel)
    {
        _context.Hotels.Add(newHotel);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetHotelById), new { id = newHotel.Id }, newHotel);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Hotel>> UpdateHotel(int id, Hotel updatedHotel)
    {
        var hotel = await _context.Hotels.FindAsync(id);

        if (hotel == null)
        {
            return NotFound("Hotel introuvable");
        }

        hotel.Name = updatedHotel.Name;
        hotel.Description = updatedHotel.Description;
        hotel.Image = updatedHotel.Image;
        hotel.Price = updatedHotel.Price;
        hotel.Link = updatedHotel.Link;

        await _context.SaveChangesAsync();

        return Ok(hotel);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteHotel(int id)
    {
        var hotel = await _context.Hotels.FindAsync(id);

        if (hotel == null)
        {
            return NotFound("Hotel introuvable");
        }

        _context.Hotels.Remove(hotel);
        await _context.SaveChangesAsync();

        return Ok("Hotel supprimé avec succès");
    }
}
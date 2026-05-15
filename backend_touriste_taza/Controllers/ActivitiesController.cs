using backend_touriste_taza.Data;
using backend_touriste_taza.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend_touriste_taza.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ActivitiesController : ControllerBase
{
    private readonly AppDbContext _context;

    public ActivitiesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Activity>>> GetActivities()
    {
        return Ok(await _context.Activities.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Activity>> GetActivityById(int id)
    {
        var activity = await _context.Activities.FindAsync(id);

        if (activity == null)
        {
            return NotFound("Activity introuvable");
        }

        return Ok(activity);
    }

    [HttpPost]
    public async Task<ActionResult<Activity>> AddActivity(Activity newActivity)
    {
        _context.Activities.Add(newActivity);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetActivityById), new { id = newActivity.Id }, newActivity);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Activity>> UpdateActivity(int id, Activity updatedActivity)
    {
        var activity = await _context.Activities.FindAsync(id);

        if (activity == null)
        {
            return NotFound("Activity introuvable");
        }

        activity.Title = updatedActivity.Title;
        activity.Category = updatedActivity.Category;
        activity.Description = updatedActivity.Description;
        activity.Image = updatedActivity.Image;
        activity.Link = updatedActivity.Link;

        await _context.SaveChangesAsync();

        return Ok(activity);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteActivity(int id)
    {
        var activity = await _context.Activities.FindAsync(id);

        if (activity == null)
        {
            return NotFound("Activity introuvable");
        }

        _context.Activities.Remove(activity);
        await _context.SaveChangesAsync();

        return Ok("Activity supprimée avec succès");
    }
}
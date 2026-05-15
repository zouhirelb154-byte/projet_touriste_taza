using backend_touriste_taza.Models;
using Microsoft.EntityFrameworkCore;

namespace backend_touriste_taza.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Place> Places { get; set; }
    public DbSet<Activity> Activities { get; set; }
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<TravelTip> TravelTips { get; set; }
}
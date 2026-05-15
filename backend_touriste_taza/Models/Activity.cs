namespace backend_touriste_taza.Models;

public class Activity
{
    public int Id { get; set; }

    public string Title { get; set; } = "";
    public string Category { get; set; } = "";
    public string Description { get; set; } = "";
    public string Image { get; set; } = "";
    public string Link { get; set; } = "";
}
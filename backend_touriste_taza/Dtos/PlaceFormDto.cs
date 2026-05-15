

using Microsoft.AspNetCore.Http;

namespace backend_touriste_taza.Dtos
{
    public class PlaceFormDto
    {
        public string Title { get; set; } = "";
        public string Category { get; set; } = "";
        public string Description { get; set; } = "";
        public string Link { get; set; } = "";
        public IFormFile? ImageFile { get; set; }
        public string? CurrentImage { get; set; }
    }
}
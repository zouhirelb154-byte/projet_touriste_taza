using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend_touriste_taza.Data;
using backend_touriste_taza.Models;

namespace backend_touriste_taza.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlacesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PlacesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/places
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Place>>> GetPlaces()
        {
            return await _context.Places.ToListAsync();
        }

        // GET: api/places/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Place>> GetPlace(int id)
        {
            var place = await _context.Places.FindAsync(id);

            if (place == null)
            {
                return NotFound();
            }

            return place;
        }

        // POST: api/places
        // خاص بـ FormData + imageFile من admin.html
        [HttpPost]
        public async Task<IActionResult> CreatePlace([FromForm] PlaceFormDto dto)
        {
            string imagePath = "";

            if (dto.ImageFile != null && dto.ImageFile.Length > 0)
            {
                imagePath = await SaveImage(dto.ImageFile);
            }

            var place = new Place
            {
                Title = dto.Title,
                Category = dto.Category,
                Description = dto.Description,
                Image = imagePath,
                Link = dto.Link
            };

            _context.Places.Add(place);
            await _context.SaveChangesAsync();

            return Ok(place);
        }

        // PUT: api/places/1
        // فـ modification إلا ما اخترتيش صورة جديدة، كتخلي الصورة القديمة
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlace(int id, [FromForm] PlaceFormDto dto)
        {
            var place = await _context.Places.FindAsync(id);

            if (place == null)
            {
                return NotFound();
            }

            string imagePath = !string.IsNullOrWhiteSpace(dto.CurrentImage)
                ? dto.CurrentImage
                : place.Image;

            if (dto.ImageFile != null && dto.ImageFile.Length > 0)
            {
                imagePath = await SaveImage(dto.ImageFile);
            }

            place.Title = dto.Title;
            place.Category = dto.Category;
            place.Description = dto.Description;
            place.Image = imagePath;
            place.Link = dto.Link;

            await _context.SaveChangesAsync();

            return Ok(place);
        }

        // DELETE: api/places/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlace(int id)
        {
            var place = await _context.Places.FindAsync(id);

            if (place == null)
            {
                return NotFound();
            }

            _context.Places.Remove(place);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Place deleted successfully" });
        }

        // Function باش نخزنو image فـ wwwroot/uploads
        private async Task<string> SaveImage(IFormFile imageFile)
        {
            var uploadsFolder = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "uploads"
            );

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var extension = Path.GetExtension(imageFile.FileName);
            var fileName = Guid.NewGuid().ToString() + extension;
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return $"http://localhost:5017/uploads/{fileName}";
        }
    }

    // DTO كيستقبل البيانات جاية من FormData
    public class PlaceFormDto
    {
        public string Title { get; set; } = "";
        public string Category { get; set; } = "";
        public string Description { get; set; } = "";
        public string Link { get; set; } = "";

        // خاص الاسم يكون نفس اللي فـ JS:
        // formData.append("imageFile", imageFile);
        public IFormFile? ImageFile { get; set; }

        // باش فـ Modifier تبقى الصورة القديمة إلا ما اخترتيش صورة جديدة
        public string? CurrentImage { get; set; }
    }
}
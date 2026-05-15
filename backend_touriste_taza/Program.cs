using backend_touriste_taza.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

/* Database SQLite */
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite("Data Source=touriste_taza.db");
});

/* CORS باش frontend/admin.html يقدر يتاصل بال backend */
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins("http://127.0.0.1:5500", "http://localhost:5500")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("AllowFrontend");

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
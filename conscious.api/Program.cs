using conscious.api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Ajouter le service SQLite
builder.Services.AddDbContext<BrandsDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Ajouter les services nécessaires
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()   // Autoriser tous les domaines
              .AllowAnyMethod()   // Autoriser toutes les méthodes (GET, POST, etc.)
              .AllowAnyHeader();  // Autoriser tous les en-têtes
    });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

// Utiliser CORS
app.UseCors("AllowAll");


app.MapControllers();


app.Run();
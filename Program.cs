using Calavier_backend.Data;
using Calavier_backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Password hasher
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

// -------------------- DATABASE --------------------
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Angular se aane wale camelCase ya PascalCase names bind ho jayenge
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });
// -------------------- SERVICES --------------------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Cavalier Logistics API",
        Version = "v1",
        Description = "Cavalier Logistics API Backend"
    });
});

// -------------------- CORS --------------------
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:4200") // Angular dev server
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// -------------------- MIDDLEWARE --------------------
app.UseHttpsRedirection();
app.UseStaticFiles();

// ✅ Enable CORS
app.UseCors();

app.UseAuthorization();

// -------------------- SWAGGER --------------------
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cavalier Logistics API V1");
    c.RoutePrefix = string.Empty;
});

// -------------------- CONTROLLERS --------------------
app.MapControllers();

app.Run();

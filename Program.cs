using Calavier_backend.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// -------------------- DATABASE --------------------
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// -------------------- SERVICES --------------------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Calavier API",
        Version = "v1",
        Description = "Calavier API Backend"
    });
});

var app = builder.Build();

// -------------------- MIDDLEWARE --------------------
app.UseHttpsRedirection();
app.UseAuthorization();

// -------------------- SWAGGER --------------------
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Calavier API V1");
    c.RoutePrefix = string.Empty;
});

// -------------------- MINIMAL API TEST --------------------
//app.MapGet("/api", () =>
//{
//    return Results.Ok(new
//    {
//        status = true,
//        message = "Calavier API is working fine"
//    });
//});

// DB Connection Test
//app.MapGet("/db-check", async (ApplicationDbContext db) =>
//{
//    try
//    {
//        var canConnect = await db.Database.CanConnectAsync();

//        return Results.Ok(new
//        {
//            databaseConnected = canConnect
//        });
//    }
//    catch (Exception ex)
//    {
//        return Results.Problem(ex.Message);
//    }
//});

// -------------------- CONTROLLERS --------------------
app.MapControllers();

app.Run();

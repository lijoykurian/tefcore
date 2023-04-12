using dhs.models;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
//add refererralDbContext for postgresql
builder.Services.AddDbContext<ReferralContext>(options => options.UseNpgsql("Host=localhost;Database=postgres;Username=postgres;Password=postgres"));
//add mvc   
builder.Services.AddControllers();
var app = builder.Build();
//add mapcontrollers
app.MapControllers();
// initizale database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ReferralContext>();
        DbInitializer.Initialize(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred creating the DB.");
    }
}
app.MapGet("/", () => "Hello World!");

app.Run();
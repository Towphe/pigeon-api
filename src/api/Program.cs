using Microsoft.EntityFrameworkCore;
using repo;

var builder = WebApplication.CreateBuilder(args);

// configure services here
builder.Services.AddDbContext<PigeonContext>(opts => {
    opts.UseNpgsql(builder.Configuration["DB:Key"]);
});
builder.Services.AddControllers();

var app = builder.Build();

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthentication();

app.MapDefaultControllerRoute();

app.Run();

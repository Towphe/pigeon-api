using Microsoft.EntityFrameworkCore;
using repo;

var builder = WebApplication.CreateBuilder(args);

// configure services here
builder.Services.AddDbContext<PigeonContext>(opts => {
    opts.UseNpgsql(builder.Configuration["DB:DEV"]);
});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();

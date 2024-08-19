var builder = WebApplication.CreateBuilder(args);

// configure services here

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();

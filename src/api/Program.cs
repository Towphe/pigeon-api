using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using repo;
using services.auth;
using services.contacts;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// configure services here
builder.Services.AddDbContext<PigeonContext>(opts => {
    opts.UseNpgsql(builder.Configuration["DB:Key"]);
});
builder.Services.AddControllers();

builder.Services.AddAuthorization();

// Supabase authentication
builder.Services.AddAuthentication(opts => {
    opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    opts.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(opts => {
    opts.IncludeErrorDetails = true;
    opts.SaveToken = true;
    opts.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes("91IN8gJ6SKG4fNMhTrwlczG7jGXdH8rGa6beCThxGnyl21Q+UbkFyTcxaAnn0AOOyuM/qiX3axPnFalrxgVqpg==")
        ),
        ValidateIssuer = false,
        ValidateAudience = true,
        ValidAudience = "authenticated",
        ValidIssuer = $"https://{builder.Configuration["SupabaseAuth:ProjectId"]}.supabase.co/auth/v1"
    };
    opts.Events = new JwtBearerEvents()
    {
    };
});

builder.Services.AddScoped<IContactsHandler, ContactsHandler>();
builder.Services.AddScoped<IAuthService, AuthService>();

var app = builder.Build();

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthentication();

app.MapDefaultControllerRoute();

app.Run();

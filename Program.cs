using BookHub.Context;
using BookHub.Extensions;
using BookHub.Profiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add db context EF core
builder.Services.AddDbContext<ApplicationDbContext>(
    o => o.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// add automapper
builder.Services.AddAutoMapper(typeof(BookHubProfile));

// add authentication
builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization();
builder.Services.AddAuthorizationBuilder()
    .AddPolicy("RequierdAdminFromBrazil",policy =>
    {
        policy.RequireRole("admin");
        policy.RequireClaim("country", "Brazil");
    });

// Swagger config
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("TokenAuthBookHub",
        new()
        {
            Name = "Authorization",
            Description = "Token based Authentication and Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            In = ParameterLocation.Header
        }
    );
    options.AddSecurityRequirement(new()
        {
            {
                new ()
                {
                    Reference = new OpenApiReference {
                        Type = ReferenceType.SecurityScheme,
                        Id = "TokenAuthBookHub" 
                    }
                }, 
                new List<string>()
            }
        }
    );
});

// json IgnoreCycles
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

// Register endpoints
app.RegisterAuthorEndpoints();
app.RegisterBookEndpoints();

app.Run();
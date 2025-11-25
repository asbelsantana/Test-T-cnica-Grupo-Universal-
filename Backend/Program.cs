using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PruebaTecnicaUniversal.Data;
using PruebaTecnicaUniversal.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// -----------------------------------------------------
// 1. CONFIGURACIÓN DE LA CADENA JWT
// -----------------------------------------------------
var jwtKey = builder.Configuration["Jwt:Key"] ?? "ClaveSuperSecretaTemporal123!"; 
var keyBytes = Encoding.UTF8.GetBytes(jwtKey);

// -----------------------------------------------------
// 2. REGISTRO DE SERVICIOS
// -----------------------------------------------------
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseInMemoryDatabase("UsersDb");
});

builder.Services.AddScoped<JwtService>();     // NECESARIO
builder.Services.AddScoped<UserService>();

builder.Services.AddHttpClient();

// -----------------------------------------------------
// 3. CONFIGURACIÓN DE AUTHENTICATION Y JWT
// -----------------------------------------------------
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
            ValidateLifetime = true
        };
    });

// -----------------------------------------------------
// 4. CONTROLADORES
// -----------------------------------------------------
builder.Services.AddControllers();

// -----------------------------------------------------
// 5. SWAGGER CONFIG
// -----------------------------------------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Prueba Técnica .NET",
        Version = "v1"
    });

    // JWT en Swagger
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        In = ParameterLocation.Header,
        Description = "Introduce el token JWT con el prefijo Bearer. Ejemplo: Bearer {token}"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// -----------------------------------------------------
// 6. CONFIGURACIÓN DE SWAGGER / MIDDLEWARES
// -----------------------------------------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

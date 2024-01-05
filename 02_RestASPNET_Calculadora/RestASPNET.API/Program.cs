using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using RestASPNET.API.Context;
using RestASPNET.API.Services;
using RestASPNET.API.Services.Implementations;
using Serilog;
using Pomelo.EntityFrameworkCore;
using MySqlConnector;
using EvolveDb;
using System.Reflection.Metadata.Ecma335;
using RestASPNET.API.Persist.Contracts;
using RestASPNET.API.Persist;
using RestASPNET.API.Infraestructure;
using RestASPNET.API.Configurations;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using RestASPNET.API.Infraestructure.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddRouting(Options => Options.LowercaseUrls = true);


var tokenConfigurations = new TokenConfiguration();

new ConfigureFromConfigurationOptions<TokenConfiguration>(
   builder.Configuration.GetSection("TokenConfigurations")
   ).Configure(tokenConfigurations);

builder.Services.AddSingleton(tokenConfigurations);

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters 
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = tokenConfigurations.Issuer,
                ValidAudience = tokenConfigurations.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfigurations.Secret))
            };
        });

builder.Services.AddAuthorization(auth =>
{
    auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser().Build()
        );
});



builder.Services.AddCors(Options => Options.AddDefaultPolicy(builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();


}));


builder.Services.AddControllers();



var connection = builder.Configuration["ConnectionStrings:MySQLConnectionString"];

builder.Services.AddDbContext<MySqlContext>(options =>
    options.UseMySql(
        connection,
        new MySqlServerVersion(new Version(8, 0, 29)))
);

if (builder.Environment.IsDevelopment())
{
    MigrateDatabase(connection);
}

void MigrateDatabase(string? connection)
{
    try
    {
        var evolveConnection = new MySqlConnection(connection);
        var evolve = new Evolve(evolveConnection, Log.Information)
        {
            Locations = new List<string> { "db/migrations", "db/dataset" },
            IsEraseDisabled = true,
        };
        evolve.Migrate();

    }
    catch (Exception ex)
    {
        Log.Error("DataBase migration failed: ", ex.Message);
    }

}

builder.Services.AddApiVersioning();

builder.Services.AddScoped<IPersonPersist, PersonPersist>();
builder.Services.AddScoped<IGeralPersist, GeralPersist>();
builder.Services.AddScoped<IBookPersist, BookPersist>();
builder.Services.AddScoped<IUserPersist, UserPersist>();


builder.Services.AddScoped<ITokenInfrastructure, TokenInfrastructure>();

builder.Services.AddScoped<IPersonService, PersonServiceImplementation>();
builder.Services.AddScoped<IBookService, BookServiceImplementation>();
builder.Services.AddScoped<ILoginService, LoginServiceImplementation>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

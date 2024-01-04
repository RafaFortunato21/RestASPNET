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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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
            Locations = new List<string> { "db/migrations", "db/dataset"},
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

builder.Services.AddScoped<IPersonService, PersonServiceImplementation>();
builder.Services.AddScoped<IBookService, BookServiceImplementation>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

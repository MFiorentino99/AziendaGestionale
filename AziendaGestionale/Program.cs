using AziendaGestionale.Models;
using Microsoft.EntityFrameworkCore;
using Test.Abstractions;
using Test.InterfacesRepository;
using Test.Models;
using Test.Queries;
using Test.Repositories;



var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddDbContext<Context>(opt =>opt.UseInMemoryDatabase("DbAzienda"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProdottiQueries,ProdottiQueries>();
builder.Services.AddScoped<IProdottiRepository,ProdottiRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

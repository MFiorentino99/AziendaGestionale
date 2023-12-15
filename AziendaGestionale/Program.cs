using AziendaGestionale.Controllers;
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

builder.Services.AddScoped<IProdottiQueries, ProdottiQueries>();
builder.Services.AddScoped<IProdottiRepository, ProdottiRepository>();
builder.Services.AddScoped<IGestioneQuery, GestioneQueries>();
builder.Services.AddScoped<IGestioneRepository, GestioneRepository>();
builder.Services.AddScoped<IFattureQueries, FattureQueries>();
builder.Services.AddScoped<IFattureRepository, FattureRepository>();
builder.Services.AddScoped<IDipendentiQueries, DipendentiQueries>();
builder.Services.AddScoped<IDipendentiRepository, DipendentiRepository>();
builder.Services.AddScoped<IDettagliQueries, DettagliQueries>();
builder.Services.AddScoped<IDettagliRepository, DettagliRepository>();
builder.Services.AddScoped<IClientiQueries, ClientiQueries>();
builder.Services.AddScoped<IClientiRepository, ClientiRepository>();
builder.Services.AddScoped<ICliente_FattureQueries, Cliente_FattureQueries>();

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

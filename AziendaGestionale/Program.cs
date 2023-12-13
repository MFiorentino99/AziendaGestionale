using AziendaGestionale.Models;
using Microsoft.EntityFrameworkCore;
using Test.Abstractions;
using Test.Models;
using Test.Queries;



var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddDbContext<Context>(opt =>opt.UseInMemoryDatabase("DbAzienda"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IHttpCall<DTOProdotto>, ProdottiQueries>();
builder.Services.AddSingleton<IHttpCallMoreThanOneId<DTOGestione>, GestioneQueries>();

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

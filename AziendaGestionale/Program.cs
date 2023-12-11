using AziendaGestionale.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.OracleClient;

//var _db = new Oracle.ManagedDataAccess.Client.
//    OracleConnection("User Id=ITS_TEST;Password=ZexpDEV2224;Data Source=localhost");
var builder = WebApplication.CreateBuilder(args);

//_db.Open();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<Context>(opt =>opt.UseInMemoryDatabase("DbAzienda"));
builder.Services.AddSwaggerGen();

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

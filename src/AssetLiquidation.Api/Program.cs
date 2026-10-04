using AssetLiquidation.Core;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
// 1. Adicionar suporte a Controllers REST
builder.Services.AddControllers();
// 2. Configurar o DbContext com Banco de Dados Em Memória
builder.Services.AddDbContext<LiquidateDbContext>(options =>
    options.UseInMemoryDatabase("AssetLiquidationDb"));

// 3. Documentação Swagger/OpenAPI (nativo do .NET)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
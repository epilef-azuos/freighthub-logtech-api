using FreightHub.Shipments.Application;
using FreightHub.Shipments.Infrastructure;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Registar dependências dos módulos
builder.Services.AddShipmentsApplication();
builder.Services.AddShipmentsInfrastructure(builder.Configuration);

// 2. Registar controladores e Swagger (adicionaremos as rotas mais à frente)
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
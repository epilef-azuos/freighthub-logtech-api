using FreightHub.Shipments.Application.Interfaces;
using FreightHub.Shipments.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FreightHub.Shipments.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddShipmentsInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("WriteDatabase");

        services.AddDbContext<ShipmentsDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped<IShipmentWriteRepository, ShipmentWriteRepository>();

        return services;
    }
}

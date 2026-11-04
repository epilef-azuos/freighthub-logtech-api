using FluentValidation;
using FreightHub.Shipments.Application.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace FreightHub.Shipments.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddShipmentsApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        // O MediatR vai varrer este Assembly para encontrar o nosso CreateShipmentCommandHandler
        services.AddMediatR(config => {
            config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        // Regista todos ostypeof(DependencyInjection).Assemblyors do FluentValidation automaticamente
        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}

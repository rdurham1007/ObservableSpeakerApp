using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MassTransit;

namespace CommonComponents.MassTransit
{
    public static class MassTransitServiceExt
    {
        public static IServiceCollection AddServiceBus(this IServiceCollection services, IConfiguration configuration)
        {
            return AddServiceBus(services, configuration, null);
        }

        public static IServiceCollection AddServiceBus(this IServiceCollection services, IConfiguration configuration, Action<IBusRegistrationConfigurator>? cfgMassTransit)
        {

            var mtOptions = new MassTransitOptions(configuration);

            services.AddMassTransit(x =>
            {

                cfgMassTransit?.Invoke(x);


                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(mtOptions.Host, mtOptions.VirtualHost, h =>
                    {
                        h.Username(mtOptions.Username);
                        h.Password(mtOptions.Password);
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });

            return services;
        }
    }
}
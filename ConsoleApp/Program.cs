using EventSource;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                           .ConfigureServices((_, services) =>
                           {
                               services.AddHostedService<EventSender>();
                               services.AddMassTransit(x =>
                               {
                                   x.SetKebabCaseEndpointNameFormatter();
                                   x.UsingRabbitMq((context, cfg) =>
                                   {
                                       cfg.Host("rabbitmq", "/", h =>
                                       {
                                           h.Username("guest");
                                           h.Password("guest");
                                       });
                                       cfg.ConfigureEndpoints(context);
                                   });

                               });
                           }).Build();

            await host.RunAsync();
        }

        public class EventSender : BackgroundService
        {
            private readonly IBus _bus;

            public EventSender(IBus bus) => _bus = bus;

            protected override async Task ExecuteAsync(CancellationToken stoppingToken)
            {
                var source = new CancellationTokenSource(1000 * 10);

                //while(true)
                //{
                //    var endpoint = await _bus.GetPublishSendEndpoint<IEvent>();

                //    await endpoint.Send(new EventBase(DateTime.Now, Guid.NewGuid(), "rubenstozzi@pecege.com", "Olá Evento"), stoppingToken);
                //    await Task.Delay(3000);
                //}
            }
        }
    }
}
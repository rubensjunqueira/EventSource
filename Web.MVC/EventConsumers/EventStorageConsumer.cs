using EventSource;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using Web.MVC.Hubs;

namespace Web.MVC.EventConsumers
{
    public class EventStorageConsumer : IConsumer<IEvent>
    {
        public EventStorageConsumer()
        {
        }

        public async Task Consume(ConsumeContext<IEvent> context)
        {
        }
    }
}

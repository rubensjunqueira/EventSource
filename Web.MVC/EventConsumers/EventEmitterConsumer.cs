using EventSource;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using Web.MVC.Hubs;

namespace Web.MVC.EventConsumers
{
    public class EventEmitterConsumer : IConsumer<IEvent>
    {
        private readonly IDistributedCache _cache;
        private readonly IHubContext<EventHub> _hubContext;

        public EventEmitterConsumer(IHubContext<EventHub> hubContext, IDistributedCache cache)
        {
            _cache = cache;
            _hubContext = hubContext;
        }

        public async Task Consume(ConsumeContext<IEvent> context)
        {
            var @event = context.Message;

            var connectionId = await GetUserCurrentConnection(@event.UserId.ToString());

            var proxyClient = _hubContext.Clients.Client(connectionId);
            await proxyClient.SendAsync("receiveEvent", @event);
        }

        private async Task<string> GetUserCurrentConnection(string userId)
        {
            return await _cache.GetStringAsync(userId);
        }
    }
}

using EventSource;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace SignalR.Event.Hub.EventHub
{
    public class EventHub : Microsoft.AspNetCore.SignalR.Hub, IConsumer<IEvent>
    {
        public void RedirectEvent(IEvent @event)
        {
            var proxy = Clients.Client(@event.UserId.ToString());
            proxy.SendAsync("receiveEvent", @event);
        }

        public Task Consume(ConsumeContext<IEvent> context)
        {
            RedirectEvent(context.Message);
            return Task.CompletedTask;
        }
    }
}

using Microsoft.Extensions.Caching.Distributed;

namespace Web.MVC.Hubs
{
    public class EventHub : Microsoft.AspNetCore.SignalR.Hub
    {
        private readonly IDistributedCache _cache;

        public EventHub(IDistributedCache cache) => _cache = cache;


        public async Task OnConnected(string userId, string connectionId)
        {
            var currentCachedConnectionId = await _cache.GetStringAsync(userId);

            if (string.IsNullOrEmpty(currentCachedConnectionId))
            {
                await _cache.SetStringAsync(userId, connectionId);
            }
            else if (currentCachedConnectionId != connectionId)
            {
                await _cache.SetStringAsync(userId, connectionId);
            }
        }
    }
}

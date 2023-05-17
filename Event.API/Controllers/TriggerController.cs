using EventSource;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Event.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TriggerController : ControllerBase
    {
        private readonly IBus _bus;

        public TriggerController(IBus bus) => _bus = bus;

        [HttpPost]
        [Route("/trigger")]
        public async Task<IActionResult> Trigger()
        {
            await _bus.Publish<IEvent>(new EventBase(DateTime.Now, Guid.NewGuid(), "rubenstozzi@pecege.com", "Teste"));
        }
    }
}

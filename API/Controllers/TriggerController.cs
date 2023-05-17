using EventSource;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TriggerController : ControllerBase
    {
        private readonly IBus _bus;

        public TriggerController(IBus bus) => _bus = bus;

        [Route("/Trigger")]
        [HttpPost]
        public async Task<IActionResult> Trigger()
        {
            var endpoint = await _bus.GetPublishSendEndpoint<IEvent>();
            await endpoint.Send<IEvent>(new EventBase(DateTime.Now, new Guid("de6cd175-68f9-4ce7-bbb7-48a7fd7cf386"), "daoskoasd", "sokaskoaok"));
            return Ok();
        }
    }
}

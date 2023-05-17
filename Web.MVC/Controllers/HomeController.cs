using EventSource;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web.MVC.Models;

namespace Web.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBus _bus;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IBus bus)
        {
            _bus = bus;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult SendEvent()
        {
            _bus.Publish<IEvent>(new EventBase(DateTime.Now, Guid.NewGuid(), "rubenstozzi@pecege.com", "Olá evento"));
            return Ok();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
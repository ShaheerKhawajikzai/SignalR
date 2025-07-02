using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SingalR.Hubs;
using SingalR.Models;

namespace SingalR.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHubContext<NotificationHub> hubContext;

        public HomeController(ILogger<HomeController> logger, IHubContext<NotificationHub> hubContext)
        {
            _logger = logger;
            this.hubContext=hubContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Notify()
        {
            await hubContext.Clients.All.SendAsync("ReceiveNotification", "🔔 This is a live notification!");
            return RedirectToAction("Index");
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

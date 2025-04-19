using HIS.APP.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace TestMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Index page visited at {Time}", DateTime.UtcNow);
            return View();
        }

        public IActionResult Privacy()
        {
            _logger.LogInformation("Privacy page accessed.");
            return View();
        }

        /// <summary>
        /// Returns system/environment info. Useful for health checks or diagnostics.
        /// </summary>
        [HttpGet("Home/SystemInfo")]
        public IActionResult SystemInfo()
        {
            var info = new
            {
                MachineName = Environment.MachineName,
                OSVersion = Environment.OSVersion.ToString(),
                Is64Bit = Environment.Is64BitOperatingSystem,
                Time = DateTime.UtcNow,
                Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
                Uptime = (DateTime.Now - Process.GetCurrentProcess().StartTime).ToString(@"dd\.hh\:mm\:ss")
            };

            _logger.LogInformation("SystemInfo endpoint accessed.");

            return Json(info);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel 
            { 
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier 
            });
        }
    }
}

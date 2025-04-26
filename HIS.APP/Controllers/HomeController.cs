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

        [HttpGet]
        public IActionResult Index()
        {
            var stopwatch = Stopwatch.StartNew();
            _logger.LogInformation("Index page visited at {Time}", DateTime.UtcNow);

            var result = View();
            stopwatch.Stop();
            _logger.LogInformation("Index action executed in {ElapsedMilliseconds} ms", stopwatch.ElapsedMilliseconds);

            return result;
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            _logger.LogInformation("Privacy page accessed at {Time}", DateTime.UtcNow);
            return View();
        }

        /// <summary>
        /// Returns detailed system/environment info. Useful for diagnostics and health checks.
        /// </summary>
        [HttpGet("api/system-info")]
        public IActionResult SystemInfo()
        {
            try
            {
                var process = Process.GetCurrentProcess();

                var info = new
                {
                    MachineName = Environment.MachineName,
                    OSVersion = Environment.OSVersion.ToString(),
                    Is64Bit = Environment.Is64BitOperatingSystem,
                    ProcessorCount = Environment.ProcessorCount,
                    CurrentUser = Environment.UserName,
                    Time = DateTime.UtcNow,
                    Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
                    Uptime = (DateTime.Now - process.StartTime).ToString(@"dd\.hh\:mm\:ss"),
                    MemoryUsedMB = (process.WorkingSet64 / (1024 * 1024)).ToString("N0") + " MB"
                };

                _logger.LogInformation("SystemInfo endpoint accessed at {Time}", DateTime.UtcNow);
                return Json(info);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving system information.");
                return StatusCode(500, "Failed to retrieve system info.");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet]
        public IActionResult Error()
        {
            var requestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogError("An error occurred. RequestId: {RequestId}", requestId);

            return View(new ErrorViewModel
            {
                RequestId = requestId
            });
        }
    }
}

using System.Diagnostics;
using CarDealerApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarDealerApp.Controllers //hangi talebe nasýl cevap vericeðimiz classlar
{
    public class HomeController(CarDealerDbContext dbContext) : Controller  //mvc kütüphanesindeki controller classýný miras aldýk(microsoftun yazdýðý class)
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index() //index isimli css html dosyasýný arar
        {
            return View();  // view methodu views in içinde index.cshtml arar.
        }

        public IActionResult Privacy()
        {
            return View(); //view methodu views in içinde Privacy.cshtml arar.
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

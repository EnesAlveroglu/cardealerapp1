using System.Diagnostics;
using CarDealerApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarDealerApp.Controllers //hangi talebe nasýl cevap vericeðimiz classlar
{
    public class HomeController(CarDealerDbContext dbContext) : Controller  //mvc kütüphanesindeki controller classýný miras aldýk(microsoftun yazdýðý class) DbContext classýný inject ettik
    {
       
        public IActionResult Index() //index isimli css html dosyasýný arar
        {
            ViewBag.Makes = dbContext.Makes.ToList();  //controllerdan gelen property viewBag bunun içine markalar listesini koyduk.
            return View();  // view methodu views in içinde index.cshtml arar.
        }

        public IActionResult Privacy()
        {
            return View(); //view methodu views in içinde Privacy.cshtml arar.
        }

        public IActionResult Make()
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

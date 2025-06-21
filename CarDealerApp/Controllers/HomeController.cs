using System.Diagnostics;
using CarDealerApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarDealerApp.Controllers //hangi talebe nas�l cevap verice�imiz classlar
{
    public class HomeController(CarDealerDbContext dbContext) : Controller  //mvc k�t�phanesindeki controller class�n� miras ald�k(microsoftun yazd��� class) DbContext class�n� inject ettik
    {
       
        public IActionResult Index() //index isimli css html dosyas�n� arar
        {
            ViewBag.Makes = dbContext.Makes.ToList();  //controllerdan gelen property viewBag bunun i�ine markalar listesini koyduk.
            return View();  // view methodu views in i�inde index.cshtml arar.
        }

        public IActionResult Privacy()
        {
            return View(); //view methodu views in i�inde Privacy.cshtml arar.
        }

        public IActionResult Make()
        {
            return View(); //view methodu views in i�inde Privacy.cshtml arar.
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

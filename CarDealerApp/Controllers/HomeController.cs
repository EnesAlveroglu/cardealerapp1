using System.Diagnostics;
using CarDealerApp.Domain;
using CarDealerApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarDealerApp.Controllers //hangi talebe nas�l cevap verice�imiz classlar
{
    public class HomeController(CarDealerDbContext dbContext) : Controller  //mvc k�t�phanesindeki controller class�n� miras ald�k(microsoftun yazd��� class) DbContext class�n� inject ettik
    {

        public IActionResult Index() //index isimli css html dosyas�n� arar
        {
            ViewBag.Makes = dbContext
                .Makes
                .OrderBy(p=>p.Sort)
                .ToList();  //controllerdan gelen property viewBag bunun i�ine markalar listesini koyduk.
            ViewBag.Models = dbContext
                .Models
                .OrderBy(p => p.Name)
                .ToList();
            ViewBag.Vehicles = dbContext
                .Vehicles
                .Include(p=>p.Model).ThenInclude(p=>p.Make)//Include ThenInclude tablolara ula�mak i�in kullan�l�r.
                .Where(p=>p.Featured)
                .Take(7).ToList(); //Take(6) = 6 tane sat�r �ek.
            return View();  // view methodu views in i�inde index.cshtml arar. //ViewBagler otomatik olarak index sayfas�nda g�z�k�r bu y�zden Index.cshtml i�inde yukar� adresini yazmaya gerek yoktur. ama de�i�ken i�ine koyulmu�sa modeller viewe d�nd�rmemiz gerekir. ko�ul where veya single ile veilir!!!
        }

        public IActionResult Privacy()
        {
            return View(); //view methodu views in i�inde Privacy.cshtml arar.
        }

        public IActionResult Make(Guid id)
        {
            var vehicles = dbContext.Makes.
                Include(p => p.Models)
                .ThenInclude(p => p.Vehicles)
                .Single(p => p.Id == id);

           // var vehicles = dbContext
             //   .Vehicles
               // .Include(p => p.Model).ThenInclude(p => p.Make)
                //.Where(p => p.Model.MakeId == id) //Where ile ara�lar filtreleniyor (Marka baz�nda filtreleniyor ama d�n�� arac�n kendisi).Include ve ThenInclude ile ili�kili Model ve Make nesneleri ara�larla birlikte y�kleniyor.
                //.ToList();

                 
            
            return View(vehicles); 
        }

        public IActionResult Detail(Guid id) // urlde gelen id yi i�ine koyduk.
        {
            var vehicle = dbContext
                .Vehicles
                .Include(p => p.Model).ThenInclude(p=> p.Make) // model ve marka tablosunu �ektik.
                .Single(p=>p.Id == id); //fonsiyon veriyoruz kay�tlara uyguluyor. Id'si parametre ile ayn� olan bir tane kayd� d�nd�r.Single bir sat�r d�nd�r�r.
            return View(vehicle); 
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}


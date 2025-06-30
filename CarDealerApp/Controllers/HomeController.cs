using System.Diagnostics;
using CarDealerApp.Domain;
using CarDealerApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarDealerApp.Controllers //hangi talebe nasýl cevap vericeðimiz classlar
{
    public class HomeController(CarDealerDbContext dbContext) : Controller  //mvc kütüphanesindeki controller classýný miras aldýk(microsoftun yazdýðý class) DbContext classýný inject ettik
    {

        public IActionResult Index() //index isimli css html dosyasýný arar
        {
            ViewBag.Makes = dbContext
                .Makes
                .OrderBy(p=>p.Sort)
                .ToList();  //controllerdan gelen property viewBag bunun içine markalar listesini koyduk.
            ViewBag.Models = dbContext
                .Models
                .OrderBy(p => p.Name)
                .ToList();
            ViewBag.Vehicles = dbContext
                .Vehicles
                .Include(p=>p.Model).ThenInclude(p=>p.Make)//Include ThenInclude tablolara ulaþmak için kullanýlýr.
                .Where(p=>p.Featured)
                .Take(7).ToList(); //Take(6) = 6 tane satýr çek.
            return View();  // view methodu views in içinde index.cshtml arar. //ViewBagler otomatik olarak index sayfasýnda gözükür bu yüzden Index.cshtml içinde yukarý adresini yazmaya gerek yoktur. ama deðiþken içine koyulmuþsa modeller viewe döndürmemiz gerekir. koþul where veya single ile veilir!!!
        }

        public IActionResult Privacy()
        {
            return View(); //view methodu views in içinde Privacy.cshtml arar.
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
                //.Where(p => p.Model.MakeId == id) //Where ile araçlar filtreleniyor (Marka bazýnda filtreleniyor ama dönüþ aracýn kendisi).Include ve ThenInclude ile iliþkili Model ve Make nesneleri araçlarla birlikte yükleniyor.
                //.ToList();

                 
            
            return View(vehicles); 
        }

        public IActionResult Detail(Guid id) // urlde gelen id yi içine koyduk.
        {
            var vehicle = dbContext
                .Vehicles
                .Include(p => p.Model).ThenInclude(p=> p.Make) // model ve marka tablosunu çektik.
                .Single(p=>p.Id == id); //fonsiyon veriyoruz kayýtlara uyguluyor. Id'si parametre ile ayný olan bir tane kaydý döndür.Single bir satýr döndürür.
            return View(vehicle); 
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}


using CarDealerApp.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace CarDealerApp.Areas.Admin.Controllers;

//CRUD = DÖRT İŞLEM = Yeni kayıt oluşturma, güncelleme , silme , raporlama
[Area("Admin")]
public class VehiclesController(CarDealerDbContext dbContext) : Controller //bütün database işlemlerini yaptığımız dbContext classını inject ettik.
{
    public IActionResult Index()
    {
        var model = dbContext.Vehicles.Include(p=>p.Model).ThenInclude(p=>p.Make).ToList(); //veritabanından kullanıcının girdiği bilgileri Orderby(alfabe sırasına göre yukarıdan aşağıya(büyükten küçüğe) çektik.
          //include ile modelin içine eklenen make modelini ve vehicle modelinin listesini çağırdık.
        return View(model); //model olarak indexe gönderdik.

    }

    public IActionResult Create() // verilerin View’a taşınması için GET metodunun çalışması gerekir. bu yüzden viewbag [HttpPost] içinde değil. ayrı bir create actionun da.
    {
        
        ViewBag.Models = new SelectList(dbContext.Models.OrderBy(p => p.Name), "Id", "Name"); //model listesini SelectList kullanarak ViewBag.Models içine koyduk. "Id","Name" = Id'sini value olarak kullan(databaseye göndermek için) Name'ini kullanıcıya display(varsayılan) olarak göster.
        return View();
        
    }
   
    [HttpPost]
    public IActionResult Create(Vehicle model) //post metoduyla gelen verileri Vehicle modelin içine koyduk. Kullanıcının doldurduğu veriler Vehicle modelin içinde geldi.
    {
        if (model.PhotoFile != null ) //PhotoFile boş değilse  
        {
            using var ms = new MemoryStream(); // Bellekte geçici bir MemoryStream(ram de geçici alan) oluştur
            model.PhotoFile.OpenReadStream().CopyTo(ms); //// Yüklenen dosyayı stream'e kopyala
            model.Photo = ms.ToArray(); // Stream'i byte[]'e çevirip veritabanına kaydedilecek alana ata
        }
        dbContext.Vehicles.Add(model); //dbSetin içine ekledik.
        dbContext.SaveChanges(); //databaseye gönderdi.
        return RedirectToAction(nameof(Index)); //verileri indexe döndürdük.
    }

    public IActionResult Edit(Guid id) // url'deki id'yi aldık.
    {

        ViewBag.Models = new SelectList(dbContext.Models.OrderBy(p => p.Name), "Id", "Name"); //eğer viewBag.Models ve Edit.cshtml de asp-for="ModelId" asp-items="@ViewBag.Models" bu kısımlar olmaz ise modelid si null gittiği için databaseye kaydederken null olarak kaydetmeye çalışıyor. çünkü .WithOne(p => p.Model) her aracın modeli var diye configuration da tanımladığımız için araçtaki public Guid ModelId { get; set; } null olamayacağı için kaydettirmez.
        var model = dbContext.Vehicles.Find(id); //dbContext classının Vehicles dbSetinin find methodu ile primary keyini gönderiyoruz o bize databaseden satırı bulup veriyor. Hangi modeli çekmek istediğimiz bulduk databaseden çektik View içinde göstericez.
        //item.id urlye koyduk yani markanın id'sini şimdi databaseye id si bu olan kaydı bana ver dememiz lazım.
        return View(model);
    }


    [HttpPost]
    public IActionResult Edit(Guid id, Vehicle model) //url'deki id aynı kalır. kullanıcının girdiği bilgiyi modele koydu.
    {
        if (model.PhotoFile != null) //PhotoFile boş değilse  
        {
            using var ms = new MemoryStream(); // Bellekte geçici bir MemoryStream(ram de geçici alan) oluştur
            model.PhotoFile.OpenReadStream().CopyTo(ms); //// Yüklenen dosyayı stream'e kopyala
            model.Photo = ms.ToArray(); // Stream'i byte[]'e çevirip veritabanına kaydedilecek alana ata
        }
        dbContext.Vehicles.Update(model);  // modelin güncellenmesini sağladı.
        dbContext.SaveChanges(); // Databaseye gönderdik.
        return RedirectToAction(nameof(Index)); //indexe yeni halini döndürdük.
    }

    public IActionResult Delete(Guid id) // url'deki id'yi aldık.
    {

        var model = dbContext.Vehicles.Where(p => p.Id == id).ExecuteDelete(); //where() = parametresine bir fonksiyon(iş) girince id'ye uyan satırı seçer. ExecuteDelete ile siler.
        return RedirectToAction(nameof(Index));
    }

}










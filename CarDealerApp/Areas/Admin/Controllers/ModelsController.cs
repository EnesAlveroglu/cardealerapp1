using CarDealerApp.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CarDealerApp.Areas.Admin.Controllers;

//CRUD = DÖRT İŞLEM = Yeni kayıt oluşturma, güncelleme , silme , raporlama
[Area("Admin")]
public class ModelsController(CarDealerDbContext dbContext) : Controller //bütün database işlemlerini yaptığımız dbContext classını inject ettik.
{
    public IActionResult Index()
    {
        var model = dbContext.Models.Include(p=>p.Make).OrderBy(p => p.Name).ToList(); //veritabanından kullanıcının girdiği bilgileri Orderby(alfabe sırasına göre yukarıdan aşağıya(büyükten küçüğe) çektik.
          //include ile modelin içine eklenen marka modelini çağırdık.
        return View(model); //model olarak indexe gönderdik.

    }

    public IActionResult Create() // verilerin View’a taşınması için GET metodunun çalışması gerekir. bu yüzden viewbag [HttpPost] içinde değil. ayrı bir create actionun da.
    {
        ViewBag.Makes = new SelectList( dbContext.Makes.OrderBy(p => p.Name),"Id","Name"); //marka listesini SelectList kullanarak ViewBag.Makes içine koyduk. "Id","Name" = Id'sini value olarak kullan Name'ini kullanıcıya display(varsayılan) olarak göster.
        return View();
    }

    [HttpPost]
    public IActionResult Create(Model model) //post metoduyla gelen verileri Model modelin içine koyduk. Kullanıcının doldurduğu veriler Model modelin içinde geldi.
    {
        dbContext.Models.Add(model); //dbSetin içine ekledik.
        dbContext.SaveChanges(); //databaseye gönderdi.
        return RedirectToAction(nameof(Index)); //verileri indexe döndürdük.
    }

    public IActionResult Edit(Guid id) // url'deki id'yi aldık.
    {

        ViewBag.Makes = new SelectList(dbContext.Makes.OrderBy(p => p.Name), "Id", "Name"); //eğer viewBag.Makes ve Edit.cshtml de asp-for="MakeId" asp-items="@ViewBag.Makes" bu kısımlar olmaz ise makeid si null gittiği için databaseye kaydederken null olarak kaydetmeye çalışıyor. çünkü .WithOne(p => p.Make) her modelin markası var diye configuration da tanımladığımız için Modeldeki public Guid MakeId { get; set; } null olamayacağı için kaydettirmez.
        var model = dbContext.Models.Find(id); //dbContext classının Models dbSetinin find methodu ile primary keyini gönderiyoruz o bize databaseden satırı bulup veriyor. Hangi modeli çekmek istediğimiz bulduk databaseden çektik View içinde göstericez.
        //item.id urlye koyduk yani markanın id'sini şimdi databaseye id si bu olan kaydı bana ver dememiz lazım.
        return View(model);
    }


    [HttpPost]
    public IActionResult Edit(Guid id, Model model) //url'deki id aynı kalır. kullanıcının girdiği bilgiyi modele koydu.
    {
        dbContext.Models.Update(model);  // modelin güncellenmesini sağladı.
        dbContext.SaveChanges(); // Databaseye gönderdik.
        return RedirectToAction(nameof(Index)); //indexe yeni halini döndürdük.
    }

    public IActionResult Delete(Guid id) // url'deki id'yi aldık.
    {

        var model = dbContext.Models.Where(p => p.Id == id).ExecuteDelete(); //where() = parametresine bir fonksiyon(iş) girince id'ye uyan satırı seçer. ExecuteDelete ile siler.
        return RedirectToAction(nameof(Index));
    }

}










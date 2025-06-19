using CarDealerApp.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarDealerApp.Areas.Admin.Controllers;

//CRUD = DÖRT İŞLEM = Yeni kayıt oluşturma, güncelleme , silme , raporlama
[Area("Admin")]
public class MakesController(CarDealerDbContext dbContext) : Controller //bütün database işlemlerini yaptığımız dbContext classını inject ettik.
{
    public IActionResult Index()
    {
        var model = dbContext.Makes.OrderBy(p => p.Name).ToList(); //veritabanından kullanıcının girdiği bilgileri Orderby(alfabe sırasına göre yukarıdan aşağıya(büyükten küçüğe) çektik.
        return View(model); //model olarak indexe gönderdik.
    }

    public IActionResult Create()
    {

        return View();
    }

    [HttpPost]
    public IActionResult Create(Make model) //post metoduyla gelen verileri make modelin içine koyduk. Kullanıcının doldurduğu veriler make modelin içinde geldi.
    {
        dbContext.Makes.Add(model); //dbSetin içine ekledik.
        dbContext.SaveChanges(); //databaseye gönderdi.
        return RedirectToAction(nameof(Index)); //verileri indexe döndürdük.
    }

    public IActionResult Edit(Guid id) // url'deki id'yi aldık.
    {
        var model = dbContext.Makes.Find(id); //dbContext classının Makes dbSetinin find methodu ile primary keyini gönderiyoruz o bize databaseden satırı bulup veriyor. Hangi markayı çekmek istediğimiz bulduk databaseden çektik View içinde göstericez.
        //item.id urlye koyduk yani markanın id'sini şimdi databaseye id si bu olan kaydı bana ver dememiz lazım.
        return View(model);
    }


    [HttpPost]
    public IActionResult Edit(Guid id, Make model) //url'deki id aynı kalır. modeli çağırdı.
    {
        dbContext.Makes.Update(model);  // markayı güncelledikten sonraki hali.
        dbContext.SaveChanges(); // Databaseye gönderdik.
        return RedirectToAction(nameof(Index)); //indexe yeni halini döndürdük.
    }

    public IActionResult Delete(Guid id) // url'deki id'yi aldık.
    {

        var model = dbContext.Makes.Where(p => p.Id == id).ExecuteDelete(); //where() = parametresine bir fonksiyon(iş) girince id'ye uyan satırı seçer. ExecuteDelete ile siler.
        return RedirectToAction(nameof(Index));
    }

}










using CarDealerApp;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();  //AddControllersWithViews adlý methhod dependencyInjection listesine birþey ekliyor. bu bir siteyi çalýþtýrdýðýmýz zaman// home.Controller.cs ve home/ýndex.cshtml yi projemize tanýtmýþýz. model veriyi modeller. controller hangi requeste nasýl cevap vericeðini tanýmlar. verilen cevabýn da sunumu vievde olur(htmlsi cssi) buunun gibi servis listemize dbcontext ekleyelim.
//DependencyInjection = Dizayn peter(Tasarým Deseni) Yazýlým camiasýnda bazý problemleri çözmek için bazý yöntemler bulmuþlar. bir ihtiyaca karþý kurulmuþ yöntemler bütünü
//Yaptýðý Ýþ = Bir class listesi var. Bu classý Listede tutuyoruz.Methoda parametre olarak kullandýðýmýz zaman ýnject edip(tasarlayýp) bize veriyor. Classýn instance(nesne) deðil de interfacesini tutar ram de yer tutmasýn diye.
builder.Services.AddDbContext<CarDealerDbContext>(Config =>
{
    Config.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
    //ConnectionString(baðlantý ayarlarý) = veritabaný hangi bilgisayarda , kullanýcý adý nedir , hangi parola ile baðlanýcaðýný belirttik.
    //msssql kullanýcaðýmýz için msssqlin kütüphanesini ekliyoruz.
    //hangi databaseye hangi þartlarda baðlanýlacaðýný tarif ettik(Dependecy injection ConnectionSring)
});

 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")  //launchSettings.json dosyasýndan istek atýnca burda url de olan ilk parça controllerýn ismidir. ikinci parça action un ismidir. Üçüncü parçada id vardýr. eðer urlye yazýlmaz ise olduðu gibi göster.
    .WithStaticAssets();


app.Run();

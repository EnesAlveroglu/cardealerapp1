using CarDealerApp;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();  //AddControllersWithViews adl� methhod dependencyInjection listesine bir�ey ekliyor. bu bir siteyi �al��t�rd���m�z zaman// home.Controller.cs ve home/�ndex.cshtml yi projemize tan�tm���z. model veriyi modeller. controller hangi requeste nas�l cevap verice�ini tan�mlar. verilen cevab�n da sunumu vievde olur(htmlsi cssi) buunun gibi servis listemize dbcontext ekleyelim.
//DependencyInjection = Dizayn peter(Tasar�m Deseni) Yaz�l�m camias�nda baz� problemleri ��zmek i�in baz� y�ntemler bulmu�lar. bir ihtiyaca kar�� kurulmu� y�ntemler b�t�n�
//Yapt��� �� = Bir class listesi var. Bu class� Listede tutuyoruz.Methoda parametre olarak kulland���m�z zaman �nject edip(tasarlay�p) bize veriyor. Class�n instance(nesne) de�il de interfacesini tutar ram de yer tutmas�n diye.
builder.Services.AddDbContext<CarDealerDbContext>(Config =>
{
    Config.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
    //ConnectionString(ba�lant� ayarlar�) = veritaban� hangi bilgisayarda , kullan�c� ad� nedir , hangi parola ile ba�lan�ca��n� belirttik.
    //msssql kullan�ca��m�z i�in msssqlin k�t�phanesini ekliyoruz.
    //hangi databaseye hangi �artlarda ba�lan�laca��n� tarif ettik(Dependecy injection ConnectionSring)
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
    pattern: "{controller=Home}/{action=Index}/{id?}")  //launchSettings.json dosyas�ndan istek at�nca burda url de olan ilk par�a controller�n ismidir. ikinci par�a action un ismidir. ���nc� par�ada id vard�r. e�er urlye yaz�lmaz ise oldu�u gibi g�ster.
    .WithStaticAssets();


app.Run();

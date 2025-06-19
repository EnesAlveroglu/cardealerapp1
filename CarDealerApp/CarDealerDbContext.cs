using CarDealerApp.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace CarDealerApp;

public class CarDealerDbContext(DbContextOptions options): DbContext(options) //veritabanıyla ilgili her türlü iş veritabanı oluştuur,sorgu çalıştır,kayıt ekle,kayıt sil,kayıt listele,rapor çek,kayıtları düzenle somutlaştırır gerçek hale dönüştürür.

    //public class CarDelerDbContext : DbContext
    //{

   //public class CarDelerDbContext(DbContextOptions options) : base(options)
   //{

   //}
{

    protected override void OnModelCreating(ModelBuilder modelBuilder) // microsoft entity framework database oluştırurken bu kodları çalışıtırır. configutationları çağırır.
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CarDealerDbContext).Assembly); //kodun ezilmiş hali
    }

    public DbSet<Make> Makes { get; set; }
    public DbSet<Model> Models { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }

}

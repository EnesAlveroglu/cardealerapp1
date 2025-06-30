using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarDealerApp.Domain;

    public class Model
    {
        public Guid Id { get; set; }
        public Guid MakeId { get; set; }
        public required string Name { get; set; }
       public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
        public Make? Make { get; set; }   //marka classında zorunlu(required) alanlar olduğu için ad girmeden marka oluşmuyor. Bu yüzden Marka boş olabilir diyoruz. Veritabanından veri çekerken bazen sadece model çekilir bazen make tablosu da dahil edilip hepsi çekilir bu durumlarda null olabilir.(navigasyon property) 
          //Find ile sadece modelin özellikleri çekilir.(MakeId dahil) Include ile make tablosu da dahil edilip çekilir.
}

public class ModelConfiguration : IEntityTypeConfiguration<Model>
{
    public void Configure(EntityTypeBuilder<Model> builder)
    {
        builder
            .HasMany(p => p.Vehicles) 
            .WithOne(p => p.Model)   
            .HasForeignKey(p => p.ModelId) 
            .OnDelete(DeleteBehavior.Restrict);
            
    }
}

/*
 * one to many
 * many to many
 * zero or one to many
 * one to one
 * 
 */
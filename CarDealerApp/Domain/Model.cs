using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarDealerApp.Domain;

    public class Model
    {
        public Guid Id { get; set; }
        public Guid MakeId { get; set; }
        public required string Name { get; set; }
        public Make? Make { get; set; }   //marka classında zorunlu(required) alanlar olduğu için ad girmeden marka oluşmuyor. Bu yüzden Marka boş olabilir diyoruz.Model oluşturmadan marka oluşturmuyoruz.
       public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
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
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarDealerApp.Domain;

    public class Make
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public ICollection<Model> Models { get; set; } = new List<Model>(); // new List<Model>(); içine birden fazla model atmayı sağlar.


}


public class MarkaConfiguration : IEntityTypeConfiguration<Make> //IEntityTypeConfiguration interface'dir classın içinde ne olucağını söyler. Bu İnterface diyor ki beni tanımlarsan aşağıda ki methodu koymak zorundasın.
{
    public void Configure(EntityTypeBuilder<Make> builder) //veritabanı tablosu oluştururken şunu şöyle yap böyle yap diyecek.
    {
        builder
            .HasMany(p => p.Models) // markanın modelleri olur (Marka => Marka.Modeller)
            .WithOne(p => p.Make)   //modelin bir markası olur  (Model => Model.Marka)
            .HasForeignKey(p => p.MakeId) // (Model => Model.MarkaId)
             .OnDelete(DeleteBehavior.Restrict); // silinmemesi gerektiği için. Yoksa mantıksız olur.
        // .OnDelete(DeleteBehavior.Cascade); //cascade=basamaklamak Bir markayı silersem onunu da databaseleri otomatik silinsin demek.
    }
}

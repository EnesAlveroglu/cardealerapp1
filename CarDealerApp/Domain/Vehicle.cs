using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealerApp.Domain;

public class Vehicle
{
    public Guid Id { get; set; }
    public required Guid ModelId { get; set; }
    public decimal Price { get; set; }
    public byte[]? Photo { get; set; } //bu, fotoğrafın veritabanına kaydedilecek halini tutar.
    public int Km { get; set; }
    public bool Featured { get; set; } = false;  //Anasayfada gösterilsin mi gösterilmesin mi onu belirliyoruz.
    public string? Description { get; set; }

    [NotMapped]
    public IFormFile? PhotoFile { get; set; } //Bu ise kullanıcının formdan yüklediği dosyayı geçici olarak tutar. PhotoFile → Kullanıcı formda bir dosya seçerse, bu alana gelir. kullanıcının formdan gönderdiği dosyayı geçici olarak alır, ama bu veri doğrudan veritabanına kaydedilmez.
    public Model? Model { get; set; }

}
public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
     

    }
}
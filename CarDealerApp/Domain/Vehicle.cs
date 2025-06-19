using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarDealerApp.Domain;

public class Vehicle
{
    public Guid Id { get; set; }
    public required Guid ModelId { get; set; }
    public decimal Price { get; set; }
    public byte[]? Photo { get; set; }
    public int Km { get; set; }
    public string? Description { get; set; }
    public Model? Model { get; set; }

}
public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
     

    }
}
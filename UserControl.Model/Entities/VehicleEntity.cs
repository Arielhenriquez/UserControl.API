using UserControl.Core.Enums;

namespace UserControl.Model.Entities;

public class Vehicle
{
    public int Id { get; set; }

    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string Plate { get; set; } = string.Empty;
    public bool IsAvailable { get; set; }

    public FuelType Fuel { get; set; }

    public List<string> Features { get; set; } = new();

    public DateTime ManufactureDate { get; set; }

    public decimal Price { get; set; }

    public double MileageKm { get; set; }
}

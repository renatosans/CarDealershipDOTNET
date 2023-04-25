using System.ComponentModel.DataAnnotations.Schema;


namespace CarDealershipDOTNET.Models;

// TODO: use annotation to ajust tablename to snakecase
[Table("cars_for_sale")]
public class CarsForSale
{
    [Key]
    public int id { get; set; }
    public string brand { get; set; } = null!;
    public string model { get; set; } = null!;
    public int year { get; set; }
    public string? img { get; set; }
    public string? color { get; set; }
    public int? mileage { get; set; }
    public string? category { get; set; }
    public decimal price { get; set; }
}

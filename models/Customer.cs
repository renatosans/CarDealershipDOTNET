using System.ComponentModel.DataAnnotations.Schema;


namespace CarDealershipDOTNET.Models;

[Table("customer")]
public partial class Customer
{
    [Key]
    public int id { get; set; }
    public string first_name { get; set; } = null!;
    public string last_name { get; set; } = null!;
    public DateOnly birth_date { get; set; }
    public string? email { get; set; }
    public string? phone { get; set; }
}

using System.ComponentModel.DataAnnotations.Schema;


namespace CarDealershipDOTNET.Models;

[Table("salesperson")]
public partial class Salesperson
{
    [Key]
    public int id { get; set; }
    public string first_name { get; set; } = null!;
    public string last_name { get; set; } = null!;
    public decimal commission { get; set; }
}

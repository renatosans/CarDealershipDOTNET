using System.ComponentModel.DataAnnotations.Schema;


namespace CarDealershipDOTNET.Models;

[Table("invoice")]
public partial class Invoice
{
    [Key]
    public int id { get; set; }
    public int customer_id { get; set; }
    public int salesperson_id { get; set; }
    public int car_id { get; set; }
    public int? amount { get; set; }
}

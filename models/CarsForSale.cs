using System.ComponentModel.DataAnnotations.Schema;


namespace APIDemo.Models
{
    // TODO: use annotation to ajust tablename to snakecase

    [Table("cars_for_sale")]
    public class CarsForSale
    {
        [Key]
        public int id { get; set; }
        public string nome { get; set; }
        public decimal preco { get; set; }
        public DateTime dataCriacao { get; set; }
    }

    class CarDealershipDB : DbContext
    {
        public CarDealershipDB(DbContextOptions<CarDealershipDB> options) : base(options) { }
        public DbSet<CarsForSale> Cars => Set<CarsForSale>();
    }
}

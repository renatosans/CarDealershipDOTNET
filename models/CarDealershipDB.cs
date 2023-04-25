using System;
using Microsoft.EntityFrameworkCore;


namespace CarDealershipDOTNET.Models;


public class CarDealershipDB : DbContext
{
    public CarDealershipDB(DbContextOptions<CarDealershipDB> options): base(options) { }

    public DbSet<CarsForSale> Cars => Set<CarsForSale>();

    public DbSet<Customer> Customers => Set<Customer>();

    public DbSet<Invoice> Invoices => Set<Invoice>();

    public DbSet<Salesperson> Salespeople => Set<Salesperson>();
}

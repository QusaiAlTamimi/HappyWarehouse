using HappyWarehouseAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace HappyWarehouseAPI.Data
{
    public class DataContext : IdentityDbContext<ApplicationUser> 
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        //public DbSet<User> Users => Set<User>();
        public DbSet<Warehouse> Warehouses => Set<Warehouse>();
        public DbSet<Item> Items => Set<Item>();
    }
}

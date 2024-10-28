using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using HappyWarehouseCore.Models;
using Microsoft.Extensions.Logging.Abstractions;

namespace HappyWarehouseData.DataAccess
{
    public class DataContext : IdentityDbContext<ApplicationUser> 
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        //public DbSet<User> Users => Set<User>();
        public DbSet<Warehouse> Warehouses => Set<Warehouse>();
        public DbSet<Item> Items => Set<Item>();
        public DbSet<Logs> Logs => Set<Logs>();

    }
}

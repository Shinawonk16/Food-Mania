using Food_Mania.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Food_Mania.Models.ApplicationContext
{
    public class Context:DbContext
    {
        public Context(DbContextOptions<Context> dbContextOptions): base(dbContextOptions)
        {
            
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<FoodOrder> FoodOrder { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using ProductManagementAPI.Dto;

namespace ProductManagementAPI.Data 
{
    public class ApplicationDbContext : DbContext 
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { 
        
        }

        public DbSet<Product> Product { get; set; } 

        public DbSet<Item> Item { get; set;}
    }
}

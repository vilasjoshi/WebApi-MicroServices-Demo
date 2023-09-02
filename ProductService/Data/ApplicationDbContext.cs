using Microsoft.EntityFrameworkCore;
using ProductService.Models;
namespace ProductService.Data
{
    public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Product> Products { get; set; }        
	}
}


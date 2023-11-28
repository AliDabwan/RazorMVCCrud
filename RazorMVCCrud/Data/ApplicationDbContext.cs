using Microsoft.EntityFrameworkCore;
using RazorMVCCrud.Models;

namespace RazorMVCCrud.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }


        public DbSet<Product> Products { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using MyStore.Models;

namespace MyStore.Data;

public class AppDbContext:DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options):base(options){}
    public DbSet<Product> Products { get; set; }
    
}
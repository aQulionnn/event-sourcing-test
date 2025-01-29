using Microsoft.EntityFrameworkCore;
using ui.Entities;

namespace ui;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Book> Books { get; set; }
}
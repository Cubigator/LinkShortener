using LinkShortenerDatabaseLib.Entities;
using Microsoft.EntityFrameworkCore;

namespace LinkShortenerDatabaseLib;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions options) : base(options)
    {
        
    }

    public DbSet<Link> Links { get; set; }
}

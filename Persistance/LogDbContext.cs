using LogWatcher.Models;
using Microsoft.EntityFrameworkCore;

namespace LogWatcher.Persistance
{
    public class LogDbContext : DbContext
    {
        public LogDbContext(DbContextOptions<LogDbContext> dbContext) : base(dbContext) { }

        public DbSet<LogFile> LogFiles { get; set; }
        
    }
}
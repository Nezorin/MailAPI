using DataAccesLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccesLayer
{
    public class DataContext : DbContext
    {
        public DbSet<Mail> Mails { get; set; }
        public DataContext()
        {
        }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}

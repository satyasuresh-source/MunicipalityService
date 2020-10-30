using Microsoft.EntityFrameworkCore;

namespace MunicipalityService.DataAccess
{
    public class MunicipalityDataContext : DbContext
    {
        public DbSet<Tax> Taxes { get; set; }
        public DbSet<Municipalities> Municipalities { get; set; }
        public DbSet<Frequency> Frequencies { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                  @"Server=localhost\SQLEXPRESS;Database=Municipality;Integrated Security=True");
        }
    }
}

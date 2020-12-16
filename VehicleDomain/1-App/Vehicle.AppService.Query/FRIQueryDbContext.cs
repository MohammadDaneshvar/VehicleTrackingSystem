using Microsoft.EntityFrameworkCore;
using Vehicle.Infra.Persistance.EF;

namespace Infra.Persistance.EF
{
    public class FRIQueryDbContext : DbContext
    {

        public FRIQueryDbContext()
        {

        }
        public FRIQueryDbContext(DbContextOptions<FRIQueryDbContext> options) : base(options)
        {
        }

       
        

        protected internal virtual void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(VehicleDbContext).Assembly); // Here UseConfiguration is any IEntityTypeConfiguration
        }
        private static DbContextOptions GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        }
    }
}

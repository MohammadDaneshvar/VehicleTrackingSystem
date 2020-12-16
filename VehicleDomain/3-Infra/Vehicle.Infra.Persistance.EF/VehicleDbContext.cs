using Framework.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Vehicle.Domain;
using Vehicle.Domain.Logs;
using Vehicle.Domain.PropertyTypes;
using Vehicle.Domain.VehiclePositions;
using Vehicle.Domain.VehicleProperties;
using Vehicle.Domain.Vehicles;

namespace Vehicle.Infra.Persistance.EF
{
    public class VehicleDbContext : DbContext, IDbContext
    {
        public virtual DbSet<ApplicationLog> Logs { get; set; }
        public virtual DbSet<RolePermission> RolePermissions { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }

        //public virtual DbSet<VehicleDomain> VehicleDomains { get; set; }
        //public virtual DbSet<Property> Properties { get; set; }
        //public virtual DbSet<PropertyType> PropertyTypes { get; set; }
        //public virtual DbSet<VehiclePosition> VehiclePositions { get; set; }

        public VehicleDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(VehicleDbContext).Assembly); // Here UseConfiguration is any IEntityTypeConfiguration

            //modelBuilder.SeedPermission();
            base.OnModelCreating(modelBuilder);
        }

        public async Task BeginAsync(CancellationToken cancellationToken = default)
        {
            await Database.BeginTransactionAsync(cancellationToken);
        }

        public void Commit()
        {
            Database.CommitTransaction();
        }

        public async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            await Database.BeginTransactionAsync(cancellationToken);
        }

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var result = await base.SaveChangesAsync(cancellationToken);
            return result;
        }

        private static DbContextOptions GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        }

        public void RemoveRange<T>(IEnumerable<T> entities) where T : class
        {
            base.RemoveRange(entities);
        }

        void IDbContext.Remove<T>(T entity)
        {
            base.Remove(entity);
        }

        void IDbContext.Update<T>(T entity)
        {
            base.Update(entity);
        }

        public new IQueryable<T> Query<T>() where T : class
        {
            return base.Set<T>().AsQueryable();
         
        }

        public async Task AddRangeAsync<T>(IEnumerable<T> items, CancellationToken cancellationToken = default) where T : class
        {
            await base.AddRangeAsync(items, cancellationToken);
        }

        public async Task AddAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class
        {
            await base.AddAsync(entity, cancellationToken);
        }

        public new async Task<TEntity> FindAsync<TEntity, TKey>(TKey id, CancellationToken cancellationToken = default) where TEntity : class
        {
            var res = await base.FindAsync<TEntity>(new object[] { id }, cancellationToken);
            return res;
        }
    }
}

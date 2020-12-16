using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using Vehicle.Infra.Persistance.EF;

namespace App.Service.AspDotNetDistributor
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<VehicleDbContext>
    {
        public VehicleDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<VehicleDbContext>();
            var connectionString = configuration.GetConnectionString("ConnectionString");
            builder.UseSqlServer(connectionString, x => x.UseNetTopologySuite());
            return new VehicleDbContext(builder.Options);
        }
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Vehicle.Infra.Persistance.EF
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, bool isTestEnvironment)
        {
    //        var provider =(Container) services.BuildServiceProvider();
    //        var reg = Lifestyle.Scoped.CreateRegistration(() =>
    //        {
    //            var optionsBuilder =
    //                new DbContextOptionsBuilder<VehicleDbContext>().UseSqlServer("FRIQuery");
    //            return new VehicleDbContext(optionsBuilder.Options);
    //        },
    //provider);


            //services.AddDbContext<VehicleDbContext>(options =>
            //    options.UseSqlServer(
            //        configuration.GetConnectionString("FRIQuery"),
            //        b => b.MigrationsAssembly(typeof(VehicleDbContext).Assembly.FullName)));

            //services.AddScoped<IVehicleDbContext>(provider => provider.GetService<VehicleDbContext>());

            //services.AddDefaultIdentity<ApplicationUser>()
            //    .AddEntityFrameworkStores<VehicleDbContext>();

            if (isTestEnvironment)
            {
                //services.AddIdentityServer()
                //    .AddApiAuthorization<ApplicationUser, VehicleDbContext>(options =>
                //    {
                //        options.Clients.Add(new Client
                //        {
                //            ClientId = "CleanArchitecture.IntegrationTests",
                //            AllowedGrantTypes = { GrantType.ResourceOwnerPassword },
                //            ClientSecrets = { new Secret("secret".Sha256()) },
                //            AllowedScopes = { "CleanArchitecture.WebUIAPI", "openid", "profile" }
                //        });
                //    }).AddTestUsers(new List<TestUser>
                //    {
                //        new TestUser
                //        {
                //            SubjectId = "f26da293-02fb-4c90-be75-e4aa51e0bb17",
                //            Username = "jason@clean-architecture",
                //            Password = "CleanArchitecture!",
                //            Claims = new List<Claim>
                //            {
                //                new Claim(JwtClaimTypes.Email, "jason@clean-architecture")
                //            }
                //        }
                //    });
            }
            else
            {
                //services.AddIdentityServer()
                //    .AddApiAuthorization<ApplicationUser, VehicleDbContext>();

                //services.AddTransient<IDateTime, DateTimeService>();
                //services.AddTransient<IIdentityService, IdentityService>();
                //services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();
            }

            //services.AddAuthentication()
            //    .AddIdentityServerJwt();

            return services;
        }
    }
}

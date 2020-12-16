using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;
using Framework.Application;
using Framework.Data;
using Microsoft.EntityFrameworkCore;
using Framework.Application.Common.Attributes;
using Microsoft.AspNetCore.Http;
using App.Service.AspDotNetDistributor.Filters;
using SimpleInjector.Lifestyles;
using System.Web.Mvc;
using SimpleInjector.Integration.Web.Mvc;
using Framework.Domain.Events;
using App.Service.Distributor.Common;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using App.Service.AspDotNetDistributor.swagger;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Vehicle.Domain;
using Vehicle.Infra.Persistance.EF;
using Vehicle.AppService.Query;
using Vehicle.AppService.Command_Handler.Subscriptions;
using Framework.Application.Config;
using AppService.Config;
using Vehicle.AppService.Contracts.Queries.Vehicles;
using App.Service.AspDotNetDistributor.Helpers;
using Vehicle.AppService.DataSeeders;

namespace App.Service.AspDotNetDistributor
{
    public class Startup
    {
        public static Container _container;
        private AppSettings _appSettings;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
            _appSettings = _configuration.Get<AppSettings>();
            _container = new SimpleInjector.Container();
            //_container.Options.PropertySelectionBehavior = new ImportPropertySelectionBehavior();
            //     _container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            // This is an extension method from the integration package.

            _container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            //_container.RegisterWebApiControllers(GlobalConfiguration.Configuration, Assembly.GetExecutingAssembly());

            // _container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            // _container.RegisterMvcAttributeFilterProvider();
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(_container));

            //GlobalConfiguration.Configuration.DependencyResolver =
            //    new SimpleInjectorWebApiDependencyResolver(_container);



                 

            // Set to false. This will be the default in v5.x and going forward.
            _container.Options.ResolveUnregisteredConcreteTypes = false;
        }

        private IConfiguration _configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            InitializeContainer();
            services.AddHostedService<StartupService>();
            services.AddHttpClient();
            services.AddSingleton<AppSettings>(x => _configuration.Get<AppSettings>());
            services.Configure<AppSettings>(_configuration);
            //.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerGenOptions>()
            //.AddScoped(sp => sp.GetRequiredService<IOptionsSnapshot<AppSettings>>().Value);
            services.AddScoped<Framework.Domain.Repository.IRepository<RolePermission>, Framework.Data.EF.EFRepository<RolePermission>>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddControllersWithViews().AddMvcOptions(options =>
            {
                options.Filters.Add(typeof(EncodeInputsActionFilter));
            });

            services.AddInfrastructure(_configuration, true);
            services.AddServiceQuery(_configuration, true);
            services.AddSimpleInjector(_container
         //, options =>
         //   {

         //       options.AddAspNetCore()

         //// Ensure activation of a specific framework type to be created by
         //// Simple Injector instead of the built-in configuration system.
         //// All calls are optional. You can enable what you need. For instance,
         //// ViewComponents, PageModels, and TagHelpers are not needed when you
         //// build a Web API.
         //.AddControllerActivation()
         //.AddViewComponentActivation()
         //.AddPageModelActivation()
         //.AddTagHelperActivation();

         //       // Optionally, allow application components to depend on the non-generic
         //       // ILogger (Microsoft.Extensions.Logging) or IStringLocalizer
         //       // (Microsoft.Extensions.Localization) abstractions.
         //       options.AddLogging();
         //       options.AddLocalization();
         //   }
         );


            services.
                AddMvc(o =>
                {
                    o.Conventions.Add(new GenericControllerRouteConvention());
                    o.EnableEndpointRouting = false;
                }
                    ).ConfigureApplicationPartManager(m => m.FeatureProviders.Add(new RemoteControllerFeatureProvider(_container)))
                //ConfigureApplicationPartManager(m => m.FeatureProviders.Add(new GenericTypeControllerFeatureProvider()));
                .AddNewtonsoftJson(o =>
                {
                    o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });


            services.AddInfrastructure(_configuration, true);
            services.AddServiceQuery(_configuration, true);


            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc(
                   name: "LibraryOpenAPISpecification",
                   info: new Microsoft.OpenApi.Models.OpenApiInfo()
                   {
                       Title = "Library API",
                       Version = "1",
                       Description = "Through this API you can access authors and their books.",
                       Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                       {
                           //Email = "name@site.com",
                           Name = "Daneshvar",
                           Url = new Uri("http://www.webestan.net")
                       },
                       License = new Microsoft.OpenApi.Models.OpenApiLicense()
                       {
                           Name = "Login",
                           Url = new Uri(_appSettings.ApplicationLaunchUrl + "/home")
                       }
                   });

                setupAction.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri(_appSettings.IdentityServer.Url + "/connect/authorize"),
                            TokenUrl = new Uri(_appSettings.IdentityServer.Url + "/connect/token"),
                            Scopes = new Dictionary<string, string>
                            {
                                {_appSettings.IdentityServer.Scope,"Demo API - full access"
                            }
                        }
                        }
                    }
                });

                setupAction.OperationFilter<AuthorizeCheckOperationFilter>();
                setupAction.SchemaFilter<SwaggerIgnoreFilter>(Array.Empty<object>());
                setupAction.ParameterFilter<SwaggerIgnoreParameterFilter>(Array.Empty<object>());
                setupAction.OperationFilter<SwaggerIgnoreIOperationFilterFilter>(Array.Empty<object>());
                setupAction.DocumentFilter<TagDescriptionsDocumentFilter>();
                setupAction.DescribeAllEnumsAsStrings();
                Enumerable.ToList<string>(Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly)).ForEach(delegate (string xmlFile)
                {
                    setupAction.IncludeXmlComments(xmlFile, false);
                });
            });
            services.AddSwaggerGenNewtonsoftSupport();

            

            // services.AddScoped<IRepository<User>, EFRepository<User>>();
            services.AddScoped<IDbContext, VehicleDbContext>(x => new VehicleDbContext(new DbContextOptionsBuilder<VehicleDbContext>().UseSqlServer(_appSettings.ConnectionStrings.ConnectionString, x => x.UseNetTopologySuite()).Options));
            services.AddDbContext<VehicleDbContext>(options => options.UseSqlServer(_appSettings.ConnectionStrings.ConnectionString, options => options.UseNetTopologySuite()));
            services.AddScoped<IEventDispatcher, EventDispatcher>();
            services.AddScoped<IEventHandlerFactory, EventHandlerFactory>();
            
            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    // required audience of access tokens
                    options.ApiName = "api1";

                    // auth server base endpoint (this will be used to search for disco doc)
                    options.Authority = _appSettings.IdentityServer.Url;//"https://localhost:5000";
                });


            //    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            //    {
            //        options.Authority = _appSettings.IdentityServer.Url;
            //        options.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidateAudience = false
            //        };
            //    })
            //    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
            //.AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
            //{
            //    options.Authority = _appSettings.IdentityServer.Url;

            //    options.ClientId = _appSettings.IdentityServer.ClientIdGrantTypeCode;
            //    options.ClientSecret = _appSettings.IdentityServer.ClientSecret;
            //    options.ResponseType = "code";

            //    options.Scope.Add(_appSettings.IdentityServer.Scope);

            //    options.SaveTokens = true;
            //});

            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("ApiScope", policy =>
            //    {
            //        policy.RequireAuthenticatedUser();
            //        //policy.RequireClaim("","");
            //    });
            //});


            //End Configure Quartz Services 

            //services.AddSingleton((serviceProvider) =>
            //{
            //    return new GetLog();
            //});

            services.AddSingleton<ICacheProvider, RedisCacheProvider>();

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });


            services.AddDistributedRedisCache(option =>
            {
                option.Configuration = "127.0.0.1";
                option.InstanceName = "master";
            });
        }

        private void InitializeContainer()
        {
            // Add application services. For instance:
            //   _container.Register<ICommandBus, CommandBus>(Lifestyle.Singleton);
            FrameworkConfigurator.WireUp(_container, false, typeof(VehicleAppService).Assembly, typeof(GetVehicleCurrentPositionQuery).Assembly);
            AppServiceConfigurator.WireUp(_container);
            _container.Register<ICurrentUserService, CurrentUserService>();
            _container.Register<IHttpContextAccessor, HttpContextAccessor>();
            _container.Register<IDataSeeder, DataSeeder>();


            //options.UseSqlServer(
            //       configuration.GetConnectionString("FRIQuery"),
            //       b => b.MigrationsAssembly(typeof(VehicleDbContext).Assembly.FullName)));

            //var reg = Lifestyle.Scoped.CreateRegistration(() =>
            //{
            //    return new VehicleDbContext(new DbContextOptionsBuilder<VehicleDbContext>().UseSqlServer(_configuration.GetConnectionString("FRIQuery")).Options);
            //}, _container);

            var reg = Lifestyle.Scoped.CreateRegistration(() =>
            {
                return new VehicleDbContext(new DbContextOptionsBuilder<VehicleDbContext>().UseSqlServer(_appSettings.ConnectionStrings.ConnectionString, x => x.UseNetTopologySuite()).Options);
            }, _container);

            _container.AddRegistration<IDbContext>(reg);
            //_container.Register<IDbContext>(() =>
            //{
            //    return new VehicleDbContext(new DbContextOptionsBuilder<VehicleDbContext>().UseSqlServer(_configuration.GetConnectionString("FRIQuery")).Options);
            //});
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {


            app.UseSimpleInjector(_container);


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCustomExceptionHandler();
            //app.UseSimpleInjector(_serviceProvider);

            _container.Verify();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/")
                {
                    context.Response.Redirect("/swagger");
                }
                else
                {
                    await next.Invoke();
                }
            });

            app.UseAuthentication();
            app.UseAuthorization();



            app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();


            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/LibraryOpenAPISpecification/swagger.json", "My API V1");
                c.OAuthClientId(_appSettings.IdentityServer.ClientIdGrantTypeCode);
                c.OAuthAppName("Demo API - Swagger");
                c.OAuthUsePkce();
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    //spa.UseAngularCliServer(npmScript: "start");
                    //spa.UseProxyToSpaDevelopmentServer("http://localhost:4202");
                }
            });

            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                var seed = _container.GetService<IDataSeeder>();
                seed.SeedAsync().Wait();
            }


        }
    }


}

//using System.Reflection;

//namespace Framework.Data.EF
//{
//    public class SessionFactoryBuilder
//    {
//        public static ISessionFactory Create(string connectionStringName, Assembly assembly)
//        {
//            var configure = Fluently.Configure()
//                .Database(
//                    MsSqlConfiguration
//                        .MsSql2008
//                /*.ShowSql()
//                .FormatSql()*/)
//                // .ConnectionString(new AppSettingsReader().GetValue("ConnectionStringName", typeof(string)).ToString()))
//                .Mappings(configuration =>
//                {
//                    configuration.FluentMappings.AddFromAssembly(typeof(EventMapper).Assembly);
//                    configuration.FluentMappings.AddFromAssembly(assembly);
//                    configuration.FluentMappings.Conventions.Setup(x => x.Add(AutoImport.Never()));
//                })
//                .BuildConfiguration();

//            configure.DataBaseIntegration(cfg =>
//            {

//                cfg.Dialect<MsSql2012Dialect>();
//                cfg.Driver<SqlClientDriver>();
//                cfg.ConnectionStringName = connectionStringName;
//                cfg.IsolationLevel = System.Data.IsolationLevel.ReadCommitted;
//            });
//            var modelMapper = new ModelMapper();
//            modelMapper.AddMappings(assembly.GetExportedTypes());

//            var hbmMapping = modelMapper.CompileMappingForAllExplicitlyAddedEntities();
//            configure.AddDeserializedMapping(hbmMapping, "test");

//            return configure.BuildSessionFactory();
//        }
//    }
//}

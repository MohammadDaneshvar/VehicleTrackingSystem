//using Orient.Client;
//using Orient.Client.API.Types;
//using OrientDB_Net.binary.Innov8tive.API;
//using System.Collections.Generic;

namespace Framework.Data.Orient
{
    public static class OrientDb
    {
   //     private static string _hostname = "localhost";
   //     private static string _databaseName = "person";
   //     private static ODatabaseType _type = ODatabaseType.Graph;
   //     private static int _port = 2424;
   //     private static string _user = "root";
   //     private static string _passwd = "root_passwd";

   //     public static OServer ConnectToServer() => new OServer(_hostname, _port, _user, _passwd);

   //     public static ConnectionOptions GetDatabaseConnectionOption() => new ConnectionOptions() { DatabaseName = _databaseName, DatabaseType = _type, HostName = _hostname, Port = _port, UserName = _user, Password = _passwd };
   //     public static ConnectionOptions GetDatabaseConnectionOption(string host, int port, string databaseName, string user, string passwd, ODatabaseType type = ODatabaseType.Graph) => new ConnectionOptions() { DatabaseName = databaseName, DatabaseType = type, HostName = host, Port = port, UserName = user, Password = passwd };
   //     public static ODatabase OpenDatabase(this ConnectionOptions connectionOptions) => new ODatabase(connectionOptions);
   //     public static OCommandResult RunCommand(this ODatabase oDatabase, string command) => oDatabase.Command(command);
   //     public static List<ODocument> RunQuery(this ODatabase oDatabase, string sql) => oDatabase.Query(sql);
   //     public static List<ODocument> RunSelectQuery(this ODatabase oDatabase, string className) => oDatabase.Select().From(className).ToList();//oDatabase.Query($"SELECT FROM {className}");
   //     public static List<ODocument> RunSelectParamsQuery(this ODatabase oDatabase, string className, params string[] projections) => oDatabase.Select(projections).From(className).ToList();
   //     //public static OCommandQuery RunSelectQuerySqlBatch(this ODatabase oDatabase, string name, string region) => oDatabase.SqlBatch($"begin let region = select from Region where name = {region} let employee = create vertex Sales set name = '{name}' let e = create edge Assignment from $employee to $accountcommit retry 100return $e");
   //     public static List<ODocument> RunSelectQueryByCondition(this ODatabase oDatabase, string className, Dictionary<string, string> conditions)
   //     {
   //         string condition = "";
   //         foreach (var item in conditions)
   //         {
   //             condition += $"{item.Key}={item.Value}";
   //         }
   //         return oDatabase.Query($"SELECT FROM {className} WHERE");
   //     }
   //     /// <summary>
   //     ///Equals<T>() Returns the record if the value of the field is equal to the given argument.
   //     ///NotEquals<T>() Returns the record if the value of the field is not equal to the given argument.
   //     ///Lesser<T>() Returns the record if the value of the field is less than the given argument.
   //     ///LesserEqual<T>() Returns the record if the value of the field is less than or equal to the given argument.
   //     ///Greater<T>() Returns the record if the value of the field is greater than the given argument.
   //     ///GreaterEqual<T>() Returns the record if the value of the field is greater than or equal to the given argument.
   //     ///Like<T>() Returns the record if the value of the field is similar to the given argument.
   //     ///In<T>() Returns the record if the value occurs in the given argument list.
   //     ///Lucene<T>() Returns true if the value occurs in the given argument, searched using the Lucene Engine.
   //     ///IsNull() Returns the record if the value of the field is NULL.
   //     ///Contains<T>() Returns the record if the value of the field contains the given argument.
   //     ///
   //     /// 
   //     /// 
   //     /// Between() This method takes two arguments, which are integers. It limits the return records to those that occur between them.
   //     ///Skip() This method takes one argument, which is an integer.It acts as offset, only returning the records that occur after this point.
   //     ///Limit() This mehtod takes one argument, which is an integer. It defines the maximum number of records to return.
   //     ///
   //     //// </summary>
   //     ///
   //     //// <param name="oDatabase"></param>
   //     //// <param name="className"></param>
   //     //// <returns></returns>
   //     public static List<ODocument> RunSelectQueryByCondition(this ODatabase oDatabase, string className, int take, int skip, string orderbydesc) =>
   //          oDatabase.Select().From(className)
   // .Where("user-status").Equals<string>("active")
   // .And("profile-picture").IsNull()
   //  .OrderBy(orderbydesc)
   //.Descending()
   //  .Skip(skip)
   //  .Limit(take)
   // .ToList();
   //     public static List<ODocument> RunQueryByFetchPlan(this ODatabase oDatabase, string sql, string fetchPlan) => oDatabase.Query(sql, fetchPlan);
   //     public static List<ODocument> RunSelectQueryByFetchPlan(this ODatabase oDatabase, string className, string fetchPlan) => oDatabase.Select().From(className).ToList(fetchPlan);
   //     public static void CloseDatabase(this ODatabase oDatabase) => oDatabase.Close();
   //     public static bool CheckDatabase(this OServer server, string database) => server.DatabaseExist(database, OStorageType.Memory);
   //     public static bool CreateDatabase(this OServer server, string databaseName, ODatabaseType type = ODatabaseType.Graph, OStorageType storage = OStorageType.Memory) => server.CreateDatabase(databaseName, type, storage);
   //     public static Dictionary<string, ODatabaseInfo> GetAllDatabases(this OServer server) => server.Databases();
   //     public static Dictionary<string, string> GetConfigList(this OServer server) => server.ConfigList();
   //     public static string GetConfigByKey(this OServer server, string key) => server.ConfigGet(key);
   //     public static bool SetConfig(this OServer server, string key, string value) => server.ConfigSet(key, value);
   //     public static bool DropDatabase(this OServer server, string databaseName, ODatabaseType type = ODatabaseType.Graph) => server.DropDatabase(databaseName, type);

   //     public static int UpdateDatabase(ODatabase database, string className, Dictionary<string, string> changes)
   //     {
   //         // INITIALIZE UPDATE
   //         OSqlUpdate update = database.Update().Class(className);

   //         // DEFINE CHANGES
   //         foreach (KeyValuePair<string, string> setting in changes)
   //         {
   //             // APPLY CHANGES
   //             update.Set(setting.Key, setting.Value);
   //             // update.Add(setting.Key, setting.Value);
   //             //update.Remove(setting.Key, setting.Value);
   //         }

   //         // RUN AND RETURN ODOCUMENT
   //         return update.Run();
   //     }

    }
}

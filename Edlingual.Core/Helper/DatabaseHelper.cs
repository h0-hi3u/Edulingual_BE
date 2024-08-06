using DbUp;
using Edulingual.Common.Constants;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace EduLingual.Common.Helper;

public static class DatabaseHelper
{
    private static string _connectionString;
    private static IConfiguration _config; 

    public static void InitConfiguration(IConfiguration config)
    {
        _config = config;
    }

    public static string GetConnectionString()
    {
        return _config.GetConnectionString(CoreConstants.DEFAULT_CONNECTION);
    }

    public static void ExecuteDbUpForSqlServer(string assemblyName)
    {
        var connection = _connectionString == null ? GetConnectionString() : _connectionString;

        EnsureDatabase.For.SqlDatabase(connection);
        var upgrader = DeployChanges.To.SqlDatabase(connection)
                        .WithScriptsEmbeddedInAssembly(Assembly.Load(assemblyName))
                        .LogToConsole()
                        .Build();

        var result = upgrader.GetScriptsToExecute();
        if(result.Any())
        {
            var success = upgrader.PerformUpgrade();
            if(!success.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(success.Error);
                Console.ResetColor();
            }
        } else
        {
            Console.WriteLine("No scripts found!");
        }
    }
    public static void ExecuteDbUpForPosgresql(string assemblyName)
    {
        var connection = _connectionString == null ? GetConnectionString() : _connectionString;

        EnsureDatabase.For.PostgresqlDatabase(connection);
        var upgrader = DeployChanges.To.PostgresqlDatabase(connection)
                        .WithScriptsEmbeddedInAssembly(Assembly.Load(assemblyName))
                        .LogToConsole()
                        .Build();

        var result = upgrader.GetScriptsToExecute();
        if (result.Any())
        {
            var success = upgrader.PerformUpgrade();
            if (!success.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(success.Error);
                Console.ResetColor();
            }
        }
        else
        {
            Console.WriteLine("No scripts found!");
        }
    }
}

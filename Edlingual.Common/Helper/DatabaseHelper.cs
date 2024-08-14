using DbUp;
using Edulingual.Common.Constants;
using Edulingual.Common.Exceptions;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace EduLingual.Common.Helper;

public static class DatabaseHelper
{
    private static IConfiguration _config; 

    public static void InitConfiguration(IConfiguration config)
    {
        _config = config;
    }

    public static string GetConnectionString()
        => _config.GetConnectionString(DatabaseConstants.DEFAULT_CONNECTION) ?? throw new MissingConnectionStringException();

    public static void ExecuteDbUpForSqlServer(string assemblyName, string? connection, string database)
    {
        if(database == DatabaseConstants.POSTGRESQL_NAME)
        {
            EnsureDatabase.For.PostgresqlDatabase(connection);
        } else
        {
            EnsureDatabase.For.SqlDatabase(connection);
        }
        connection ??= GetConnectionString();

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
}

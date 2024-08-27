using DbUp;
using DbUp.Engine;
using Edulingual.Common.Constants;
using Edulingual.Common.Exceptions;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Edulingual.Common.Helper;

public static class DatabaseHelper
{
    private static IConfiguration _config; 

    public static void InitConfiguration(IConfiguration config)
    {
        _config = config;
    }

    public static string GetConnectionString()
        => _config.GetConnectionString(DatabaseConstants.DEFAULT_CONNECTION) ?? throw new MissingConnectionStringException();

    public static void ExecuteDbUp(string assemblyName, string database = DatabaseConstants.SQL_SERVER_NAME, string? connection = null)
    {
        connection ??= GetConnectionString();
        UpgradeEngine upgrader = default!;
        if (database == DatabaseConstants.POSTGRESQL_NAME)
        {
            EnsureDatabase.For.PostgresqlDatabase(connection);
            upgrader = DeployChanges.To.PostgresqlDatabase(connection)
                        .WithScriptsEmbeddedInAssembly(Assembly.Load(assemblyName))
                        .LogToConsole()
                        .Build();
        } else if (database == DatabaseConstants.SQL_SERVER_NAME)
        {
            EnsureDatabase.For.SqlDatabase(connection);
            upgrader = DeployChanges.To.SqlDatabase(connection)
                            .WithScriptsEmbeddedInAssembly(Assembly.Load(assemblyName))
                            .LogToConsole()
                            .Build();
        }

        var result = upgrader!.GetScriptsToExecute();
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

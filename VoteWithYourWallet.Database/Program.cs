﻿using DbUp;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace VoteWithYourWallet.Database;

public class Program
{
    public static int Main(string[]? args)
    {
        // Attempt to read the connection string from the settings.
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddEnvironmentVariables()
            .AddJsonFile("appsettings.Development.json", true);

        IConfiguration configuration = builder.Build();

        var connectionString = ((args?.FirstOrDefault() != null) ?  
            args.FirstOrDefault()
            : configuration.GetConnectionString("PrimaryContext"));

        EnsureDatabase.For.SqlDatabase(connectionString);

        var upgradeEngine =
            DeployChanges.To
                .SqlDatabase(connectionString)
                .WithScriptsEmbeddedInAssembly(Assembly.GetAssembly(typeof(Program)))
                .WithTransactionPerScript()
                .LogToConsole()
                .Build();

        var result = upgradeEngine.PerformUpgrade();

        if (!result.Successful)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(result.Error);
            Console.ResetColor();

            return -1;
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Success!");
        Console.ResetColor();

        return 0;
    }
}

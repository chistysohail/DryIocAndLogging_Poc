using DryIoc;
using System;

// Define the ILogger interface
public interface ILogger
{
    void Log(string message);
}

// Implement the ILogger interface with ConsoleLogger
public class ConsoleLogger : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine($"ConsoleLogger: {message}");
    }
}

// Define the DatabaseManager class with a dependency on ILogger
public class DatabaseManager
{
    private readonly ILogger _logger;

    public DatabaseManager(ILogger logger)
    {
        _logger = logger;
    }

    public void Query(string sql)
    {
        // Simulate querying and logging
        _logger.Log($"Querying database with SQL: {sql}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create and configure the DryIoc container
        var container = new Container();

        // Register the ConsoleLogger as the implementation for ILogger
        container.Register<ILogger, ConsoleLogger>(Reuse.Singleton);

        // Register the DatabaseManager with transient scope
        container.Register<DatabaseManager>();

        // Resolve an instance of DatabaseManager
        var dbManager = container.Resolve<DatabaseManager>();

        // Use the DatabaseManager instance to run a query
        dbManager.Query("SELECT * FROM Users");
    }
}

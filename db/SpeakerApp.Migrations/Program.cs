using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Data.SqlClient;

var connectionString = "Server=localhost;Database=SpeakerApp;User Id=SA;Password=SuperSecret123!;TrustServerCertificate=true;";

EnsureDatabaseExists(connectionString, dropExisting: true);

// Configure the dependency injection container
var serviceProvider = CreateServices(connectionString);

// Run the migrations
using (var scope = serviceProvider.CreateScope())
{
    UpdateDatabase(scope.ServiceProvider);
}

// Method to configure the dependency injection container
static IServiceProvider CreateServices(string connectionString)
{
    return new ServiceCollection()
        .AddFluentMigratorCore()
        .ConfigureRunner(rb => rb
            .AddSqlServer()
            .WithGlobalConnectionString(connectionString)
            .ScanIn(typeof(Program).Assembly).For.Migrations())
        .AddLogging(lb => lb.AddFluentMigratorConsole())
        .BuildServiceProvider(false);
}

// Method to update the database with migrations
static void UpdateDatabase(IServiceProvider serviceProvider)
{
    var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
    runner.MigrateUp();
}


// ...

static void EnsureDatabaseExists(string connectionString, bool dropExisting = false)
{
    var builder = new SqlConnectionStringBuilder(connectionString);
    var databaseName = builder.InitialCatalog;

    // Remove the database name from the connection string
    builder.Remove("Initial Catalog");

    using (var connection = new SqlConnection(builder.ConnectionString))
    {
        connection.Open();

        // Check if the database exists
        var commandText = $"SELECT COUNT(*) FROM sys.databases WHERE name = '{databaseName}'";

        using (var command = new SqlCommand(commandText, connection))
        {
            var result = (int)command.ExecuteScalar();

            if (result == 0)
            {
                CreateDatabase(databaseName, connection);
            }
            else if (dropExisting)
            {
                // Drop the database
                commandText = $"DROP DATABASE {databaseName}";
                using (var dropCommand = new SqlCommand(commandText, connection))
                {
                    dropCommand.ExecuteNonQuery();
                }

                Console.WriteLine($"Database '{databaseName}' dropped successfully.");

                // Recreate the database
                CreateDatabase(databaseName, connection);
            }
            else
            {
                Console.WriteLine($"Database '{databaseName}' already exists.");
            }
        }
    }
}

static void CreateDatabase(string databaseName, SqlConnection connection)
{
    // Create the database
    var commandText = $"CREATE DATABASE {databaseName}";

    using (var createCommand = new SqlCommand(commandText, connection))
    {
        createCommand.ExecuteNonQuery();
    }

    Console.WriteLine($"Database '{databaseName}' created successfully.");
}
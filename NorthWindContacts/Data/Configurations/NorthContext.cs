using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

// ReSharper disable once CheckNamespace
namespace NorthWindContacts.Data
{
    /// <summary>
    /// DbContext connections
    /// </summary>
    public partial class NorthContext
    {
        /// <summary>
        /// Simple configuration for setting the connection string
        /// </summary>
        /// <param name="optionsBuilder"></param>
        private static void NoLogging(DbContextOptionsBuilder optionsBuilder)
        {
            var config = ReadAppsettings(out _);
            optionsBuilder.UseSqlServer(config.GetConnectionString("DatabaseConnection"));
        }

        /// <summary>
        /// Default logging to output window
        /// </summary>
        /// <param name="optionsBuilder"></param>
        private static void StandardLogging(DbContextOptionsBuilder optionsBuilder)
        {
            var config = ReadAppsettings(out _);
            optionsBuilder.UseSqlServer(config.GetConnectionString("DatabaseConnection"))
                .EnableSensitiveDataLogging()
                .LogTo(message => Debug.WriteLine(message));
        }

        /// <summary>
        /// Writes/appends to a file
        /// </summary>
        /// <param name="optionsBuilder"></param>
        private static void CustomLogging(DbContextOptionsBuilder optionsBuilder)
        {

            var config = ReadAppsettings(out _);
            optionsBuilder.UseSqlServer(config.GetConnectionString("DatabaseConnection")).EnableSensitiveDataLogging()
                .LogTo(new DbContextLogger().Log)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }
        /// <summary>
        /// Slimmed down to specific details
        /// </summary>
        /// <param name="optionsBuilder"></param>
        private static void DatabaseCategoryLogging(DbContextOptionsBuilder optionsBuilder)
        {
            var config = ReadAppsettings(out _);

            optionsBuilder.UseSqlServer(config.GetConnectionString("DatabaseConnection")).EnableSensitiveDataLogging()
                .LogTo(message => Debug.WriteLine(message),
                    new[] { DbLoggerCategory.Database.Command.Name },
                    LogLevel.Information,
                    DbContextLoggerOptions.SingleLine | DbContextLoggerOptions.UtcTime);
        }

        private static IConfigurationRoot ReadAppsettings(out IConfigurationBuilder builder)
        {
            builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            IConfigurationRoot config = builder.Build();

            return config; // connection string
        }
    }
}

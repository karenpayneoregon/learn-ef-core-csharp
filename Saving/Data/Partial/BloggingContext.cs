using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Saving.Helpers;

namespace Saving.Data.NotUsedCurrently 
{
    public  class BloggingContext
    {

        private static readonly string _connectionString =
            "Server=(localdb)\\mssqllocaldb;Database=EFSaving.RelatedData;Trusted_Connection=True";

        /// <summary>
        /// Simple configuration for setting the connection string
        /// </summary>
        /// <param name="optionsBuilder"></param>
        private static void NoLogging(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        /// <summary>
        /// Default logging to output window
        /// </summary>
        /// <param name="optionsBuilder"></param>
        private static void StandardLogging(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString)
                .EnableSensitiveDataLogging()
                .LogTo(message => Debug.WriteLine(message));
        }

        /// <summary>
        /// Writes/appends to a file
        /// </summary>
        /// <param name="optionsBuilder"></param>
        private static void CustomLogging(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString).EnableSensitiveDataLogging()
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
            optionsBuilder.UseSqlServer(_connectionString).EnableSensitiveDataLogging()
                .LogTo(message => Debug.WriteLine(message),
                    new[] { DbLoggerCategory.Database.Command.Name },
                    LogLevel.Information,
                    DbContextLoggerOptions.SingleLine | DbContextLoggerOptions.UtcTime);
        }
    }
}

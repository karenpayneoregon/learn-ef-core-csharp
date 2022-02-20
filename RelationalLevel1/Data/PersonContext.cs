using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using RelationalLevel1.Models;

namespace RelationalLevel1.Data
{
    public class PersonContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        /// <summary>
        /// Belongs in appsettings.json
        /// </summary>
        private static string _connectionString = 
            "Server=(localdb)\\mssqllocaldb;Database=EF.People;Trusted_Connection=True";

    }
}

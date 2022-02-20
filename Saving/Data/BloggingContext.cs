using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Saving.Data.Logging;
using Saving.Helpers;
using Saving.Models;

namespace Saving.Data
{
    public partial class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            ContextLogging.NoLogging(optionsBuilder);
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Webtask.Models;
using System.Diagnostics;

namespace Webtask.Data
{
    public class ApplicationDbContext:DbContext

    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options) : base(options)
        {

        }

        public DbSet<Home> ListTable { get; set; }
    }
}

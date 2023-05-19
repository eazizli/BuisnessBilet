using eBusiness.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace eBusiness.DataContext
{
    public class EbuisnesDbContex:DbContext
    {
        public EbuisnesDbContex(DbContextOptions<EbuisnesDbContex> options):base(options)
        {

        }
        public DbSet<Team> Teams { get; set; }
    }
}

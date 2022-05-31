using AP203Juan.Models;
using Microsoft.EntityFrameworkCore;

namespace AP203Juan.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {

        }
        public DbSet<Slider> Sliders { get; set; }
    }
}

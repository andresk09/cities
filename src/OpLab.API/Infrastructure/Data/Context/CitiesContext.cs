using Microsoft.EntityFrameworkCore;
using OpLab.API.Infrastructure.Data.Models;

namespace OpLab.API.Infrastructure.Data.Context
{
    public class CitiesContext : DbContext
    {
        public CitiesContext(DbContextOptions<CitiesContext> options) : base(options)
        {
        }

        public DbSet<City> Cities { get; set; }
    }
}

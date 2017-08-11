using Microsoft.EntityFrameworkCore;
using OpLab.API.Infrastructure.Data.Models;

namespace OpLab.API.Infrastructure.Data.Context
{
    public class PointsOfInterestContext : DbContext
    {
        public PointsOfInterestContext(DbContextOptions<PointsOfInterestContext> options) : base(options)
        {
        }
        public DbSet<PointOfInterest> PointsOfInterest { get; set; }
    }
}

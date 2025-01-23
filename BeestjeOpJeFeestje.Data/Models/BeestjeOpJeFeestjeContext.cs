using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BeestjeOpJeFeestje.Data.Models
{
    public class BeestjeOpJeFeestjeContext : IdentityDbContext
    {
        public DbSet<Animal> Animals { get; set; } = null!;
        public DbSet<Booking> Bookings { get; set; } = null!;

        public BeestjeOpJeFeestjeContext(DbContextOptions<BeestjeOpJeFeestjeContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

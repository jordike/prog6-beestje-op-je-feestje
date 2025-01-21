using Microsoft.EntityFrameworkCore;

namespace BeestjeOpJeFeestje.Models
{
    public class BeestjeOpJeFeestjeContext : DbContext
    {
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        public BeestjeOpJeFeestjeContext()
        {
        }   

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

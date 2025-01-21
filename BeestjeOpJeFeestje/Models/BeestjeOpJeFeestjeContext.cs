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

            modelBuilder.Entity<Animal>().HasData(
            // Jungle Animals
                new Animal { Id = 1, Name = "Aap", Type = "Jungle", Price = 50.0f, ImageURL = "https://example.com/aap.jpg" },
                new Animal { Id = 2, Name = "Olifant", Type = "Jungle", Price = 200.0f, ImageURL = "https://example.com/olifant.jpg" },
                new Animal { Id = 3, Name = "Zebra", Type = "Jungle", Price = 150.0f, ImageURL = "https://example.com/zebra.jpg" },
                new Animal { Id = 4, Name = "Leeuw", Type = "Jungle", Price = 300.0f, ImageURL = "https://example.com/leeuw.jpg" },

                // Farm Animals
                new Animal { Id = 5, Name = "Hond", Type = "Boerderij", Price = 30.0f, ImageURL = "https://example.com/hond.jpg" },
                new Animal { Id = 6, Name = "Ezel", Type = "Boerderij", Price = 60.0f, ImageURL = "https://example.com/ezel.jpg" },
                new Animal { Id = 7, Name = "Koe", Type = "Boerderij", Price = 120.0f, ImageURL = "https://example.com/koe.jpg" },
                new Animal { Id = 8, Name = "Eend", Type = "Boerderij", Price = 20.0f, ImageURL = "https://example.com/eend.jpg" },
                new Animal { Id = 9, Name = "Kuiken", Type = "Boerderij", Price = 10.0f, ImageURL = "https://example.com/kuiken.jpg" },

                // Snow Animals
                new Animal { Id = 10, Name = "Pinguïn", Type = "Sneeuw", Price = 80.0f, ImageURL = "https://example.com/pinguin.jpg" },
                new Animal { Id = 11, Name = "IJsbeer", Type = "Sneeuw", Price = 250.0f, ImageURL = "https://example.com/ijsbeer.jpg" },
                new Animal { Id = 12, Name = "Zeehond", Type = "Sneeuw", Price = 100.0f, ImageURL = "https://example.com/zeehond.jpg" },

                // Desert Animals
                new Animal { Id = 13, Name = "Kameel", Type = "Woestijn", Price = 180.0f, ImageURL = "https://example.com/kameel.jpg" },
                new Animal { Id = 14, Name = "Slang", Type = "Woestijn", Price = 70.0f, ImageURL = "https://example.com/slang.jpg" },

                // VIP Animals
                new Animal { Id = 15, Name = "T-Rex", Type = "VIP", Price = 1000.0f, ImageURL = "https://example.com/t-rex.jpg" },
                new Animal { Id = 16, Name = "Unicorn", Type = "VIP", Price = 1200.0f, ImageURL = "https://example.com/unicorn.jpg" });
        }
    }
}

namespace CatHotel.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cat> Cats { get; set; }

        public DbSet<Breed> Breeds { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<CatReservation> CatsReservations { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<RoomType> RoomTypes { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public DbSet<Grooming> Groomings { get; set; }

        public DbSet<CatGrooming> CatsGroomings { get; set; }

        public DbSet<Style> Styles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer("Server=.;Database=CatHotel;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<CatReservation>()
                .HasKey(k => new {k.CatId, k.ReservationId});

            builder.Entity<CatGrooming>()
                .HasKey(k => new {k.CatId, k.GroomingId});
        }
    }
}
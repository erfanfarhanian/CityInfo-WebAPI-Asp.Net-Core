using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.DBContexts
{
    public class CityInfoDBContext : DbContext
    {
        public CityInfoDBContext(DbContextOptions<CityInfoDBContext> options) : base(options)
        {

        }

        // (null!) Null forgiving which means that the null issue has been controlled.
        // You do not need to be worried about that!
        public DbSet<City> Cities { get; set; } = null!;
        public DbSet<PointOfInterest> PointsOfInterest { get; set; } = null!;

        // One way to add database to the project. Another way is to use it in program.cs
        // which is better.
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite();
        //    base.OnConfiguring(optionsBuilder);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                .HasData(
                new City("Tehran")
                {
                    ID = 1,
                    Description = "This is Tehran."
                },
                new City("Shiraz")
                {
                    ID = 2,
                    Description = "This is Shiraz."
                },
                new City("Tabriz")
                {
                    ID = 3,
                    Description = "This is Tabriz."
                }
                );

            modelBuilder.Entity<PointOfInterest>()
                .HasData(
                new PointOfInterest("First Point")
                {
                    ID = 1,
                    CityID = 1,
                    Description = "This is first point of interest for Tehran"
                },
                new PointOfInterest("Second Point")
                {
                    ID = 2,
                    CityID = 1,
                    Description = "This is second point of interest for Tehran"
                },
                new PointOfInterest("Third Point")
                {
                    ID = 3,
                    CityID = 1,
                    Description = "This is third point of interest for Tehran"
                }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}

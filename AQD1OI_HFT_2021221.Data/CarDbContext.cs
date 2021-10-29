using AQD1OI_HFT_2021221.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AQD1OI_HFT_2021221.Data
{
    //Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Db.mdf;Integrated Security=True
    public partial class CarDbContext : DbContext
    {
        public CarDbContext(DbContextOptions<CarDbContext> options) : base(options)
        {
        }

        public CarDbContext()
        {
            this.Database.EnsureCreated();
        }

        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Rental> Rentals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseLazyLoadingProxies()
                    .UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Db.mdf;Integrated Security=True;MultipleActiveResultSets=True");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Car>()
                .HasOne(c => c.Brand)
                .WithMany(b => b.Cars)
                .HasForeignKey(c => c.BrandID)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Rental>()
                .HasOne(r => r.Car)
                .WithMany(c => c.Rentals)
                .HasForeignKey(r => r.CarID)
                .OnDelete(DeleteBehavior.ClientSetNull);

            DbSeeding(modelBuilder);

        }

        private void DbSeeding(ModelBuilder modelBuilder)
        {
            Brand alfaromeo = new Brand() { ID = 1, Name = "Alfa Romeo" };
            Brand audi = new Brand() { ID = 2, Name = "Audi" };
            Brand bmw = new Brand() { ID = 3, Name = "BMW" };
            Brand ford = new Brand() { ID = 4, Name = "Ford" };

            Car alfaromeo1 = new Car() { ID = 1, BrandID = alfaromeo.ID, Model = "Alfa Romeo 147", Price = 2000 };
            Car bmw1 = new Car() { ID = 2, BrandID = bmw.ID, Price = 20000, Model = "BMW 116d" };
            Car bmw2 = new Car() { ID = 3, BrandID = bmw.ID, Price = 30000, Model = "BMW 510" };
            Car audi1 = new Car() { ID = 4, BrandID = audi.ID, Price = 40000, Model = "Audi A4" };
            Car audi2 = new Car() { ID = 5, BrandID = audi.ID, Price = 60000, Model = "Audi A5" };
            Car audi3 = new Car() { ID = 6, BrandID = audi.ID, Price = 80000, Model = "Audi A7" };
            Car ford1 = new Car() { ID = 7, BrandID = ford.ID, Price = 50000, Model = "Ford Mustang" };
            Car ford2 = new Car() { ID = 8, BrandID = ford.ID, Price = 90000, Model = "Ford Mustang Mach-E" };
            Car ford3 = new Car() { ID = 9, BrandID = ford.ID, Price = 30000, Model = "Ford Maverick" };
            Car ford4 = new Car() { ID = 10, BrandID = ford.ID, Price = 40000, Model = "Ford Ranger" };

            Rental rental1 = new Rental() { ID = 1, CarID = alfaromeo1.ID, Renter = "Noelle Bowman", Date = new DateTime(2021, 04, 01)};
            Rental rental2 = new Rental() { ID = 2, CarID = audi1.ID, Renter = "Isreal Burch", Date = new DateTime(2021, 06, 01) };
            Rental rental3 = new Rental() { ID = 3, CarID = audi1.ID, Renter = "Glen Travis", Date = new DateTime(2021, 07, 10) };
            Rental rental4 = new Rental() { ID = 4, CarID = bmw2.ID, Renter = "Rose Hill", Date = new DateTime(2021, 04, 05) };
            Rental rental5 = new Rental() { ID = 5, CarID = ford2.ID, Renter = "Lawrence Butcher", Date = new DateTime(2021, 02, 01) };
            Rental rental6 = new Rental() { ID = 6, CarID = audi2.ID, Renter = "Antony Santiago", Date = new DateTime(2021, 04, 03) };
            Rental rental7 = new Rental() { ID = 7, CarID = audi3.ID, Renter = "Graig Richards", Date = new DateTime(2021, 06, 23) };
            Rental rental8 = new Rental() { ID = 8, CarID = ford1.ID, Renter = "Billie Patrick", Date = new DateTime(2021, 08, 10) };
            Rental rental9 = new Rental() { ID = 9, CarID = bmw2.ID, Renter = "Pauline Vaughan", Date = new DateTime(2021, 07, 25) };
            Rental rental10 = new Rental() { ID = 10, CarID = audi1.ID, Renter = "Johnathan Kelly", Date = new DateTime(2021, 10, 15) };

            modelBuilder.Entity<Brand>().HasData(alfaromeo, audi, bmw, ford);
            modelBuilder.Entity<Car>().HasData(alfaromeo1, audi1, audi2, audi3, bmw1, bmw2, ford1, ford2, ford3, ford4);
            modelBuilder.Entity<Rental>().HasData(rental1, rental2, rental3, rental4, rental5, rental6, rental7, rental8, rental9, rental10);
        }





    }
}

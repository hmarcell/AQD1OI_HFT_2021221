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
    public partial class BikeDbContext : DbContext
    {
        public BikeDbContext(DbContextOptions<BikeDbContext> options) : base(options)
        {
        }

        public BikeDbContext()
        {
            this.Database.EnsureCreated();
        }

        public virtual DbSet<Bike> Bikes { get; set; }
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

            modelBuilder.Entity<Bike>()
                .HasOne(b => b.Brand)
                .WithMany(b => b.Bikes)
                .HasForeignKey(b => b.BrandID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Rental>()
                .HasOne(r => r.Bike)
                .WithMany(b => b.Rentals)
                .HasForeignKey(r => r.BikeID)
                .OnDelete(DeleteBehavior.Cascade);

            DbSeeding(modelBuilder);

        }

        private void DbSeeding(ModelBuilder modelBuilder)              
        {
            Brand csepel = new Brand() { ID = 1, Name = "Csepel" };
            Brand merida = new Brand() { ID = 2, Name = "Merida" };
            Brand sprint = new Brand() { ID = 3, Name = "Sprint" };
            Brand cross = new Brand() { ID = 4, Name = "Cross" };

            Bike merida1 = new Bike() { ID = 1, BrandID = merida.ID, Price = 35000, Model = "Merida Dual Thrust" };
            Bike sprint1 = new Bike() { ID = 2, BrandID = sprint.ID, Price = 4900, Model = "Sprint Jaguar" };
            Bike sprint2 = new Bike() { ID = 3, BrandID = sprint.ID, Price = 10000, Model = "Sprint Sirius Tempo" };
            Bike csepel1 = new Bike() { ID = 4, BrandID = csepel.ID, Price = 6900, Model = "Csepel Budapest A" };
            Bike csepel2 = new Bike() { ID = 5, BrandID = csepel.ID, Price = 9000, Model = "Csepel Landrider 28 (2020)" };
            Bike csepel3 = new Bike() { ID = 6, BrandID = csepel.ID, Price = 8000, Model = "Csepel Budapest B" };
            Bike cross1 = new Bike() { ID = 7, BrandID = cross.ID, Price = 23500, Model = "Cross Picnic" };
            Bike cross2 = new Bike() { ID = 8, BrandID = cross.ID, Price = 24500, Model = "Cross Citerra" };
            Bike cross3 = new Bike() { ID = 9, BrandID = cross.ID, Price = 27500, Model = "Cross Riviera" };
            Bike cross4 = new Bike() { ID = 10, BrandID = cross.ID, Price = 25000, Model = "Cross Avalon" };

            Rental rental1 = new Rental() { ID = 1, BikeID = cross2.ID, Renter = "Noelle Bowman", Date = new DateTime(2021, 04, 01)};
            Rental rental2 = new Rental() { ID = 2, BikeID = csepel1.ID, Renter = "Isreal Burch", Date = new DateTime(2021, 06, 01) };
            Rental rental3 = new Rental() { ID = 3, BikeID = csepel1.ID, Renter = "Glen Travis", Date = new DateTime(2021, 07, 10) };
            Rental rental4 = new Rental() { ID = 4, BikeID = sprint2.ID, Renter = "Rose Hill", Date = new DateTime(2021, 04, 05) };
            Rental rental5 = new Rental() { ID = 5, BikeID = merida1.ID, Renter = "Lawrence Butcher", Date = new DateTime(2021, 02, 01) };
            Rental rental6 = new Rental() { ID = 6, BikeID = csepel2.ID, Renter = "Antony Santiago", Date = new DateTime(2021, 04, 03) };
            Rental rental7 = new Rental() { ID = 7, BikeID = csepel3.ID, Renter = "Graig Richards", Date = new DateTime(2021, 06, 23) };
            Rental rental8 = new Rental() { ID = 8, BikeID = cross1.ID, Renter = "Billie Patrick", Date = new DateTime(2021, 08, 10) };
            Rental rental9 = new Rental() { ID = 9, BikeID = sprint2.ID, Renter = "Pauline Vaughan", Date = new DateTime(2021, 07, 25) };
            Rental rental10 = new Rental() { ID = 10, BikeID = csepel1.ID, Renter = "Johnathan Kelly", Date = new DateTime(2021, 10, 15) };

            modelBuilder.Entity<Brand>().HasData(csepel, merida, sprint, cross);
            modelBuilder.Entity<Bike>().HasData(merida1, csepel1, csepel2, csepel3, sprint1, sprint2, cross1, cross2, cross3, cross4);
            modelBuilder.Entity<Rental>().HasData(rental1, rental2, rental3, rental4, rental5, rental6, rental7, rental8, rental9, rental10);
        }





    }
}

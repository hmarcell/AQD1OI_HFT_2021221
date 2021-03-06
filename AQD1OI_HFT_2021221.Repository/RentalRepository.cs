using AQD1OI_HFT_2021221.Data;
using AQD1OI_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AQD1OI_HFT_2021221.Repository
{
    public class RentalRepository : IRentalRepository
    {
        BikeDbContext DbContext;
        public RentalRepository(BikeDbContext DbContext)
        {
            this.DbContext = DbContext;
        }
        public void Create(Rental rental)
        {
            DbContext.Rentals.Add(rental);
            DbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            DbContext.Remove(Read(id));
            DbContext.SaveChanges();
        }

        public Rental Read(int id)
        {
            return DbContext.Rentals.FirstOrDefault(r => r.ID == id);
        }

        public IQueryable<Rental> ReadAll()
        {
            return DbContext.Rentals;
        }

        public void Update(Rental rental)
        {
            var old = Read(rental.ID);
            old.Date = rental.Date;
            old.Renter = rental.Renter;
            old.BikeID = rental.BikeID;
            DbContext.SaveChanges();
        }
    }
}

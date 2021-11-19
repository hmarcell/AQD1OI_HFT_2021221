using AQD1OI_HFT_2021221.Data;
using AQD1OI_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AQD1OI_HFT_2021221.Repository
{
    public class BikeRepository : IBikeRepository
    {
        BikeDbContext DbContext;
        public BikeRepository(BikeDbContext DbContext)
        {
            this.DbContext = DbContext;
        }
        public void Create(Bike bike)
        {
            DbContext.Bikes.Add(bike);
            DbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            DbContext.Remove(Read(id));
            DbContext.SaveChanges();
        }

        public Bike Read(int id)
        {
            return DbContext.Bikes.FirstOrDefault(c => c.ID == id);
        }

        public IQueryable<Bike> ReadAll()
        {
            return DbContext.Bikes;
        }

        public void Update(Bike bike)
        {
            var oldBike = Read(bike.ID);
            oldBike.BrandID = bike.BrandID;
            oldBike.Model = bike.Model;
            oldBike.Price = bike.Price;
            DbContext.SaveChanges();
        }
    }
}

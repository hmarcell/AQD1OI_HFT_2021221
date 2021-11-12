using AQD1OI_HFT_2021221.Data;
using AQD1OI_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AQD1OI_HFT_2021221.Repository
{
    public class CarRepository : ICarRepository
    {
        CarDbContext DbContext;
        public CarRepository(CarDbContext DbContext)
        {
            this.DbContext = DbContext;
        }
        public void Create(Car car)
        {
            DbContext.Cars.Add(car);
            DbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            DbContext.Remove(Read(id));
            DbContext.SaveChanges();
        }

        public Car Read(int id)
        {
            return DbContext.Cars.FirstOrDefault(c => c.ID == id);
        }

        public IQueryable<Car> ReadAll()
        {
            return DbContext.Cars;
        }

        public void Update(Car car)
        {
            var oldCar = Read(car.ID);
            oldCar.BrandID = car.BrandID;
            oldCar.Model = car.Model;
            oldCar.Price = car.Price;
            DbContext.SaveChanges();
        }
    }
}

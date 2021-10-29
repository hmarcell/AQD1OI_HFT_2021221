using AQD1OI_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AQD1OI_HFT_2021221.Repository
{
    public interface ICarRepository
    {
        public void Create(Car car);
        public Car Read(int id);
        public void Update(Car car);
        public void Delete(int id);
        public IQueryable<Car> ReadAll();

    }
}

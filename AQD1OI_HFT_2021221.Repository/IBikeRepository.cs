using AQD1OI_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AQD1OI_HFT_2021221.Repository
{
    public interface IBikeRepository
    {
        public void Create(Bike bike);
        public Bike Read(int id);
        public void Update(Bike bike);
        public void Delete(int id);
        public IQueryable<Bike> ReadAll();

    }
}

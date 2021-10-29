using AQD1OI_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AQD1OI_HFT_2021221.Repository
{
    public interface IRentalRepository
    {
        public void Create(Rental rental);
        public Rental Read(int id);
        public void Update(Rental rental);
        public void Delete(int id);
        public IQueryable<Rental> ReadAll();
    }
}

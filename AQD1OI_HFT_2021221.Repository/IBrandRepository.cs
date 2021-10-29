using AQD1OI_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AQD1OI_HFT_2021221.Repository
{
    public interface IBrandRepository
    {
        public void Create(Brand brand);
        public Brand Read(int id);
        public void Update(Brand brand);
        public void Delete(int id);
        public IQueryable<Brand> ReadAll();
    }
}

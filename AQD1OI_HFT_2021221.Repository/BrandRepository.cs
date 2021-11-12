using AQD1OI_HFT_2021221.Data;
using AQD1OI_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AQD1OI_HFT_2021221.Repository
{
    public class BrandRepository : IBrandRepository
    {
        CarDbContext DbContext;
        public BrandRepository(CarDbContext DbContext)
        {
            this.DbContext = DbContext;
        }
        public void Create(Brand brand)
        {
            DbContext.Brands.Add(brand);
            DbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            DbContext.Remove(Read(id));
            DbContext.SaveChanges();
        }

        public Brand Read(int id)
        {
            return DbContext.Brands.FirstOrDefault(brand => brand.ID == id);
        }

        public IQueryable<Brand> ReadAll()
        {
            return DbContext.Brands;
        }

        public void Update(Brand brand)                 
        {
            var old = Read(brand.ID);
            old.Name = brand.Name;
            DbContext.SaveChanges();

        }
    }
}

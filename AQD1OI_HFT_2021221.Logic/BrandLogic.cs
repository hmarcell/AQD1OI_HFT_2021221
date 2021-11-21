using AQD1OI_HFT_2021221.Models;
using AQD1OI_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AQD1OI_HFT_2021221.Logic
{
    public class BrandLogic : IBrandLogic
    {
        IBrandRepository repo;

        public BrandLogic(IBrandRepository repo)
        {
            this.repo = repo;
        }

        public void Create(Brand brand)
        {
            if (brand.Name == null || brand.Name == "")
            {
                throw new ArgumentException("Name property cannot be null");
            }
            repo.Create(brand);
        }

        public void Delete(int id)
        {
            repo.Delete(id);
        }

        public Brand Read(int id)
        {
            return repo.Read(id);
        }

        public IEnumerable<Brand> ReadAll()
        {
            return repo.ReadAll();
        }

        public void Update(Brand brand)
        {
            if (brand.Name == null || brand.Name == "")
            {
                throw new ArgumentException("Name property cannot be null");
            }
            repo.Update(brand);
        }


        // non-CRUD

        


    }
}

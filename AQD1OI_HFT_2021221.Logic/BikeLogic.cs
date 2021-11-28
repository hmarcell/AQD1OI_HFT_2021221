using AQD1OI_HFT_2021221.Models;
using AQD1OI_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AQD1OI_HFT_2021221.Logic
{
    public class BikeLogic : IBikeLogic
    {
        IBikeRepository repo;

        public BikeLogic(IBikeRepository repo)
        {
            this.repo = repo;
        }

        public void Create(Bike bike)
        {
            if (bike.Price < 0)
            {
                throw new ArgumentException("Invalid price");
            }
            if (bike.Model == "" || bike.Model == null)
            {
                throw new ArgumentException("Model property cannot be null");
            }
            repo.Create(bike);
        }

        public void Delete(int id)
        {
            repo.Delete(id);
        }

        public Bike Read(int id)
        {
            return repo.Read(id);
        }

        public IEnumerable<Bike> ReadAll()
        {
            return repo.ReadAll();
        }

        public void Update(Bike bike)
        {
            if (bike.Price < 0)
            {
                throw new ArgumentException("Invalid price");
            }
            if (bike.Model == "" || bike.Model == null)
            {
                throw new ArgumentException("Model property cannot be null");
            }
            repo.Update(bike);
        }

        //non-CRUD

        public IEnumerable<KeyValuePair<string,double?>> AVGPriceByBrands()
        {
            var q = from x in repo.ReadAll().ToList()
                    group x by x.Brand into g
                    select new KeyValuePair<string, double?>(g.Key.Name, g.Average(x => x.Price));

            return q;
        }



    }
}

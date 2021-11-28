using AQD1OI_HFT_2021221.Models;
using AQD1OI_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AQD1OI_HFT_2021221.Logic
{
    public class RentalLogic : IRentalLogic
    {

        IRentalRepository repo;

        public RentalLogic(IRentalRepository repo)
        {
            this.repo = repo;
        }

        public void Create(Rental rental)
        {
            if (rental.Renter == null || rental.Renter == "")
            {
                throw new ArgumentException("Renter property cannot be null");
            }
            if (rental.Date == DateTime.MinValue)
            {
                throw new ArgumentException("Date property must be set");
            }
            repo.Create(rental);
        }

        public void Delete(int id)
        {
            repo.Delete(id);
        }

        public Rental Read(int id)
        {
            return repo.Read(id);
        }

        public IEnumerable<Rental> ReadAll()
        {
            return repo.ReadAll();
        }

        public void Update(Rental rental)
        {
            if (rental.Renter == null || rental.Renter == "")
            {
                throw new ArgumentException("Renter property cannot be null");
            }
            if (rental.Date == DateTime.MinValue)
            {
                throw new ArgumentException("Date property must be set");
            }
            repo.Update(rental);
        }


        //non-CRUD



        public IEnumerable<string> MostExpensiveBikeRenters()
        {

            int? maxPrice = repo.ReadAll().Max(x => x.Bike.Price);

            var renters = from x in repo.ReadAll()
                          where x.Bike.Price == maxPrice
                          select x.Renter;

            return renters;
        }
        public IEnumerable<KeyValuePair<string,DateTime>> DatesAndRenters(string model)
        {
            var q = from x in repo.ReadAll()
                    where x.Bike.Model == model
                    select new KeyValuePair<string, DateTime>(x.Renter, x.Date);
            return q;
        }

        public IEnumerable<KeyValuePair<string, int>> RentalsPerBike()
        {
            var q = from x in repo.ReadAll()
                    group x by x.Bike.Model into g
                    select new KeyValuePair<string, int>(g.Key, g.Count());

            return q;
        }

        public IEnumerable<KeyValuePair<string,DateTime>> Dates()
        {
            var q = from x in repo.ReadAll()
                    select new KeyValuePair<string, DateTime>(x.Bike.Model, x.Date);
            return q;
        }


        public IEnumerable<KeyValuePair<string,int?>> EarningsByBikes()
        {
            var q = from x in repo.ReadAll().ToList()
                    group x by x.Bike.Model into g
                    select new KeyValuePair<string, int?>(g.Key, g.Sum(x => x.Bike.Price));

            return q;
        }
    }
}

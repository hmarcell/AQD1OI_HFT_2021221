using AQD1OI_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AQD1OI_HFT_2021221.Logic
{
    public interface IRentalLogic
    {
        void Create(Rental rental);
        Rental Read(int id);
        void Update(Rental rental);
        void Delete(int id);
        IEnumerable<Rental> ReadAll();
    }
}

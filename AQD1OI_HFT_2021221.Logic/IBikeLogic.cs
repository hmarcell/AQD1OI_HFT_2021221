using AQD1OI_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AQD1OI_HFT_2021221.Logic
{
    interface IBikeLogic
    {
        void Create(Bike bike);
        Bike Read(int id);
        void Update(Bike bike);
        void Delete(int id);
        IEnumerable<Bike> ReadAll();
    }
}

using AQD1OI_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AQD1OI_HFT_2021221.Logic
{
    public interface IBrandLogic
    {
        void Create(Brand brand);
        Brand Read(int id);
        void Update(Brand brand);
        void Delete(int id);
        IEnumerable<Brand> ReadAll();
    }
}

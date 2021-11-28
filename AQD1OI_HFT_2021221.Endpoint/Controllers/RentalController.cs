using AQD1OI_HFT_2021221.Logic;
using AQD1OI_HFT_2021221.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AQD1OI_HFT_2021221.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        IRentalLogic rl;

        public RentalController(IRentalLogic rl)
        {
            this.rl = rl;
        }

        [HttpGet]
        public IEnumerable<Rental> Get()
        {
            return rl.ReadAll();
        }

        [HttpGet("{id}")]
        public Rental Get(int id)
        {
            return rl.Read(id);
        }
        
        [HttpPost]
        public void Post([FromBody] Rental rental)
        {
            rl.Create(rental);
        }
        
        [HttpPut]
        public void Put([FromBody] Rental rental)
        {
            rl.Update(rental);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            rl.Delete(id);
        }


    }
}

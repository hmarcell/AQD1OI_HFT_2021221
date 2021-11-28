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
    public class BikeController : ControllerBase
    {
        IBikeLogic bl;

        public BikeController(IBikeLogic bl)
        {
            this.bl = bl;
        }

        [HttpGet]
        public IEnumerable<Bike> Get()
        {
            return bl.ReadAll();
        }

        [HttpGet("{id}")]
        public Bike Get(int id)
        {
            return bl.Read(id);
        }

        [HttpPost]
        public void Post([FromBody] Bike bike)
        {
            bl.Create(bike);
        }
        
        [HttpPut]
        public void Put([FromBody] Bike bike)
        {
            bl.Update(bike);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            bl.Delete(id);
        }

    }
}

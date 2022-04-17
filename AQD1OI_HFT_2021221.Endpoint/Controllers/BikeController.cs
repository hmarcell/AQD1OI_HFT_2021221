using AQD1OI_HFT_2021221.Endpoint.Services;
using AQD1OI_HFT_2021221.Logic;
using AQD1OI_HFT_2021221.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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

        IHubContext<SignalRHub> hub;
        
        public BikeController(IBikeLogic bl, IHubContext<SignalRHub> hub)
        {
            this.bl = bl;
            this.hub = hub;
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
            this.hub.Clients.All.SendAsync("BikeCreated", bike);
        }

        [HttpPut]
        public void Put([FromBody] Bike bike)
        {
            bl.Update(bike);
            this.hub.Clients.All.SendAsync("BikeUpdated", bike);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var bikeToDelete = bl.Read(id);
            bl.Delete(id);
            this.hub.Clients.All.SendAsync("BikeDeleted", bikeToDelete);
        }

    }
}

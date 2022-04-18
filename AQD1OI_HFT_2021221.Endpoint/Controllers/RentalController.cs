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
    public class RentalController : ControllerBase
    {
        IRentalLogic rl;
        IHubContext<SignalRHub> hub;

        public RentalController(IRentalLogic rl, IHubContext<SignalRHub> hub)
        {
            this.rl = rl;
            this.hub = hub;
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
            this.hub.Clients.All.SendAsync("RentalCreated", rental);
        }

        [HttpPut]
        public void Put([FromBody] Rental rental)
        {
            rl.Update(rental);
            this.hub.Clients.All.SendAsync("RentalUpdated", rental);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Rental rentalToDelete = rl.Read(id);
            rl.Delete(id);
            this.hub.Clients.All.SendAsync("RentalDeleted", rentalToDelete);
        }


    }
}

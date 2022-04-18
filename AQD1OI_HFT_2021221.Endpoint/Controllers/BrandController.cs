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
    public class BrandController : ControllerBase
    {
        IBrandLogic bl;
        IHubContext<SignalRHub> hub;

        public BrandController(IBrandLogic bl, IHubContext<SignalRHub> hub)
        {
            this.bl = bl;
            this.hub = hub;
        }

        //Get /Brand
        [HttpGet]
        public IEnumerable<Brand> Get()
        {
            return bl.ReadAll();
        }

        //Get /brand/5
        [HttpGet("{id}")]
        public Brand Get(int id)
        {
            return bl.Read(id);
        }

        //Post /brand
        [HttpPost]
        public void Post([FromBody] Brand brand)
        {
            bl.Create(brand);
            this.hub.Clients.All.SendAsync("BrandCreated", brand);
        }

        //Put /brand
        [HttpPut]
        public void Put([FromBody] Brand brand)
        {
            bl.Update(brand);
            this.hub.Clients.All.SendAsync("BrandUpdated", brand);
        }

        //Delete /brand/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var brandToDelete = bl.Read(id);
            bl.Delete(id);
            this.hub.Clients.All.SendAsync("BrandDeleted", brandToDelete);
        }

    }
}

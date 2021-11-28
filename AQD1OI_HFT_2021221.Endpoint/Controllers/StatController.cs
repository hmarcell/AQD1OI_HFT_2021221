using AQD1OI_HFT_2021221.Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AQD1OI_HFT_2021221.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class StatController : ControllerBase
    {
        IRentalLogic rl;
        IBikeLogic bl;

        public StatController(IRentalLogic rl, IBikeLogic bl)
        {
            this.rl = rl;
            this.bl = bl;
        }

        //Get stat/avgpricebybrands
        [HttpGet]
        public IEnumerable<KeyValuePair<string, double?>> AVGPriceByBrands()
        {
            return bl.AVGPriceByBrands();
        }

        [HttpGet]
        public IEnumerable<string> MostExpensiveBikeRenters()
        {
            return rl.MostExpensiveBikeRenters();
        }
        [HttpGet("{model}")]
        public IEnumerable<KeyValuePair<string, DateTime>> DatesAndRenters(string model)
        {
            return rl.DatesAndRenters(model);
        }
        [HttpGet]
        public IEnumerable<KeyValuePair<string, int>> RentalsPerBike()
        {
            return rl.RentalsPerBike();
        }

        [HttpGet]
        public IEnumerable<KeyValuePair<string,DateTime>> Dates()
        {
            return rl.Dates();
        }

        [HttpGet]
        public IEnumerable<KeyValuePair<string, int?>> EarningsByBikes()
        {
            return rl.EarningsByBikes();
        }
    }
}

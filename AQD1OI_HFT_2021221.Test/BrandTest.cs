using AQD1OI_HFT_2021221.Logic;
using AQD1OI_HFT_2021221.Models;
using AQD1OI_HFT_2021221.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AQD1OI_HFT_2021221.Test
{
    [TestFixture]
    public class BrandTest
    {
        Mock<IBrandRepository> brandMock;
        BrandLogic bl;
        public BrandTest()
        {
            brandMock = new Mock<IBrandRepository>();
            bl = new BrandLogic(brandMock.Object);


            brandMock.Setup(r => r.Create(It.IsAny<Brand>()));

        }

        [TestCase("",true)]
        [TestCase(null,true)]
        [TestCase("testbrand",false)]
        public void CreateBrandThrowsException(string name, bool shouldThrow)
        {
            Brand testbrand = new Brand() { Name = name };

            if (shouldThrow)
            {
                Assert.That(() => bl.Create(testbrand), Throws.Exception);
            }
            else
            {
                Assert.That(() => bl.Create(testbrand), Throws.Nothing);
            }
            
        }

    }
}

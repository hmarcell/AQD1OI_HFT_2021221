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
    public class BikeTest
    {
        Mock<IBikeRepository> bikeMock;
        BikeLogic bl;
        public BikeTest()
        {
            bikeMock = new Mock<IBikeRepository>();
            bl = new BikeLogic(bikeMock.Object);

            Brand brand1 = new Brand() { Name = "brand1" };
            Brand brand2 = new Brand() { Name = "brand2" };

            bikeMock.Setup(r => r.ReadAll()).Returns(
                new List<Bike>()
                {
                    new Bike()
                        {
                            Model = "model1",
                            Price = 1000,
                            Brand = brand1
                        },
                     new Bike()
                        {
                            Model = "model2",
                            Price = 2000,
                            Brand = brand1
                        },
                      new Bike()
                        {
                            Model = "model3",
                            Price = 3000,
                            Brand = brand2
                        }
                }.AsQueryable());
            bikeMock.Setup(r => r.Create(It.IsAny<Bike>()));
        }

        [TestCase(-1,"testmodel",true)]
        [TestCase(0,null,true)]
        [TestCase(0,"",true)]
        [TestCase(0,"testmodel",false)]
        public void CreateBikeThrowsException(int price,string model, bool shouldThrow)
        {
            Bike testbike = new Bike()
            {
                Price = price,
                Model = model
            };

            if (shouldThrow)
            {
                Assert.That(() => bl.Create(testbike), Throws.Exception);
                bikeMock.Verify(r => r.Create(testbike), Times.Never);
            }
            else
            {
                Assert.That(() => bl.Create(testbike), Throws.Nothing);
                bikeMock.Verify(r => r.Create(testbike), Times.Once);
            }
        }

        [Test]
        public void AVGPriceByBrandsIsCorrect()
        {
            var expected = new List<KeyValuePair<string, double?>>()
            {
                new KeyValuePair<string, double?>("brand1",1500),
                new KeyValuePair<string, double?>("brand2",3000),
            };

            Assert.That(bl.AVGPriceByBrands(), Is.EquivalentTo(expected));
            
        }


    }
}

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
    public class RentalTest
    {
        Mock<IRentalRepository> rentalMock;
        RentalLogic rl;

        public RentalTest()
        {
            rentalMock = new Mock<IRentalRepository>();

            Bike expensiveBike = new Bike()
            {
                Model = "Merida Dual Thrust",
                Price = 10000000
            };

            Bike cheapBike = new Bike()
            {
                Model = "Csepel X2",
                Price = 10000
            };

            rentalMock.Setup(r => r.ReadAll()).Returns(
                new List<Rental>()
                {
                    new Rental()
                    {
                        Renter = "Expensive renter 1",
                        Bike = expensiveBike,
                        Date = new DateTime(2021,11,5)
                    },
                    new Rental()
                    {
                        Renter = "Expensive renter 2",
                        Bike = expensiveBike,
                        Date = new DateTime(2021,11,10)
                    },
                    new Rental()
                    {
                        Renter = "Cheap renter",
                        Bike = cheapBike,
                        Date = new DateTime(2021,11,15)
                    }
                }.AsQueryable()
                );
            rl = new RentalLogic(rentalMock.Object);

            rentalMock.Setup(r => r.Create(It.IsAny<Rental>()));
        }

        [Test]
        public void MostExpensiveBikeRentersIsCorrect()
        {
            var result = rl.MostExpensiveBikeRenters();

            Assert.That(result, Is.EquivalentTo(new List<string>() 
            { 
                "Expensive renter 1",
                "Expensive renter 2"
            }));
        }

        [TestCase(null,true)]
        [TestCase("testname",false)]
        public void CreateRentalWithoutRenterThrowsException(string renter, bool shouldThrow)
        {
            Rental r = new Rental()
            {
                Renter = renter,
                Date = DateTime.Now
            };
            if (shouldThrow)
            {
                Assert.That(() => rl.Create(r), Throws.ArgumentException);
                rentalMock.Verify(rm => rm.Create(r), Times.Never);
            }
            else
            {
                Assert.That(() => rl.Create(r), Throws.Nothing);
                rentalMock.Verify(x => x.Create(r), Times.Once);
            }
        }

        [TestCase(true)]
        [TestCase(false)]
        public void CreateRentalWithoutSettingDateThrowsException(bool shouldThrow)
        {
            
            if (shouldThrow)
            {
                Rental r = new Rental()
                {
                    Renter = "name"
                };
                Assert.That(() => rl.Create(r), Throws.ArgumentException);
                rentalMock.Verify(rm => rm.Create(r), Times.Never);
            }
            else
            {
                Rental r = new Rental()
                {
                    Renter = "name",
                    Date = DateTime.Now
                };
                Assert.That(() => rl.Create(r), Throws.Nothing);
                rentalMock.Verify(x => x.Create(r), Times.Once);
            }
        }

        [Test]
        public void RentalsPerBikeIsCorrect()
        {
            var result = rl.RentalsPerBike().ToList();

            var expected = new List<KeyValuePair<string,int>>()
                            {
                                new KeyValuePair<string,int>("Merida Dual Thrust", 2),
                                new KeyValuePair<string,int>("Csepel X2", 1),

                            };
            Assert.That(result, Is.EquivalentTo(expected));
        }

       [Test]
       public void DatesIsCorrect()
       {
            var expected = new List<KeyValuePair<string, DateTime>>()
                           {
                                new KeyValuePair<string, DateTime>("Merida Dual Thrust", new DateTime(2021,11,5)),
                                new KeyValuePair<string, DateTime>("Merida Dual Thrust", new DateTime(2021,11,10)),
                                new KeyValuePair<string, DateTime>("Csepel X2", new DateTime(2021,11,15))
                           };
            Assert.That(rl.ModelsAndDates(), Is.EquivalentTo(expected));
       }

       [TestCase("Merida Dual Thrust")]
       public void DatesAndRentersIsCorrect(string model)
       {
            var expected = new List<KeyValuePair<string, DateTime>>()
                           {
                                new KeyValuePair<string, DateTime>("Expensive renter 1", new DateTime(2021,11,5)),
                                new KeyValuePair<string, DateTime>("Expensive renter 2", new DateTime(2021,11,10))
                           };

            Assert.That(rl.DatesAndRenters(model), Is.EquivalentTo(expected));
       }

       [Test]
       public void EarningsByBikesIsCorrect()
       { 
            var expected = new List<KeyValuePair<string, int?>>()
                                {
                                    new KeyValuePair<string, int?>("Merida Dual Thrust", 20000000),
                                    new KeyValuePair<string, int?>("Csepel X2", 10000)
                                };
            Assert.That(rl.EarningsByBikes(), Is.EquivalentTo(expected));
       }


    }
}

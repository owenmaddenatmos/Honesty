using System;
using System.Collections.Generic;
using System.Linq;
using CarSalesmen;
using NUnit.Framework;

namespace CarSalesmenTests
{
    /// <summary>
    /// You have a discerning client who wants to buy 2 vehicles
    /// 1. A Honda Jazz that must be less than 50 days old
    /// 2. A Ford Luton van that must be less than 100 days old
    /// Your client is very money conscious so your task is to shop around
    /// and buy these vehicles for the lowest possible price.
    /// You have some potential untrustworthy sellers that I have instantiated for you. 
    /// </summary>

    public class VehicleBuyer
    {
        public IEnumerable<VehicleReceipt> BuyVehicles(IEnumerable<VehicleRequest> vehiclesToBuy)
        {
            var receipts = new List<VehicleReceipt>();
            JudsonsJallopies judsonsJallopies = new JudsonsJallopies();
            IMattsMotors mattsMotors = new MattsMotors();
            JimmysAutomobiles jimmysAutomobiles = new JimmysAutomobiles();

            throw new NotImplementedException();
        }
    }

    public class VehicleBuyerTests
    {
        [Test]
        public void WhenIBuyTheVehicles_IGetTheLowestPossiblePrice()
        {
            var vehicleBuyer = new VehicleBuyer();
            var receipts = vehicleBuyer.BuyVehicles(new []
            {
                new VehicleRequest(VehicleModel.HondaJazz, TimeSpan.FromDays(50)),
                new VehicleRequest(VehicleModel.FordLuton, TimeSpan.FromDays(100))
            });

            AssertReceiptsAreCorrect(receipts);
        }

        private static void AssertReceiptsAreCorrect(IEnumerable<VehicleReceipt> receipts)
        {
            Assert.That(receipts.Count(), Is.EqualTo(2));
            var jazzReceipt = receipts.FirstOrDefault(r => r.PurchasedModel == VehicleModel.HondaJazz);
            Assert.IsNotNull(jazzReceipt);
            Assert.That(jazzReceipt.Price.Amount, Is.EqualTo(1000.0));
            var lutonReceipt = receipts.FirstOrDefault(r => r.PurchasedModel == VehicleModel.FordLuton);
            Assert.IsNotNull(lutonReceipt);
            Assert.That(lutonReceipt.Price.Amount, Is.EqualTo(5000.0));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarSalesmen
{   
    /// <summary>
    /// Where the seller has the last laugh
    /// </summary>
    public class JimmysAutomobiles
    {
        private List<Vehicle> _prices;

        public JimmysAutomobiles()
        {
            _prices = new List<Vehicle> { new Vehicle(VehicleModel.HondaJazz, new PriceInPounds(1000.0), TimeSpan.FromDays(40)), 
                new Vehicle(VehicleModel.HondaCivic, new PriceInPounds(70000.0), TimeSpan.Zero)};
        }

        public JimmyDeal GetQuote(VehicleModel vehicleModel, TimeSpan maxAge)
        {
            if (maxAge > TimeSpan.FromDays(60)) throw new ArgumentException("We don't sell old bangers here, get down to MattsMotors for that kind of rubbish");
            
            var vehicle = _prices.FirstOrDefault(v => v.Model == vehicleModel && v.Age < maxAge);
            if (vehicle != null)
            {
                return new JimmyDeal(vehicle.Model, vehicle.Price, vehicle.Age);
            }
            return null;
        }

        public VehicleReceipt BuyVehicle(JimmyDeal quote)
        {
            var price = _prices.First(v => v.Model == quote.Model && v.Age == quote.Age).Price;
            return new VehicleReceipt(price, quote.Model);
        }
        
        private class Vehicle
        {
            public VehicleModel Model { get; }
            public PriceInPounds Price { get; }
            public TimeSpan Age { get; }

            public Vehicle(VehicleModel model, PriceInPounds price, TimeSpan age)
            {
                Model = model;
                Price = price;
                Age = age;
            }
        }
    }
    
    public class JimmyDeal
    {
        public JimmyDeal(VehicleModel model, PriceInPounds price, TimeSpan age)
        {
            Model = model;
            Price = price;
            Age = age;
        }

        public VehicleModel Model { get; }
        public PriceInPounds Price { get; }
        public TimeSpan Age { get; }
    }
}
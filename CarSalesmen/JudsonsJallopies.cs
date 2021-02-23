using System;
using System.Collections.Generic;
using System.Linq;

namespace CarSalesmen
{
    /// <summary>
    /// Free jumper with every purchase
    /// </summary>
    public class JudsonsJallopies
    {
        private readonly List<Vehicle> _prices = new List<Vehicle> { new Vehicle(VehicleModel.HondaJazz, new PriceInPounds(800.0), TimeSpan.FromDays(40)), 
            new Vehicle(VehicleModel.HondaCivic, new PriceInPounds(70000.0), TimeSpan.Zero)};
        
        public JudsonsUnbeatableQuote GetQuote(VehicleModel vehicleModel, TimeSpan maxAge)
        {
            var vehicle = _prices.FirstOrDefault(v => v.Model == vehicleModel && v.Age < maxAge);
            if (vehicle != null)
            {
                var quote = new JudsonsUnbeatableQuote(vehicle.Model, vehicle.Price, vehicle.Age);
                vehicle.Price = new PriceInPounds(vehicle.Price.Amount * 1.5); // This car is getting popular let's increase the price. 
                return quote;
            }
            return null;
        }

        public VehicleReceipt BuyVehicle(JudsonsUnbeatableQuote quote)
        {
            var price = _prices.First(v => v.Model == quote.Model && v.Age == quote.Age).Price;
            return new VehicleReceipt(price, quote.Model);
        }
        
        private class Vehicle
        {
            public VehicleModel Model { get; }
            public PriceInPounds Price { get; set; }
            public TimeSpan Age { get; }

            public Vehicle(VehicleModel model, PriceInPounds price, TimeSpan age)
            {
                Model = model;
                Price = price;
                Age = age;
            }
        }
    }
    
    public class JudsonsUnbeatableQuote
    {
        public JudsonsUnbeatableQuote(VehicleModel model, PriceInPounds price, TimeSpan age)
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
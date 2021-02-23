using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Atmos.Shared.Code.FunctionalMethods.ResultObject;

namespace CarSalesmen
{
    /// <summary>
    /// Free jumper with every purchase
    /// </summary>
    public class JudsonsJallopies
    {
        private List<Vehicle> _prices;

        public JudsonsJallopies()
        {
            _prices = new List<Vehicle> { new Vehicle(VehicleModel.HondaJazz, new PriceInPounds(1000.0), TimeSpan.FromDays(40)), 
                new Vehicle(VehicleModel.HondaCivic, new PriceInPounds(70000.0), TimeSpan.Zero)};
        }

        public JudsonsUnbeatableQuote GetQuote(VehicleModel vehicleModel, TimeSpan maxAge)
        {
            var vehicle = _prices.FirstOrDefault(v => v.Model == vehicleModel && v.Age < maxAge);
            if (vehicle != null)
            {
                return new JudsonsUnbeatableQuote(vehicle.Model, vehicle.Price, vehicle.Age);
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
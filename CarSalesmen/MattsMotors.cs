using System.Collections.Generic;
using System.Linq;

namespace CarSalesmen
{
    public interface IMattsMotors
    {
        /// <summary>
        /// Age is in hours 
        /// </summary>
        MattsTrustworthyQuote GetPrice(VehicleModel vehicleModel, double maxAge);

        VehicleReceipt BuyVehicle(MattsTrustworthyQuote quote);
    }

    /// <summary>
    /// "I normally just hire one"
    /// </summary>
    public class MattsMotors : IMattsMotors
    {   
        /// <summary>
        /// Prices are in crypto currency atmoscoin, cause we are cutting edge. Exchange rate is one atmoscoin = £2 
        /// </summary>
        private List<Vehicle> _prices;

        public void Initialise()
        {
            _prices = new List<Vehicle> { new Vehicle(VehicleModel.HondaJazz, 500.0, 1201.0), 
                new Vehicle(VehicleModel.PushBike, 500.0, 400.0), new Vehicle(VehicleModel.FordLuton, 500.0, 24.0) };
        }
        
        /// <summary>
        /// Age is in hours 
        /// </summary>
        public MattsTrustworthyQuote GetPrice(VehicleModel vehicleModel, double maxAge)
        {
            var vehicle = _prices.FirstOrDefault(v => v.Model == vehicleModel && v.Age < maxAge);
            return new MattsTrustworthyQuote(vehicleModel, vehicle?.Price ?? -100.0, vehicle?.Age ?? 0.0);
        }

        public VehicleReceipt BuyVehicle(MattsTrustworthyQuote quote)
        {
            var price = new PriceInPounds(_prices.First(v => v.Model == quote.Model && v.Age == quote.Age).Price * 2.0);
            return new VehicleReceipt(price, quote.Model);
        }

        private class Vehicle
        {
            public VehicleModel Model { get; }
            public double Price { get; }
            public double Age { get; }

            public Vehicle(VehicleModel model, double price, double age)
            {
                Model = model;
                Price = price;
                Age = age;
            }
        }
    }
    
    public class MattsTrustworthyQuote
    {
        public MattsTrustworthyQuote(VehicleModel model, double price, double age)
        {
            Model = model;
            Price = price;
            Age = age;
        }

        public VehicleModel Model { get; }
        
        public double Price { get; }
        
        public double Age { get; }
    }
}
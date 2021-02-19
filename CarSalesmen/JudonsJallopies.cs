using System.Collections.Generic;
using System.Diagnostics;

namespace CarSalesmen
{
    public class PriceInPounds
    {
        public PriceInPounds(double amount)
        {
            Amount = amount;
        }

        public double Amount { get; }
    }

    public class JudsonsJallopies
    {
        private Dictionary<string, PriceInPounds> _prices = new Dictionary<string, PriceInPounds> {{"HONDA JAZZ", new PriceInPounds(500.0)}};

        public PriceInPounds GetPrice(string vehicleName)
        {
            return _prices.ContainsKey(vehicleName) ? _prices[vehicleName] : null;
        }
            
            
    }
}
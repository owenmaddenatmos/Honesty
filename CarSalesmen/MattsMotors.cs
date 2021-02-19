using System;
using System.Collections.Generic;

namespace CarSalesmen
{
    public class MattsMotors
    {
        private Dictionary<string, double> _prices;

        public void Initialise()
        {
            _prices = new Dictionary<string, double> {{"HONDA JAZZ", 500.0}};
        }

        public double GetPrice(string vehicleName)
        {
            return _prices[vehicleName];
        }
    }
}
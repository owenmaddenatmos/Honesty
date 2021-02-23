using System;

namespace CarSalesmen
{
    public class VehicleRequest
    {
        public VehicleRequest(VehicleModel requestedModel, TimeSpan maxAge)
        {
            MaxAge = maxAge;
            RequestedModel = requestedModel;
        }

        public TimeSpan MaxAge { get; }
        
        public VehicleModel RequestedModel { get; } 
    }
}
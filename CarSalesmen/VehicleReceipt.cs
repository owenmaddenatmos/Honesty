namespace CarSalesmen
{
    public class VehicleReceipt
    {
        public VehicleReceipt(PriceInPounds price, VehicleModel purchasedModel)
        {
            Price = price;
            PurchasedModel = purchasedModel;
        }

        public PriceInPounds Price { get; }
        
        public VehicleModel PurchasedModel { get; } 
    }
}
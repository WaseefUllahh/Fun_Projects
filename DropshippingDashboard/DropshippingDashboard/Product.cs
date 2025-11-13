public class Product
{
     public int ProductId { get; set; } // <-- ADD THIS LINE
     public string ProductName { get; set; }
     public string SupplierURL { get; set; }
     public decimal SupplierPrice { get; set; }
     public decimal ShippingCost { get; set; }
     public decimal TargetSalePrice { get; set; }

     // Calculated Property (read-only)
     public decimal TotalCost
     {
          get { return SupplierPrice + ShippingCost; }
     }

     // Calculated Property (read-only)
     public decimal PotentialProfit
     {
          // Assumes a 13% fee
          get { return TargetSalePrice - (TargetSalePrice * 0.13m) - TotalCost; }
     }
}
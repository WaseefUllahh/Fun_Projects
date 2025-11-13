using System;

// An enum makes the 'Status' field clean
public enum OrderStatus
{
     Pending,
     Ordered,
     Shipped
}

public class Order
{
     public int OrderId { get; set; } // Unique identifier for the order
     public DateTime OrderDate { get; set; }
     public string EbayItemName { get; set; }
     public decimal SalePrice { get; set; }
     public decimal TotalCost { get; set; } // This will be copied from the Product
     public string Status { get; set; }
     public string TrackingNumber { get; set; }

     // Calculated Property (read-only)
     public decimal ActualProfit
     {
          // Assumes a 13% fee
          get { return SalePrice - (SalePrice * 0.13m) - TotalCost; }
     }
}
using System;
using System.Collections.Generic;
using System.Data.SqlClient; // <-- IMPORTANT: For SQL
using System.Windows.Forms; // <-- For MessageBox

public class DataAccess
{
     // This is the "password" to your database
     private const string connectionString = @"Server=(local)\SQLEXPRESS;Database=DropshippingDB;Trusted_Connection=True;";

     // Method to get all products from the database
     public List<Product> LoadProducts()
     {
          List<Product> productList = new List<Product>();

          try
          {
               // 'using' statements automatically close the connection
               using (SqlConnection connection = new SqlConnection(connectionString))
               {
                    connection.Open();
                    string sql = "SELECT * FROM Products";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                         using (SqlDataReader reader = command.ExecuteReader())
                         {
                              while (reader.Read())
                              {
                                   Product product = new Product
                                   {
                                        ProductId = (int)reader["ProductId"],
                                        ProductName = (string)reader["ProductName"],
                                        SupplierURL = reader["SupplierURL"].ToString(),
                                        SupplierPrice = (decimal)reader["SupplierPrice"],
                                        ShippingCost = (decimal)reader["ShippingCost"],
                                        TargetSalePrice = (decimal)reader["TargetSalePrice"]
                                   };
                                   productList.Add(product);
                              }
                         }
                    }
               }
          }
          catch (Exception ex)
          {
               MessageBox.Show($"Failed to load products: {ex.Message}");
          }

          return productList;
     }

     // Method to get all orders from the database
     public List<Order> LoadOrders()
     {
          List<Order> orderList = new List<Order>();
          try
          {
               using (SqlConnection connection = new SqlConnection(connectionString))
               {
                    connection.Open();
                    string sql = "SELECT * FROM Orders";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                         using (SqlDataReader reader = command.ExecuteReader())
                         {
                              while (reader.Read())
                              {
                                   Order order = new Order
                                   {
                                        OrderId = (int)reader["OrderId"],
                                        OrderDate = (DateTime)reader["OrderDate"],
                                        EbayItemName = (string)reader["EbayItemName"],
                                        SalePrice = (decimal)reader["SalePrice"],
                                        TotalCost = (decimal)reader["TotalCost"],
                                        Status = (string)reader["Status"],
                                        TrackingNumber = reader["TrackingNumber"].ToString()
                                   };
                                   orderList.Add(order);
                              }
                         }
                    }
               }
          }
          catch (Exception ex)
          {
               MessageBox.Show($"Failed to load orders: {ex.Message}");
          }
          return orderList;
     }
     // Method to add a new product to the database
     public Product AddProduct(Product product)
     {
          try
          {
               using (SqlConnection connection = new SqlConnection(connectionString))
               {
                    connection.Open();

                    // 1. Define the SQL INSERT command
                    // SCOPE_IDENTITY() gets the new ID created by the database
                    string sql = @"INSERT INTO Products (ProductName, SupplierURL, SupplierPrice, ShippingCost, TargetSalePrice) 
                               VALUES (@ProductName, @SupplierURL, @SupplierPrice, @ShippingCost, @TargetSalePrice);
                               SELECT SCOPE_IDENTITY();"; // This gets the new ProductId

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                         // 2. Add parameters to prevent SQL injection
                         command.Parameters.AddWithValue("@ProductName", product.ProductName);
                         command.Parameters.AddWithValue("@SupplierURL", product.SupplierURL);
                         command.Parameters.AddWithValue("@SupplierPrice", product.SupplierPrice);
                         command.Parameters.AddWithValue("@ShippingCost", product.ShippingCost);
                         command.Parameters.AddWithValue("@TargetSalePrice", product.TargetSalePrice);

                         // 3. Execute the command and get the new ID
                         // ExecuteScalar is used when you expect a single value back (like the new ID)
                         int newId = Convert.ToInt32(command.ExecuteScalar());

                         // 4. Update the product object with its new ID
                         product.ProductId = newId;
                    }
               }
          }
          catch (Exception ex)
          {
               MessageBox.Show($"Failed to add product: {ex.Message}");
          }

          return product;
     }
     // Method to delete a product from the database
     public void DeleteProduct(int productId)
     {
          try
          {
               using (SqlConnection connection = new SqlConnection(connectionString))
               {
                    connection.Open();

                    // 1. Define the SQL DELETE command
                    string sql = "DELETE FROM Products WHERE ProductId = @ProductId";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                         // 2. Add the ProductId as a parameter
                         command.Parameters.AddWithValue("@ProductId", productId);

                         // 3. Execute the command
                         command.ExecuteNonQuery(); // Used for commands that don't return data
                    }
               }
          }
          catch (Exception ex)
          {
               MessageBox.Show($"Failed to delete product: {ex.Message}");
          }
     }
     // Method to update an existing product in the database
     public void UpdateProduct(Product product)
     {
          try
          {
               using (SqlConnection connection = new SqlConnection(connectionString))
               {
                    connection.Open();

                    // 1. Define the SQL UPDATE command
                    string sql = @"UPDATE Products 
                               SET ProductName = @ProductName, 
                                   SupplierURL = @SupplierURL, 
                                   SupplierPrice = @SupplierPrice, 
                                   ShippingCost = @ShippingCost, 
                                   TargetSalePrice = @TargetSalePrice
                               WHERE ProductId = @ProductId";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                         // 2. Add all parameters
                         command.Parameters.AddWithValue("@ProductName", product.ProductName);
                         command.Parameters.AddWithValue("@SupplierURL", product.SupplierURL);
                         command.Parameters.AddWithValue("@SupplierPrice", product.SupplierPrice);
                         command.Parameters.AddWithValue("@ShippingCost", product.ShippingCost);
                         command.Parameters.AddWithValue("@TargetSalePrice", product.TargetSalePrice);
                         command.Parameters.AddWithValue("@ProductId", product.ProductId); // <-- The WHERE clause

                         // 3. Execute the command
                         command.ExecuteNonQuery();
                    }
               }
          }
          catch (Exception ex)
          {
               MessageBox.Show($"Failed to update product: {ex.Message}");
          }
     }
     // Method to add a new order to the database
     public Order AddOrder(Order order)
     {
          try
          {
               using (SqlConnection connection = new SqlConnection(connectionString))
               {
                    connection.Open();

                    string sql = @"INSERT INTO Orders (OrderDate, EbayItemName, SalePrice, TotalCost, Status, TrackingNumber)
                               VALUES (@OrderDate, @EbayItemName, @SalePrice, @TotalCost, @Status, @TrackingNumber);
                               SELECT SCOPE_IDENTITY();"; // Gets the new OrderId

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                         command.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                         command.Parameters.AddWithValue("@EbayItemName", order.EbayItemName);
                         command.Parameters.AddWithValue("@SalePrice", order.SalePrice);
                         command.Parameters.AddWithValue("@TotalCost", order.TotalCost);
                         command.Parameters.AddWithValue("@Status", order.Status);
                         command.Parameters.AddWithValue("@TrackingNumber", order.TrackingNumber);

                         int newId = Convert.ToInt32(command.ExecuteScalar());
                         order.OrderId = newId; // Update the object with its new ID
                    }
               }
          }
          catch (Exception ex)
          {
               MessageBox.Show($"Failed to add order: {ex.Message}");
          }
          return order;
     }

     // Method to update an existing order
     public void UpdateOrder(Order order)
     {
          try
          {
               using (SqlConnection connection = new SqlConnection(connectionString))
               {
                    connection.Open();

                    string sql = @"UPDATE Orders
                               SET OrderDate = @OrderDate,
                                   EbayItemName = @EbayItemName,
                                   SalePrice = @SalePrice,
                                   TotalCost = @TotalCost,
                                   Status = @Status,
                                   TrackingNumber = @TrackingNumber
                               WHERE OrderId = @OrderId";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                         command.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                         command.Parameters.AddWithValue("@EbayItemName", order.EbayItemName);
                         command.Parameters.AddWithValue("@SalePrice", order.SalePrice);
                         command.Parameters.AddWithValue("@TotalCost", order.TotalCost);
                         command.Parameters.AddWithValue("@Status", order.Status);
                         command.Parameters.AddWithValue("@TrackingNumber", order.TrackingNumber);
                         command.Parameters.AddWithValue("@OrderId", order.OrderId);

                         command.ExecuteNonQuery();
                    }
               }
          }
          catch (Exception ex)
          {
               MessageBox.Show($"Failed to update order: {ex.Message}");
          }
     }

     // Method to delete an order
     public void DeleteOrder(int orderId)
     {
          try
          {
               using (SqlConnection connection = new SqlConnection(connectionString))
               {
                    connection.Open();
                    string sql = "DELETE FROM Orders WHERE OrderId = @OrderId";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                         command.Parameters.AddWithValue("@OrderId", orderId);
                         command.ExecuteNonQuery();
                    }
               }
          }
          catch (Exception ex)
          {
               MessageBox.Show($"Failed to delete order: {ex.Message}");
          }
     }
}
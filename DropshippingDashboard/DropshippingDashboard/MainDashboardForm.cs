using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq; // <-- ADD THIS LINE

namespace DropshippingDashboard
{
     public partial class MainDashboardForm : Form
     {
          // Our master list to hold all product data
          private List<Product> productList = new List<Product>();
          private List<Order> orderList = new List<Order>();
          // Add this line:
          private DataAccess dataAccess = new DataAccess();

          public MainDashboardForm()
          {
               InitializeComponent();
               
          }
          private void RefreshOrderGrid()
          {
               dgvOrders.DataSource = null; // Unbind
               dgvOrders.DataSource = orderList; // Rebind

               // Optional: Make it look a bit better
               dgvOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
          }
          private void RefreshProductGrid()
          {
               // This method now binds the *entire* list and formats it
               dgvProducts.DataSource = null;
               dgvProducts.DataSource = productList;
               FormatProductGrid();
          }
          private void FormatProductGrid()
          {
               // This helper method just applies formatting
               if (dgvProducts.Columns["SupplierURL"] != null)
               {
                    dgvProducts.Columns["SupplierURL"].Visible = false;
               }
               dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
          }

          private void btnAddProduct_Click(object sender, EventArgs e)
          {
               ProductForm newProductForm = new ProductForm();
               DialogResult result = newProductForm.ShowDialog();

               if (result == DialogResult.OK)
               {
                    // 1. Get the new product from the form
                    Product newProduct = newProductForm.CurrentProduct;

                    // 2. Save it to the database using our new method
                    // This will also update the newProduct object with its new ID
                    dataAccess.AddProduct(newProduct);

                    // 3. Add the fully updated product to our local list
                    productList.Add(newProduct);

                    // 4. Refresh the grid
                    RefreshProductGrid();
                    UpdateDashboard();
               }
          }

          private void btnDeleteProduct_Click(object sender, EventArgs e)
          {
               // 1. Check if a row is selected
               if (dgvProducts.SelectedRows.Count == 0)
               {
                    MessageBox.Show("Please select a product to delete.", "No Product Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
               }

               // 2. Get the selected product from the grid
               Product selectedProduct = (Product)dgvProducts.SelectedRows[0].DataBoundItem;

               // 3. Ask the user to confirm
               DialogResult confirm = MessageBox.Show($"Are you sure you want to delete '{selectedProduct.ProductName}'?",
                                                    "Confirm Delete",
                                                    MessageBoxButtons.YesNo,
                                                    MessageBoxIcon.Question);

               if (confirm == DialogResult.Yes)
               {
                    // 4. Delete from the database
                    dataAccess.DeleteProduct(selectedProduct.ProductId);

                    // 5. Remove from the local list
                    productList.Remove(selectedProduct);

                    // 6. Refresh the grid
                    RefreshProductGrid();
                    UpdateDashboard();
               }
          }

          private void btnEditProduct_Click(object sender, EventArgs e)
          {
               // 1. Check if a row is selected
               if (dgvProducts.SelectedRows.Count == 0)
               {
                    MessageBox.Show("Please select a product to edit.", "No Product Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
               }

               // 2. Get the selected product
               Product selectedProduct = (Product)dgvProducts.SelectedRows[0].DataBoundItem;

               // 3. Create the edit form
               ProductForm editForm = new ProductForm();
               editForm.CurrentProduct = selectedProduct; // Give the form the product

               // 4. Show the form
               DialogResult result = editForm.ShowDialog();

               // 5. If the user clicked "Save"
               if (result == DialogResult.OK)
               {
                    // The editForm already updated the 'selectedProduct' object's properties.

                    // 6. Save the changes to the database
                    dataAccess.UpdateProduct(selectedProduct);

                    // 7. Refresh the grid to show the changes
                    RefreshProductGrid();
                    UpdateDashboard();
               }
          }

          // --- ADD ORDER ---
          private void btnAddOrder_Click(object sender, EventArgs e)
          {
               OrderForm newOrderForm = new OrderForm();
               DialogResult result = newOrderForm.ShowDialog();

               if (result == DialogResult.OK)
               {
                    // 1. Get the new order from the form
                    Order newOrder = newOrderForm.CurrentOrder;

                    // 2. Save it to the database
                    dataAccess.AddOrder(newOrder);

                    // 3. Add the order (with its new ID) to our local list
                    orderList.Add(newOrder);

                    // 4. Refresh the grid
                    RefreshOrderGrid();
                    UpdateDashboard();
               }
          }

          // --- EDIT ORDER ---
          private void btnEditOrder_Click(object sender, EventArgs e)
          {
               if (dgvOrders.SelectedRows.Count == 0)
               {
                    MessageBox.Show("Please select an order to edit.", "No Order Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
               }

               Order selectedOrder = (Order)dgvOrders.SelectedRows[0].DataBoundItem;
               OrderForm editForm = new OrderForm();
               editForm.CurrentOrder = selectedOrder;

               DialogResult result = editForm.ShowDialog();

               if (result == DialogResult.OK)
               {
                    // 1. Save the changes to the database
                    dataAccess.UpdateOrder(selectedOrder);

                    // 2. Refresh the grid
                    RefreshOrderGrid();
                    UpdateDashboard();
               }
          }

          // --- DELETE ORDER ---
          private void btnDeleteOrder_Click(object sender, EventArgs e)
          {
               if (dgvOrders.SelectedRows.Count == 0)
               {
                    MessageBox.Show("Please select an order to delete.", "No Order Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
               }

               Order selectedOrder = (Order)dgvOrders.SelectedRows[0].DataBoundItem;

               DialogResult confirm = MessageBox.Show($"Are you sure you want to delete the order for '{selectedOrder.EbayItemName}'?",
                                                    "Confirm Delete",
                                                    MessageBoxButtons.YesNo,
                                                    MessageBoxIcon.Question);

               if (confirm == DialogResult.Yes)
               {
                    // 1. Delete from the database
                    dataAccess.DeleteOrder(selectedOrder.OrderId);

                    // 2. Remove from the local list
                    orderList.Remove(selectedOrder);

                    // 3. Refresh the grid
                    RefreshOrderGrid();
                    UpdateDashboard();
               }
          }

          // --- LOG SALE (This one also needs to be updated) ---
          private void btnLogSale_Click(object sender, EventArgs e)
          {
               if (dgvProducts.SelectedRows.Count == 0)
               {
                    MessageBox.Show("Please select a product to log a sale for.", "No Product Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
               }

               Product selectedProduct = (Product)dgvProducts.SelectedRows[0].DataBoundItem;

               Order newOrder = new Order();
               newOrder.OrderDate = DateTime.Now;
               newOrder.EbayItemName = selectedProduct.ProductName;
               newOrder.TotalCost = selectedProduct.TotalCost;
               newOrder.Status = "Pending"; // Set default status as a string

               OrderForm logSaleForm = new OrderForm();
               logSaleForm.CurrentOrder = newOrder;
               logSaleForm.IsLoggingSale = true;

               DialogResult result = logSaleForm.ShowDialog();

               if (result == DialogResult.OK)
               {
                    // The form updated 'newOrder' with sale price, etc.

                    // 1. Save the new order to the database
                    dataAccess.AddOrder(newOrder);

                    // 2. Add to our local list
                    orderList.Add(newOrder);

                    // 3. Refresh and switch tabs
                    RefreshOrderGrid();
                    tabControl1.SelectedTab = tabOrders;
                    UpdateDashboard();
               }
          }

          private void MainDashboardForm_Load(object sender, EventArgs e)
          {
               // Load data from the SQL database
               productList = dataAccess.LoadProducts();
               orderList = dataAccess.LoadOrders();

               // Refresh grids to show the loaded data
               RefreshProductGrid();
               RefreshOrderGrid();
               UpdateDashboard();
          }
          private void UpdateDashboard()
          {
               // Calculate total actual profit from the order list
               decimal actualProfit = 0;
               foreach (Order order in orderList)
               {
                    actualProfit += order.ActualProfit;
               }

               // Calculate total potential profit from the product list
               decimal potentialProfit = 0;
               foreach (Product product in productList)
               {
                    potentialProfit += product.PotentialProfit;
               }

               // Update the labels
               // "C" is the standard format string for currency
               lblTotalActualProfit.Text = $"Total Actual Profit: {actualProfit:C}";
               lblTotalPotentialProfit.Text = $"Total Potential Profit: {potentialProfit:C}";
          }

          private void dgvProducts_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
          {
               // List of column names to format as currency
               List<string> currencyColumns = new List<string> {
        "SupplierPrice", "ShippingCost", "TotalCost", "TargetSalePrice", "PotentialProfit"
    };

               // Get the column name
               string colName = dgvProducts.Columns[e.ColumnIndex].Name;

               if (currencyColumns.Contains(colName))
               {
                    // Format the cell as currency (e.g., "$12.34")
                    if (e.Value != null)
                    {
                         e.Value = $"{e.Value:C}";
                         e.FormattingApplied = true;
                    }
               }
          }

          private void dgvOrders_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
          {
               List<string> currencyColumns = new List<string> {
        "SalePrice", "TotalCost", "ActualProfit"
    };

               string colName = dgvOrders.Columns[e.ColumnIndex].Name;

               if (currencyColumns.Contains(colName))
               {
                    if (e.Value != null)
                    {
                         e.Value = $"{e.Value:C}";
                         e.FormattingApplied = true;
                    }
               }
          }

          private void txtSearchProducts_TextChanged(object sender, EventArgs e)
          {
               // 1. Get the search text, remove spaces, and make it lowercase
               string searchText = txtSearchProducts.Text.Trim().ToLower();

               if (string.IsNullOrWhiteSpace(searchText))
               {
                    // 2. If the search box is empty, show the full list
                    RefreshProductGrid();
               }
               else
               {
                    // 3. If search box has text, filter the list
                    //    This uses LINQ to find all products where the name contains the search text
                    var filteredList = productList.Where(product =>
                        product.ProductName.ToLower().Contains(searchText)
                    ).ToList();

                    // 4. Bind the *filtered* list to the grid
                    dgvProducts.DataSource = null;
                    dgvProducts.DataSource = filteredList;
                    FormatProductGrid(); // Apply the same formatting
               }
          }
     }
}

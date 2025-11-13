using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DropshippingDashboard
{
     public partial class OrderForm : Form
     {
          // This public property will hold the order being created or edited
          public Order CurrentOrder { get; set; }
          public bool IsLoggingSale { get; set; }
          public OrderForm()
          {
               InitializeComponent();
          }

          private void OrderForm_Load(object sender, EventArgs e)
          {
               cmbStatus.Items.Clear();
               cmbStatus.Items.Add("Pending");
               cmbStatus.Items.Add("Ordered");
               cmbStatus.Items.Add("Shipped");

               // 2. Set default increment (for cents)
               numTotalCost.Increment = 0.01m; // 'm' means decimal

               // 3. Check if an order was passed in
               if (CurrentOrder != null)
               {
                    // 4. Fill all the controls
                    dtpOrderDate.Value = CurrentOrder.OrderDate;
                    txtItemName.Text = CurrentOrder.EbayItemName;
                    numSalePrice.Value = CurrentOrder.SalePrice;
                    numTotalCost.Value = CurrentOrder.TotalCost;
                    txtTrackingNumber.Text = CurrentOrder.TrackingNumber;
                    cmbStatus.SelectedItem = CurrentOrder.Status.ToString();

                    // 5. Set the title AND lock/unlock controls
                    if (IsLoggingSale)
                    {
                         this.Text = "Log New Sale";

                         // --- THIS IS THE FIX ---
                         numTotalCost.ReadOnly = true;   // Stops typing
                         numTotalCost.Increment = 0;     // Stops arrows
                                                         // -----------------------
                    }
                    else
                    {
                         this.Text = "Edit Order";
                         // Make sure controls are editable for "Edit Mode"
                         numTotalCost.ReadOnly = false;
                         numTotalCost.Increment = 0.01m;
                    }
               }
               else
               {
                    this.Text = "Add New Order";
                    // Make sure controls are editable for "Add Mode"
                    numTotalCost.ReadOnly = false;
                    numTotalCost.Increment = 0.01m;
               }
          }

          private void btnSave_Click(object sender, EventArgs e)
          {
               // 1. Check if we are in "Add Mode"
               if (CurrentOrder == null)
               {
                    CurrentOrder = new Order();
               }

               // 2. Update the order's properties from the controls
               CurrentOrder.OrderDate = dtpOrderDate.Value;
               CurrentOrder.EbayItemName = txtItemName.Text;
               CurrentOrder.SalePrice = numSalePrice.Value;
               CurrentOrder.TotalCost = numTotalCost.Value;
               CurrentOrder.TrackingNumber = txtTrackingNumber.Text;

               // Convert the selected string back into an OrderStatus enum
               CurrentOrder.Status = cmbStatus.SelectedItem.ToString();

               // 3. Set the form's result to "OK"
               this.DialogResult = DialogResult.OK;

               // 4. Close this form
               this.Close();
          }

          private void btnCancel_Click(object sender, EventArgs e)
          {
               this.DialogResult = DialogResult.Cancel;
               this.Close();
          }
     }
}

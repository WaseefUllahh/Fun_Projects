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
     public partial class ProductForm : Form
     {
          // This public property will hold the product being created
          public Product CurrentProduct { get; set; }
          public ProductForm()
          {
               InitializeComponent();
          }

          private void btnSave_Click(object sender, EventArgs e)
          {
               // 1. Check if we are in "Add Mode"
               if (CurrentProduct == null)
               {
                    // If so, create a new product object
                    CurrentProduct = new Product();
               }

               // 2. Update the product's properties from the controls
               //    (This works for both Add and Edit mode)
               CurrentProduct.ProductName = txtName.Text;
               CurrentProduct.SupplierURL = txtSupplierURL.Text;
               CurrentProduct.SupplierPrice = numSupplierPrice.Value;
               CurrentProduct.ShippingCost = numShippingCost.Value;
               CurrentProduct.TargetSalePrice = numTargetSalePrice.Value;

               // 3. Set the form's result to "OK"
               this.DialogResult = DialogResult.OK;

               // 4. Close this form
               this.Close();
          }

          private void btnCancel_Click(object sender, EventArgs e)
          {
               //set the  form's result to "Cancel"
               this.DialogResult = DialogResult.Cancel;

               //2. Close this form
               this.Close();
          }

          private void ProductForm_Load(object sender, EventArgs e)
          {
               // Check if we are in "Edit Mode" (a product was given to us)
               if (CurrentProduct != null)
               {
                    // Fill the controls with the product's data
                    txtName.Text = CurrentProduct.ProductName;
                    txtSupplierURL.Text = CurrentProduct.SupplierURL;
                    numSupplierPrice.Value = CurrentProduct.SupplierPrice;
                    numShippingCost.Value = CurrentProduct.ShippingCost;
                    numTargetSalePrice.Value = CurrentProduct.TargetSalePrice;

                    // Optional: Change the window title
                    this.Text = "Edit Product";
               }
          }
     }
}

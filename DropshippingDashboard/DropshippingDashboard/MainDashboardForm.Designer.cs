namespace DropshippingDashboard
{
     partial class MainDashboardForm
     {
          /// <summary>
          /// Required designer variable.
          /// </summary>
          private System.ComponentModel.IContainer components = null;

          /// <summary>
          /// Clean up any resources being used.
          /// </summary>
          /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
          protected override void Dispose(bool disposing)
          {
               if (disposing && (components != null))
               {
                    components.Dispose();
               }
               base.Dispose(disposing);
          }

          #region Windows Form Designer generated code

          /// <summary>
          /// Required method for Designer support - do not modify
          /// the contents of this method with the code editor.
          /// </summary>
          private void InitializeComponent()
          {
               this.tabControl1 = new System.Windows.Forms.TabControl();
               this.tabProducts = new System.Windows.Forms.TabPage();
               this.btnLogSale = new System.Windows.Forms.Button();
               this.btnDeleteProduct = new System.Windows.Forms.Button();
               this.btnEditProduct = new System.Windows.Forms.Button();
               this.btnAddProduct = new System.Windows.Forms.Button();
               this.dgvProducts = new System.Windows.Forms.DataGridView();
               this.tabOrders = new System.Windows.Forms.TabPage();
               this.btnDeleteOrder = new System.Windows.Forms.Button();
               this.btnEditOrder = new System.Windows.Forms.Button();
               this.btnAddOrder = new System.Windows.Forms.Button();
               this.dgvOrders = new System.Windows.Forms.DataGridView();
               this.tabDashboard = new System.Windows.Forms.TabPage();
               this.tabControl1.SuspendLayout();
               this.tabProducts.SuspendLayout();
               ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).BeginInit();
               this.tabOrders.SuspendLayout();
               ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).BeginInit();
               this.SuspendLayout();
               // 
               // tabControl1
               // 
               this.tabControl1.Controls.Add(this.tabProducts);
               this.tabControl1.Controls.Add(this.tabOrders);
               this.tabControl1.Controls.Add(this.tabDashboard);
               this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
               this.tabControl1.Location = new System.Drawing.Point(0, 0);
               this.tabControl1.Name = "tabControl1";
               this.tabControl1.SelectedIndex = 0;
               this.tabControl1.Size = new System.Drawing.Size(800, 450);
               this.tabControl1.TabIndex = 0;
               // 
               // tabProducts
               // 
               this.tabProducts.Controls.Add(this.btnLogSale);
               this.tabProducts.Controls.Add(this.btnDeleteProduct);
               this.tabProducts.Controls.Add(this.btnEditProduct);
               this.tabProducts.Controls.Add(this.btnAddProduct);
               this.tabProducts.Controls.Add(this.dgvProducts);
               this.tabProducts.Location = new System.Drawing.Point(4, 25);
               this.tabProducts.Name = "tabProducts";
               this.tabProducts.Padding = new System.Windows.Forms.Padding(3);
               this.tabProducts.Size = new System.Drawing.Size(792, 421);
               this.tabProducts.TabIndex = 0;
               this.tabProducts.Text = "Research Products";
               this.tabProducts.UseVisualStyleBackColor = true;
               // 
               // btnLogSale
               // 
               this.btnLogSale.AutoSize = true;
               this.btnLogSale.Location = new System.Drawing.Point(315, 159);
               this.btnLogSale.Name = "btnLogSale";
               this.btnLogSale.Size = new System.Drawing.Size(75, 26);
               this.btnLogSale.TabIndex = 5;
               this.btnLogSale.Text = "Log Sale";
               this.btnLogSale.UseVisualStyleBackColor = true;
               this.btnLogSale.Click += new System.EventHandler(this.btnLogSale_Click);
               // 
               // btnDeleteProduct
               // 
               this.btnDeleteProduct.AutoSize = true;
               this.btnDeleteProduct.Location = new System.Drawing.Point(203, 160);
               this.btnDeleteProduct.Name = "btnDeleteProduct";
               this.btnDeleteProduct.Size = new System.Drawing.Size(106, 26);
               this.btnDeleteProduct.TabIndex = 3;
               this.btnDeleteProduct.Text = "Delete Product";
               this.btnDeleteProduct.UseVisualStyleBackColor = true;
               this.btnDeleteProduct.Click += new System.EventHandler(this.btnDeleteProduct_Click);
               // 
               // btnEditProduct
               // 
               this.btnEditProduct.AutoSize = true;
               this.btnEditProduct.Location = new System.Drawing.Point(107, 160);
               this.btnEditProduct.Name = "btnEditProduct";
               this.btnEditProduct.Size = new System.Drawing.Size(89, 26);
               this.btnEditProduct.TabIndex = 2;
               this.btnEditProduct.Text = "Edit Product";
               this.btnEditProduct.UseVisualStyleBackColor = true;
               this.btnEditProduct.Click += new System.EventHandler(this.btnEditProduct_Click);
               // 
               // btnAddProduct
               // 
               this.btnAddProduct.AutoSize = true;
               this.btnAddProduct.Location = new System.Drawing.Point(9, 160);
               this.btnAddProduct.Name = "btnAddProduct";
               this.btnAddProduct.Size = new System.Drawing.Size(91, 26);
               this.btnAddProduct.TabIndex = 1;
               this.btnAddProduct.Text = "Add Product";
               this.btnAddProduct.UseVisualStyleBackColor = true;
               this.btnAddProduct.Click += new System.EventHandler(this.btnAddProduct_Click);
               // 
               // dgvProducts
               // 
               this.dgvProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
               this.dgvProducts.Dock = System.Windows.Forms.DockStyle.Top;
               this.dgvProducts.Location = new System.Drawing.Point(3, 3);
               this.dgvProducts.Name = "dgvProducts";
               this.dgvProducts.RowHeadersWidth = 51;
               this.dgvProducts.RowTemplate.Height = 24;
               this.dgvProducts.Size = new System.Drawing.Size(786, 150);
               this.dgvProducts.TabIndex = 0;
               // 
               // tabOrders
               // 
               this.tabOrders.Controls.Add(this.btnDeleteOrder);
               this.tabOrders.Controls.Add(this.btnEditOrder);
               this.tabOrders.Controls.Add(this.btnAddOrder);
               this.tabOrders.Controls.Add(this.dgvOrders);
               this.tabOrders.Location = new System.Drawing.Point(4, 25);
               this.tabOrders.Name = "tabOrders";
               this.tabOrders.Padding = new System.Windows.Forms.Padding(3);
               this.tabOrders.Size = new System.Drawing.Size(792, 421);
               this.tabOrders.TabIndex = 1;
               this.tabOrders.Text = "Order Manangement";
               this.tabOrders.UseVisualStyleBackColor = true;
               // 
               // btnDeleteOrder
               // 
               this.btnDeleteOrder.AutoSize = true;
               this.btnDeleteOrder.Location = new System.Drawing.Point(173, 160);
               this.btnDeleteOrder.Name = "btnDeleteOrder";
               this.btnDeleteOrder.Size = new System.Drawing.Size(94, 26);
               this.btnDeleteOrder.TabIndex = 3;
               this.btnDeleteOrder.Text = "Delete Order";
               this.btnDeleteOrder.UseVisualStyleBackColor = true;
               this.btnDeleteOrder.Click += new System.EventHandler(this.btnDeleteOrder_Click);
               // 
               // btnEditOrder
               // 
               this.btnEditOrder.AutoSize = true;
               this.btnEditOrder.Location = new System.Drawing.Point(91, 160);
               this.btnEditOrder.Name = "btnEditOrder";
               this.btnEditOrder.Size = new System.Drawing.Size(77, 26);
               this.btnEditOrder.TabIndex = 2;
               this.btnEditOrder.Text = "Edit Order";
               this.btnEditOrder.UseVisualStyleBackColor = true;
               this.btnEditOrder.Click += new System.EventHandler(this.btnEditOrder_Click);
               // 
               // btnAddOrder
               // 
               this.btnAddOrder.AutoSize = true;
               this.btnAddOrder.Location = new System.Drawing.Point(9, 160);
               this.btnAddOrder.Name = "btnAddOrder";
               this.btnAddOrder.Size = new System.Drawing.Size(79, 26);
               this.btnAddOrder.TabIndex = 1;
               this.btnAddOrder.Text = "Add Order";
               this.btnAddOrder.UseVisualStyleBackColor = true;
               this.btnAddOrder.Click += new System.EventHandler(this.btnAddOrder_Click);
               // 
               // dgvOrders
               // 
               this.dgvOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
               this.dgvOrders.Dock = System.Windows.Forms.DockStyle.Top;
               this.dgvOrders.Location = new System.Drawing.Point(3, 3);
               this.dgvOrders.Name = "dgvOrders";
               this.dgvOrders.RowHeadersWidth = 51;
               this.dgvOrders.RowTemplate.Height = 24;
               this.dgvOrders.Size = new System.Drawing.Size(786, 150);
               this.dgvOrders.TabIndex = 0;
               // 
               // tabDashboard
               // 
               this.tabDashboard.Location = new System.Drawing.Point(4, 25);
               this.tabDashboard.Name = "tabDashboard";
               this.tabDashboard.Padding = new System.Windows.Forms.Padding(3);
               this.tabDashboard.Size = new System.Drawing.Size(792, 421);
               this.tabDashboard.TabIndex = 2;
               this.tabDashboard.Text = "Dashboard";
               this.tabDashboard.UseVisualStyleBackColor = true;
               // 
               // MainDashboardForm
               // 
               this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
               this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
               this.ClientSize = new System.Drawing.Size(800, 450);
               this.Controls.Add(this.tabControl1);
               this.Name = "MainDashboardForm";
               this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
               this.Text = "Dropshipping Dashboard";
               this.Load += new System.EventHandler(this.MainDashboardForm_Load);
               this.tabControl1.ResumeLayout(false);
               this.tabProducts.ResumeLayout(false);
               this.tabProducts.PerformLayout();
               ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).EndInit();
               this.tabOrders.ResumeLayout(false);
               this.tabOrders.PerformLayout();
               ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).EndInit();
               this.ResumeLayout(false);

          }

          #endregion

          private System.Windows.Forms.TabControl tabControl1;
          private System.Windows.Forms.TabPage tabProducts;
          private System.Windows.Forms.TabPage tabOrders;
          private System.Windows.Forms.DataGridView dgvProducts;
          private System.Windows.Forms.Button btnAddProduct;
          private System.Windows.Forms.Button btnEditProduct;
          private System.Windows.Forms.Button btnDeleteProduct;
          private System.Windows.Forms.DataGridView dgvOrders;
          private System.Windows.Forms.Button btnDeleteOrder;
          private System.Windows.Forms.Button btnEditOrder;
          private System.Windows.Forms.Button btnAddOrder;
          private System.Windows.Forms.Button btnLogSale;
          private System.Windows.Forms.TabPage tabDashboard;
     }
}


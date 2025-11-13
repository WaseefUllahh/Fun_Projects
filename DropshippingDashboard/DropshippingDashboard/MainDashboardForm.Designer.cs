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
               this.panel1 = new System.Windows.Forms.Panel();
               this.label1 = new System.Windows.Forms.Label();
               this.txtSearchProducts = new System.Windows.Forms.TextBox();
               this.dgvProducts = new System.Windows.Forms.DataGridView();
               this.btnLogSale = new System.Windows.Forms.Button();
               this.btnDeleteProduct = new System.Windows.Forms.Button();
               this.btnEditProduct = new System.Windows.Forms.Button();
               this.btnAddProduct = new System.Windows.Forms.Button();
               this.tabOrders = new System.Windows.Forms.TabPage();
               this.btnDeleteOrder = new System.Windows.Forms.Button();
               this.btnEditOrder = new System.Windows.Forms.Button();
               this.btnAddOrder = new System.Windows.Forms.Button();
               this.dgvOrders = new System.Windows.Forms.DataGridView();
               this.tabDashboard = new System.Windows.Forms.TabPage();
               this.lblTotalPotentialProfit = new System.Windows.Forms.Label();
               this.lblTotalActualProfit = new System.Windows.Forms.Label();
               this.tabControl1.SuspendLayout();
               this.tabProducts.SuspendLayout();
               this.panel1.SuspendLayout();
               ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).BeginInit();
               this.tabOrders.SuspendLayout();
               ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).BeginInit();
               this.tabDashboard.SuspendLayout();
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
               this.tabControl1.Size = new System.Drawing.Size(1044, 591);
               this.tabControl1.TabIndex = 0;
               // 
               // tabProducts
               // 
               this.tabProducts.Controls.Add(this.panel1);
               this.tabProducts.Controls.Add(this.btnLogSale);
               this.tabProducts.Controls.Add(this.btnDeleteProduct);
               this.tabProducts.Controls.Add(this.btnEditProduct);
               this.tabProducts.Controls.Add(this.btnAddProduct);
               this.tabProducts.Location = new System.Drawing.Point(4, 25);
               this.tabProducts.Name = "tabProducts";
               this.tabProducts.Padding = new System.Windows.Forms.Padding(3);
               this.tabProducts.Size = new System.Drawing.Size(1036, 562);
               this.tabProducts.TabIndex = 0;
               this.tabProducts.Text = "Research Products";
               this.tabProducts.UseVisualStyleBackColor = true;
               // 
               // panel1
               // 
               this.panel1.Controls.Add(this.label1);
               this.panel1.Controls.Add(this.txtSearchProducts);
               this.panel1.Controls.Add(this.dgvProducts);
               this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
               this.panel1.Location = new System.Drawing.Point(3, 3);
               this.panel1.Name = "panel1";
               this.panel1.Size = new System.Drawing.Size(1030, 248);
               this.panel1.TabIndex = 8;
               // 
               // label1
               // 
               this.label1.AutoSize = true;
               this.label1.Dock = System.Windows.Forms.DockStyle.Top;
               this.label1.Location = new System.Drawing.Point(0, 22);
               this.label1.Name = "label1";
               this.label1.Size = new System.Drawing.Size(50, 16);
               this.label1.TabIndex = 6;
               this.label1.Text = "Search";
               // 
               // txtSearchProducts
               // 
               this.txtSearchProducts.Dock = System.Windows.Forms.DockStyle.Top;
               this.txtSearchProducts.Location = new System.Drawing.Point(0, 0);
               this.txtSearchProducts.Name = "txtSearchProducts";
               this.txtSearchProducts.Size = new System.Drawing.Size(1030, 22);
               this.txtSearchProducts.TabIndex = 7;
               this.txtSearchProducts.TextChanged += new System.EventHandler(this.txtSearchProducts_TextChanged);
               // 
               // dgvProducts
               // 
               this.dgvProducts.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
               this.dgvProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
               this.dgvProducts.Dock = System.Windows.Forms.DockStyle.Fill;
               this.dgvProducts.Location = new System.Drawing.Point(0, 0);
               this.dgvProducts.Name = "dgvProducts";
               this.dgvProducts.RowHeadersWidth = 51;
               this.dgvProducts.RowTemplate.Height = 24;
               this.dgvProducts.Size = new System.Drawing.Size(1030, 248);
               this.dgvProducts.TabIndex = 0;
               this.dgvProducts.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvProducts_CellFormatting);
               // 
               // btnLogSale
               // 
               this.btnLogSale.AutoSize = true;
               this.btnLogSale.Location = new System.Drawing.Point(314, 271);
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
               this.btnDeleteProduct.Location = new System.Drawing.Point(202, 271);
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
               this.btnEditProduct.Location = new System.Drawing.Point(106, 271);
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
               this.btnAddProduct.Location = new System.Drawing.Point(8, 271);
               this.btnAddProduct.Name = "btnAddProduct";
               this.btnAddProduct.Size = new System.Drawing.Size(91, 26);
               this.btnAddProduct.TabIndex = 1;
               this.btnAddProduct.Text = "Add Product";
               this.btnAddProduct.UseVisualStyleBackColor = true;
               this.btnAddProduct.Click += new System.EventHandler(this.btnAddProduct_Click);
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
               this.tabOrders.Size = new System.Drawing.Size(1141, 562);
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
               this.dgvOrders.Size = new System.Drawing.Size(1135, 150);
               this.dgvOrders.TabIndex = 0;
               this.dgvOrders.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvOrders_CellFormatting);
               // 
               // tabDashboard
               // 
               this.tabDashboard.Controls.Add(this.lblTotalPotentialProfit);
               this.tabDashboard.Controls.Add(this.lblTotalActualProfit);
               this.tabDashboard.Location = new System.Drawing.Point(4, 25);
               this.tabDashboard.Name = "tabDashboard";
               this.tabDashboard.Padding = new System.Windows.Forms.Padding(3);
               this.tabDashboard.Size = new System.Drawing.Size(1141, 562);
               this.tabDashboard.TabIndex = 2;
               this.tabDashboard.Text = "Dashboard";
               this.tabDashboard.UseVisualStyleBackColor = true;
               // 
               // lblTotalPotentialProfit
               // 
               this.lblTotalPotentialProfit.AutoSize = true;
               this.lblTotalPotentialProfit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
               this.lblTotalPotentialProfit.Location = new System.Drawing.Point(244, 181);
               this.lblTotalPotentialProfit.Name = "lblTotalPotentialProfit";
               this.lblTotalPotentialProfit.Size = new System.Drawing.Size(245, 25);
               this.lblTotalPotentialProfit.TabIndex = 1;
               this.lblTotalPotentialProfit.Text = "Total Potential Profit: $0.00";
               // 
               // lblTotalActualProfit
               // 
               this.lblTotalActualProfit.AutoSize = true;
               this.lblTotalActualProfit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
               this.lblTotalActualProfit.Location = new System.Drawing.Point(244, 125);
               this.lblTotalActualProfit.Name = "lblTotalActualProfit";
               this.lblTotalActualProfit.Size = new System.Drawing.Size(225, 25);
               this.lblTotalActualProfit.TabIndex = 0;
               this.lblTotalActualProfit.Text = "Total Actual Profit: $0.00";
               // 
               // MainDashboardForm
               // 
               this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
               this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
               this.AutoSize = true;
               this.ClientSize = new System.Drawing.Size(1044, 591);
               this.Controls.Add(this.tabControl1);
               this.Name = "MainDashboardForm";
               this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
               this.Text = "Dropshipping Dashboard";
               this.Load += new System.EventHandler(this.MainDashboardForm_Load);
               this.tabControl1.ResumeLayout(false);
               this.tabProducts.ResumeLayout(false);
               this.tabProducts.PerformLayout();
               this.panel1.ResumeLayout(false);
               this.panel1.PerformLayout();
               ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).EndInit();
               this.tabOrders.ResumeLayout(false);
               this.tabOrders.PerformLayout();
               ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).EndInit();
               this.tabDashboard.ResumeLayout(false);
               this.tabDashboard.PerformLayout();
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
          private System.Windows.Forms.Label lblTotalPotentialProfit;
          private System.Windows.Forms.Label lblTotalActualProfit;
          private System.Windows.Forms.Label label1;
          private System.Windows.Forms.TextBox txtSearchProducts;
          private System.Windows.Forms.Panel panel1;
     }
}


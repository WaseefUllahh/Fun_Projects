namespace DropshippingDashboard
{
     partial class ProductForm
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
               this.ProductName = new System.Windows.Forms.Label();
               this.numSupplierPrice = new System.Windows.Forms.NumericUpDown();
               this.label1 = new System.Windows.Forms.Label();
               this.label2 = new System.Windows.Forms.Label();
               this.label3 = new System.Windows.Forms.Label();
               this.label4 = new System.Windows.Forms.Label();
               this.txtName = new System.Windows.Forms.TextBox();
               this.txtSupplierURL = new System.Windows.Forms.TextBox();
               this.numShippingCost = new System.Windows.Forms.NumericUpDown();
               this.numTargetSalePrice = new System.Windows.Forms.NumericUpDown();
               this.btnSave = new System.Windows.Forms.Button();
               this.btnCancel = new System.Windows.Forms.Button();
               ((System.ComponentModel.ISupportInitialize)(this.numSupplierPrice)).BeginInit();
               ((System.ComponentModel.ISupportInitialize)(this.numShippingCost)).BeginInit();
               ((System.ComponentModel.ISupportInitialize)(this.numTargetSalePrice)).BeginInit();
               this.SuspendLayout();
               // 
               // ProductName
               // 
               this.ProductName.AutoSize = true;
               this.ProductName.Location = new System.Drawing.Point(109, 40);
               this.ProductName.Name = "ProductName";
               this.ProductName.Size = new System.Drawing.Size(96, 16);
               this.ProductName.TabIndex = 0;
               this.ProductName.Text = "Product Name:";
               // 
               // numSupplierPrice
               // 
               this.numSupplierPrice.DecimalPlaces = 2;
               this.numSupplierPrice.Location = new System.Drawing.Point(230, 188);
               this.numSupplierPrice.Name = "numSupplierPrice";
               this.numSupplierPrice.Size = new System.Drawing.Size(120, 22);
               this.numSupplierPrice.TabIndex = 2;
               // 
               // label1
               // 
               this.label1.AutoSize = true;
               this.label1.Location = new System.Drawing.Point(108, 111);
               this.label1.Name = "label1";
               this.label1.Size = new System.Drawing.Size(115, 16);
               this.label1.TabIndex = 3;
               this.label1.Text = "Target Sale Price:";
               // 
               // label2
               // 
               this.label2.AutoSize = true;
               this.label2.Location = new System.Drawing.Point(108, 73);
               this.label2.Name = "label2";
               this.label2.Size = new System.Drawing.Size(90, 16);
               this.label2.TabIndex = 4;
               this.label2.Text = "Supplier URL:";
               // 
               // label3
               // 
               this.label3.AutoSize = true;
               this.label3.Location = new System.Drawing.Point(109, 188);
               this.label3.Name = "label3";
               this.label3.Size = new System.Drawing.Size(94, 16);
               this.label3.TabIndex = 5;
               this.label3.Text = "Supplier Price:";
               // 
               // label4
               // 
               this.label4.AutoSize = true;
               this.label4.Location = new System.Drawing.Point(109, 149);
               this.label4.Name = "label4";
               this.label4.Size = new System.Drawing.Size(93, 16);
               this.label4.TabIndex = 6;
               this.label4.Text = "Shipping Cost:";
               // 
               // txtName
               // 
               this.txtName.Location = new System.Drawing.Point(230, 37);
               this.txtName.Name = "txtName";
               this.txtName.Size = new System.Drawing.Size(120, 22);
               this.txtName.TabIndex = 1;
               // 
               // txtSupplierURL
               // 
               this.txtSupplierURL.Location = new System.Drawing.Point(230, 73);
               this.txtSupplierURL.Name = "txtSupplierURL";
               this.txtSupplierURL.Size = new System.Drawing.Size(120, 22);
               this.txtSupplierURL.TabIndex = 7;
               // 
               // numShippingCost
               // 
               this.numShippingCost.DecimalPlaces = 2;
               this.numShippingCost.Location = new System.Drawing.Point(230, 149);
               this.numShippingCost.Name = "numShippingCost";
               this.numShippingCost.Size = new System.Drawing.Size(120, 22);
               this.numShippingCost.TabIndex = 8;
               // 
               // numTargetSalePrice
               // 
               this.numTargetSalePrice.DecimalPlaces = 2;
               this.numTargetSalePrice.Location = new System.Drawing.Point(230, 111);
               this.numTargetSalePrice.Name = "numTargetSalePrice";
               this.numTargetSalePrice.Size = new System.Drawing.Size(120, 22);
               this.numTargetSalePrice.TabIndex = 9;
               // 
               // btnSave
               // 
               this.btnSave.Location = new System.Drawing.Point(112, 277);
               this.btnSave.Name = "btnSave";
               this.btnSave.Size = new System.Drawing.Size(75, 23);
               this.btnSave.TabIndex = 10;
               this.btnSave.Text = "Save";
               this.btnSave.UseVisualStyleBackColor = true;
               this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
               // 
               // btnCancel
               // 
               this.btnCancel.Location = new System.Drawing.Point(230, 277);
               this.btnCancel.Name = "btnCancel";
               this.btnCancel.Size = new System.Drawing.Size(75, 23);
               this.btnCancel.TabIndex = 11;
               this.btnCancel.Text = "Cancel";
               this.btnCancel.UseVisualStyleBackColor = true;
               this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
               // 
               // ProductForm
               // 
               this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
               this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
               this.ClientSize = new System.Drawing.Size(800, 450);
               this.Controls.Add(this.btnCancel);
               this.Controls.Add(this.btnSave);
               this.Controls.Add(this.numTargetSalePrice);
               this.Controls.Add(this.numShippingCost);
               this.Controls.Add(this.txtSupplierURL);
               this.Controls.Add(this.label4);
               this.Controls.Add(this.label3);
               this.Controls.Add(this.label2);
               this.Controls.Add(this.label1);
               this.Controls.Add(this.numSupplierPrice);
               this.Controls.Add(this.txtName);
               this.Controls.Add(this.ProductName);
               this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
               this.Name = "ProductForm";
               this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
               this.Text = "Product Details";
               this.Load += new System.EventHandler(this.ProductForm_Load);
               ((System.ComponentModel.ISupportInitialize)(this.numSupplierPrice)).EndInit();
               ((System.ComponentModel.ISupportInitialize)(this.numShippingCost)).EndInit();
               ((System.ComponentModel.ISupportInitialize)(this.numTargetSalePrice)).EndInit();
               this.ResumeLayout(false);
               this.PerformLayout();

          }

          #endregion

          private System.Windows.Forms.Label ProductName;
          private System.Windows.Forms.NumericUpDown numSupplierPrice;
          private System.Windows.Forms.Label label1;
          private System.Windows.Forms.Label label2;
          private System.Windows.Forms.Label label3;
          private System.Windows.Forms.Label label4;
          private System.Windows.Forms.TextBox txtName;
          private System.Windows.Forms.TextBox txtSupplierURL;
          private System.Windows.Forms.NumericUpDown numShippingCost;
          private System.Windows.Forms.NumericUpDown numTargetSalePrice;
          private System.Windows.Forms.Button btnSave;
          private System.Windows.Forms.Button btnCancel;
     }
}
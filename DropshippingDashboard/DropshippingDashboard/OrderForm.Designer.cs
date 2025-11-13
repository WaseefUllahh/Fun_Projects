namespace DropshippingDashboard
{
     partial class OrderForm
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
               this.label1 = new System.Windows.Forms.Label();
               this.label2 = new System.Windows.Forms.Label();
               this.label3 = new System.Windows.Forms.Label();
               this.label4 = new System.Windows.Forms.Label();
               this.label5 = new System.Windows.Forms.Label();
               this.label6 = new System.Windows.Forms.Label();
               this.dtpOrderDate = new System.Windows.Forms.DateTimePicker();
               this.txtTrackingNumber = new System.Windows.Forms.TextBox();
               this.txtItemName = new System.Windows.Forms.TextBox();
               this.numSalePrice = new System.Windows.Forms.NumericUpDown();
               this.numTotalCost = new System.Windows.Forms.NumericUpDown();
               this.cmbStatus = new System.Windows.Forms.ComboBox();
               this.btnSave = new System.Windows.Forms.Button();
               this.btnCancel = new System.Windows.Forms.Button();
               ((System.ComponentModel.ISupportInitialize)(this.numSalePrice)).BeginInit();
               ((System.ComponentModel.ISupportInitialize)(this.numTotalCost)).BeginInit();
               this.SuspendLayout();
               // 
               // label1
               // 
               this.label1.AutoSize = true;
               this.label1.Location = new System.Drawing.Point(215, 72);
               this.label1.Name = "label1";
               this.label1.Size = new System.Drawing.Size(76, 16);
               this.label1.TabIndex = 0;
               this.label1.Text = "Order Date:";
               // 
               // label2
               // 
               this.label2.AutoSize = true;
               this.label2.Location = new System.Drawing.Point(216, 103);
               this.label2.Name = "label2";
               this.label2.Size = new System.Drawing.Size(75, 16);
               this.label2.TabIndex = 1;
               this.label2.Text = "Item Name:";
               // 
               // label3
               // 
               this.label3.AutoSize = true;
               this.label3.Location = new System.Drawing.Point(216, 135);
               this.label3.Name = "label3";
               this.label3.Size = new System.Drawing.Size(72, 16);
               this.label3.TabIndex = 2;
               this.label3.Text = "Sale Price:";
               // 
               // label4
               // 
               this.label4.AutoSize = true;
               this.label4.Location = new System.Drawing.Point(216, 170);
               this.label4.Name = "label4";
               this.label4.Size = new System.Drawing.Size(71, 16);
               this.label4.TabIndex = 3;
               this.label4.Text = "Total Cost:";
               // 
               // label5
               // 
               this.label5.AutoSize = true;
               this.label5.Location = new System.Drawing.Point(216, 206);
               this.label5.Name = "label5";
               this.label5.Size = new System.Drawing.Size(47, 16);
               this.label5.TabIndex = 4;
               this.label5.Text = "Status:";
               // 
               // label6
               // 
               this.label6.AutoSize = true;
               this.label6.Location = new System.Drawing.Point(216, 246);
               this.label6.Name = "label6";
               this.label6.Size = new System.Drawing.Size(114, 16);
               this.label6.TabIndex = 5;
               this.label6.Text = "Tracking Number:";
               // 
               // dtpOrderDate
               // 
               this.dtpOrderDate.Location = new System.Drawing.Point(336, 66);
               this.dtpOrderDate.Name = "dtpOrderDate";
               this.dtpOrderDate.Size = new System.Drawing.Size(200, 22);
               this.dtpOrderDate.TabIndex = 6;
               // 
               // txtTrackingNumber
               // 
               this.txtTrackingNumber.Location = new System.Drawing.Point(336, 243);
               this.txtTrackingNumber.Name = "txtTrackingNumber";
               this.txtTrackingNumber.Size = new System.Drawing.Size(100, 22);
               this.txtTrackingNumber.TabIndex = 7;
               // 
               // txtItemName
               // 
               this.txtItemName.Location = new System.Drawing.Point(336, 103);
               this.txtItemName.Name = "txtItemName";
               this.txtItemName.Size = new System.Drawing.Size(100, 22);
               this.txtItemName.TabIndex = 8;
               // 
               // numSalePrice
               // 
               this.numSalePrice.DecimalPlaces = 2;
               this.numSalePrice.Location = new System.Drawing.Point(336, 132);
               this.numSalePrice.Name = "numSalePrice";
               this.numSalePrice.Size = new System.Drawing.Size(120, 22);
               this.numSalePrice.TabIndex = 9;
               // 
               // numTotalCost
               // 
               this.numTotalCost.DecimalPlaces = 2;
               this.numTotalCost.Location = new System.Drawing.Point(336, 168);
               this.numTotalCost.Name = "numTotalCost";
               this.numTotalCost.Size = new System.Drawing.Size(120, 22);
               this.numTotalCost.TabIndex = 10;
               // 
               // cmbStatus
               // 
               this.cmbStatus.FormattingEnabled = true;
               this.cmbStatus.Location = new System.Drawing.Point(335, 206);
               this.cmbStatus.Name = "cmbStatus";
               this.cmbStatus.Size = new System.Drawing.Size(121, 24);
               this.cmbStatus.TabIndex = 11;
               // 
               // btnSave
               // 
               this.btnSave.Location = new System.Drawing.Point(219, 309);
               this.btnSave.Name = "btnSave";
               this.btnSave.Size = new System.Drawing.Size(75, 23);
               this.btnSave.TabIndex = 12;
               this.btnSave.Text = "Save";
               this.btnSave.UseVisualStyleBackColor = true;
               this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
               // 
               // btnCancel
               // 
               this.btnCancel.Location = new System.Drawing.Point(335, 309);
               this.btnCancel.Name = "btnCancel";
               this.btnCancel.Size = new System.Drawing.Size(75, 23);
               this.btnCancel.TabIndex = 13;
               this.btnCancel.Text = "Cancel";
               this.btnCancel.UseVisualStyleBackColor = true;
               this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
               // 
               // OrderForm
               // 
               this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
               this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
               this.ClientSize = new System.Drawing.Size(800, 450);
               this.Controls.Add(this.btnCancel);
               this.Controls.Add(this.btnSave);
               this.Controls.Add(this.cmbStatus);
               this.Controls.Add(this.numTotalCost);
               this.Controls.Add(this.numSalePrice);
               this.Controls.Add(this.txtItemName);
               this.Controls.Add(this.txtTrackingNumber);
               this.Controls.Add(this.dtpOrderDate);
               this.Controls.Add(this.label6);
               this.Controls.Add(this.label5);
               this.Controls.Add(this.label4);
               this.Controls.Add(this.label3);
               this.Controls.Add(this.label2);
               this.Controls.Add(this.label1);
               this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
               this.Name = "OrderForm";
               this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
               this.Text = "Order Details";
               this.Load += new System.EventHandler(this.OrderForm_Load);
               ((System.ComponentModel.ISupportInitialize)(this.numSalePrice)).EndInit();
               ((System.ComponentModel.ISupportInitialize)(this.numTotalCost)).EndInit();
               this.ResumeLayout(false);
               this.PerformLayout();

          }

          #endregion

          private System.Windows.Forms.Label label1;
          private System.Windows.Forms.Label label2;
          private System.Windows.Forms.Label label3;
          private System.Windows.Forms.Label label4;
          private System.Windows.Forms.Label label5;
          private System.Windows.Forms.Label label6;
          private System.Windows.Forms.DateTimePicker dtpOrderDate;
          private System.Windows.Forms.TextBox txtTrackingNumber;
          private System.Windows.Forms.TextBox txtItemName;
          private System.Windows.Forms.NumericUpDown numSalePrice;
          private System.Windows.Forms.NumericUpDown numTotalCost;
          private System.Windows.Forms.ComboBox cmbStatus;
          private System.Windows.Forms.Button btnSave;
          private System.Windows.Forms.Button btnCancel;
     }
}
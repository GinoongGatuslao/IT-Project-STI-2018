namespace WindowsFormsApp1
{
    partial class Search
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
            this.datagrid = new System.Windows.Forms.DataGridView();
            this.nodata_lbl = new System.Windows.Forms.Label();
            this.addprice_btn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.datagrid)).BeginInit();
            this.SuspendLayout();
            // 
            // datagrid
            // 
            this.datagrid.AllowUserToAddRows = false;
            this.datagrid.AllowUserToDeleteRows = false;
            this.datagrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datagrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.datagrid.Location = new System.Drawing.Point(0, 0);
            this.datagrid.Name = "datagrid";
            this.datagrid.ReadOnly = true;
            this.datagrid.RowHeadersVisible = false;
            this.datagrid.Size = new System.Drawing.Size(597, 277);
            this.datagrid.TabIndex = 0;
            this.datagrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.datagrid_CellDoubleClick);
            // 
            // nodata_lbl
            // 
            this.nodata_lbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nodata_lbl.Location = new System.Drawing.Point(0, 0);
            this.nodata_lbl.Name = "nodata_lbl";
            this.nodata_lbl.Size = new System.Drawing.Size(597, 277);
            this.nodata_lbl.TabIndex = 1;
            this.nodata_lbl.Text = "No data found.";
            this.nodata_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.nodata_lbl.Visible = false;
            // 
            // addprice_btn
            // 
            this.addprice_btn.Location = new System.Drawing.Point(487, 242);
            this.addprice_btn.Name = "addprice_btn";
            this.addprice_btn.Size = new System.Drawing.Size(75, 23);
            this.addprice_btn.TabIndex = 2;
            this.addprice_btn.Text = "Add Price";
            this.addprice_btn.UseVisualStyleBackColor = true;
            this.addprice_btn.Visible = false;
            this.addprice_btn.Click += new System.EventHandler(this.addprice_btn_Click);
            // 
            // Search
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(597, 277);
            this.Controls.Add(this.addprice_btn);
            this.Controls.Add(this.datagrid);
            this.Controls.Add(this.nodata_lbl);
            this.MaximizeBox = false;
            this.Name = "Search";
            this.Text = "Item Search";
            this.Load += new System.EventHandler(this.ItemSearch_Load);
            ((System.ComponentModel.ISupportInitialize)(this.datagrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView datagrid;
        private System.Windows.Forms.Label nodata_lbl;
        private System.Windows.Forms.Button addprice_btn;
    }
}
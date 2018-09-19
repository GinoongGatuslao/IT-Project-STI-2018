namespace WindowsFormsApp1
{
    partial class ItemSearch
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
            this.item_data = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.item_data)).BeginInit();
            this.SuspendLayout();
            // 
            // item_data
            // 
            this.item_data.AllowUserToAddRows = false;
            this.item_data.AllowUserToDeleteRows = false;
            this.item_data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.item_data.Dock = System.Windows.Forms.DockStyle.Fill;
            this.item_data.Location = new System.Drawing.Point(0, 0);
            this.item_data.Name = "item_data";
            this.item_data.ReadOnly = true;
            this.item_data.Size = new System.Drawing.Size(597, 277);
            this.item_data.TabIndex = 0;
            this.item_data.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.item_data_CellDoubleClick);
            // 
            // ItemSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(597, 277);
            this.Controls.Add(this.item_data);
            this.MaximizeBox = false;
            this.Name = "ItemSearch";
            this.Text = "Item Search";
            this.Load += new System.EventHandler(this.ItemSearch_Load);
            ((System.ComponentModel.ISupportInitialize)(this.item_data)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView item_data;
    }
}
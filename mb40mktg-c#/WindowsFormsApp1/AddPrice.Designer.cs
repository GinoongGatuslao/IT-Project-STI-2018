namespace WindowsFormsApp1
{
    partial class AddPrice
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
            this.addprice_tb = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pcancel_btn = new System.Windows.Forms.Button();
            this.padd_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Price:";
            // 
            // addprice_tb
            // 
            this.addprice_tb.Location = new System.Drawing.Point(92, 33);
            this.addprice_tb.Name = "addprice_tb";
            this.addprice_tb.Size = new System.Drawing.Size(127, 20);
            this.addprice_tb.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(78, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "P";
            // 
            // pcancel_btn
            // 
            this.pcancel_btn.Location = new System.Drawing.Point(196, 84);
            this.pcancel_btn.Name = "pcancel_btn";
            this.pcancel_btn.Size = new System.Drawing.Size(75, 23);
            this.pcancel_btn.TabIndex = 4;
            this.pcancel_btn.Text = "Cancel";
            this.pcancel_btn.UseVisualStyleBackColor = true;
            this.pcancel_btn.Click += new System.EventHandler(this.pcancel_btn_Click);
            // 
            // padd_btn
            // 
            this.padd_btn.Location = new System.Drawing.Point(115, 84);
            this.padd_btn.Name = "padd_btn";
            this.padd_btn.Size = new System.Drawing.Size(75, 23);
            this.padd_btn.TabIndex = 5;
            this.padd_btn.Text = "Add";
            this.padd_btn.UseVisualStyleBackColor = true;
            this.padd_btn.Click += new System.EventHandler(this.padd_btn_Click);
            // 
            // AddPrice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(283, 119);
            this.Controls.Add(this.padd_btn);
            this.Controls.Add(this.pcancel_btn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.addprice_tb);
            this.Controls.Add(this.label1);
            this.Name = "AddPrice";
            this.Text = "Add Price";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox addprice_tb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button pcancel_btn;
        private System.Windows.Forms.Button padd_btn;
    }
}
namespace WindowsFormsApp1
{
    partial class SummaryPrompt
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
            this.select_lbl = new System.Windows.Forms.Label();
            this.dayPicker = new System.Windows.Forms.DateTimePicker();
            this.ok_btn = new System.Windows.Forms.Button();
            this.week_tb = new System.Windows.Forms.TextBox();
            this.week_lbl = new System.Windows.Forms.Label();
            this.summary_data = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.summary_data)).BeginInit();
            this.SuspendLayout();
            // 
            // select_lbl
            // 
            this.select_lbl.AutoSize = true;
            this.select_lbl.Location = new System.Drawing.Point(25, 27);
            this.select_lbl.Name = "select_lbl";
            this.select_lbl.Size = new System.Drawing.Size(62, 13);
            this.select_lbl.TabIndex = 0;
            this.select_lbl.Text = "Select Day:";
            // 
            // dayPicker
            // 
            this.dayPicker.Location = new System.Drawing.Point(59, 54);
            this.dayPicker.Name = "dayPicker";
            this.dayPicker.Size = new System.Drawing.Size(200, 20);
            this.dayPicker.TabIndex = 1;
            // 
            // ok_btn
            // 
            this.ok_btn.Location = new System.Drawing.Point(232, 101);
            this.ok_btn.Name = "ok_btn";
            this.ok_btn.Size = new System.Drawing.Size(75, 23);
            this.ok_btn.TabIndex = 2;
            this.ok_btn.Text = "OK";
            this.ok_btn.UseVisualStyleBackColor = true;
            this.ok_btn.Click += new System.EventHandler(this.ok_btn_Click);
            // 
            // week_tb
            // 
            this.week_tb.Location = new System.Drawing.Point(113, 54);
            this.week_tb.Name = "week_tb";
            this.week_tb.Size = new System.Drawing.Size(146, 20);
            this.week_tb.TabIndex = 3;
            // 
            // week_lbl
            // 
            this.week_lbl.AutoSize = true;
            this.week_lbl.Location = new System.Drawing.Point(38, 57);
            this.week_lbl.Name = "week_lbl";
            this.week_lbl.Size = new System.Drawing.Size(69, 13);
            this.week_lbl.TabIndex = 4;
            this.week_lbl.Text = "Week (1-52):";
            // 
            // summary_data
            // 
            this.summary_data.AllowUserToAddRows = false;
            this.summary_data.AllowUserToDeleteRows = false;
            this.summary_data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.summary_data.Location = new System.Drawing.Point(15, 7);
            this.summary_data.Name = "summary_data";
            this.summary_data.ReadOnly = true;
            this.summary_data.Size = new System.Drawing.Size(240, 150);
            this.summary_data.TabIndex = 5;
            this.summary_data.Visible = false;
            // 
            // SummaryPrompt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 136);
            this.Controls.Add(this.week_lbl);
            this.Controls.Add(this.week_tb);
            this.Controls.Add(this.ok_btn);
            this.Controls.Add(this.dayPicker);
            this.Controls.Add(this.select_lbl);
            this.Controls.Add(this.summary_data);
            this.Name = "SummaryPrompt";
            this.Text = "Summary of Collections";
            this.Load += new System.EventHandler(this.SummaryPrompt_Load);
            ((System.ComponentModel.ISupportInitialize)(this.summary_data)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label select_lbl;
        private System.Windows.Forms.DateTimePicker dayPicker;
        private System.Windows.Forms.Button ok_btn;
        private System.Windows.Forms.TextBox week_tb;
        private System.Windows.Forms.Label week_lbl;
        private System.Windows.Forms.DataGridView summary_data;
    }
}
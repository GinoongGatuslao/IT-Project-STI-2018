namespace WindowsFormsApp1
{
    partial class Message
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Message));
            this.message_lbl = new System.Windows.Forms.Label();
            this.error_pb = new System.Windows.Forms.PictureBox();
            this.success_pb = new System.Windows.Forms.PictureBox();
            this.ok_btn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.error_pb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.success_pb)).BeginInit();
            this.SuspendLayout();
            // 
            // message_lbl
            // 
            this.message_lbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.message_lbl.Location = new System.Drawing.Point(0, 0);
            this.message_lbl.Name = "message_lbl";
            this.message_lbl.Size = new System.Drawing.Size(295, 113);
            this.message_lbl.TabIndex = 1;
            this.message_lbl.Text = "Account updated successfully.";
            this.message_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // error_pb
            // 
            this.error_pb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.error_pb.Image = ((System.Drawing.Image)(resources.GetObject("error_pb.Image")));
            this.error_pb.Location = new System.Drawing.Point(131, 12);
            this.error_pb.Name = "error_pb";
            this.error_pb.Size = new System.Drawing.Size(37, 36);
            this.error_pb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.error_pb.TabIndex = 0;
            this.error_pb.TabStop = false;
            // 
            // success_pb
            // 
            this.success_pb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.success_pb.Image = ((System.Drawing.Image)(resources.GetObject("success_pb.Image")));
            this.success_pb.Location = new System.Drawing.Point(131, 12);
            this.success_pb.Name = "success_pb";
            this.success_pb.Size = new System.Drawing.Size(37, 36);
            this.success_pb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.success_pb.TabIndex = 0;
            this.success_pb.TabStop = false;
            // 
            // ok_btn
            // 
            this.ok_btn.Location = new System.Drawing.Point(215, 85);
            this.ok_btn.Name = "ok_btn";
            this.ok_btn.Size = new System.Drawing.Size(75, 23);
            this.ok_btn.TabIndex = 2;
            this.ok_btn.Text = "OK";
            this.ok_btn.UseVisualStyleBackColor = true;
            this.ok_btn.Click += new System.EventHandler(this.ok_btn_Click);
            // 
            // Message
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(295, 113);
            this.Controls.Add(this.ok_btn);
            this.Controls.Add(this.success_pb);
            this.Controls.Add(this.message_lbl);
            this.Controls.Add(this.error_pb);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Message";
            this.Text = "MessageDialog";
            this.Load += new System.EventHandler(this.Message_Load);
            ((System.ComponentModel.ISupportInitialize)(this.error_pb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.success_pb)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox success_pb;
        private System.Windows.Forms.Label message_lbl;
        private System.Windows.Forms.PictureBox error_pb;
        private System.Windows.Forms.Button ok_btn;
    }
}
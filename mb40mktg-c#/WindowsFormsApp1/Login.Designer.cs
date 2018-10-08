namespace WindowsFormsApp1
{
    partial class Login
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
            this.app_name_lbl = new System.Windows.Forms.Label();
            this.login_lbl = new System.Windows.Forms.Label();
            this.username_lbl = new System.Windows.Forms.Label();
            this.password_lbl = new System.Windows.Forms.Label();
            this.username_tb = new System.Windows.Forms.TextBox();
            this.password_tb = new System.Windows.Forms.TextBox();
            this.login_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // app_name_lbl
            // 
            this.app_name_lbl.AutoSize = true;
            this.app_name_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.app_name_lbl.Location = new System.Drawing.Point(29, 53);
            this.app_name_lbl.Name = "app_name_lbl";
            this.app_name_lbl.Size = new System.Drawing.Size(264, 39);
            this.app_name_lbl.TabIndex = 0;
            this.app_name_lbl.Text = "MB40 Marketing";
            this.app_name_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.app_name_lbl.UseWaitCursor = true;
            // 
            // login_lbl
            // 
            this.login_lbl.AutoSize = true;
            this.login_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.login_lbl.Location = new System.Drawing.Point(132, 103);
            this.login_lbl.Name = "login_lbl";
            this.login_lbl.Size = new System.Drawing.Size(48, 20);
            this.login_lbl.TabIndex = 1;
            this.login_lbl.Text = "Login";
            this.login_lbl.UseWaitCursor = true;
            // 
            // username_lbl
            // 
            this.username_lbl.AutoSize = true;
            this.username_lbl.Location = new System.Drawing.Point(129, 158);
            this.username_lbl.Name = "username_lbl";
            this.username_lbl.Size = new System.Drawing.Size(55, 13);
            this.username_lbl.TabIndex = 2;
            this.username_lbl.Text = "Username";
            this.username_lbl.UseWaitCursor = true;
            // 
            // password_lbl
            // 
            this.password_lbl.AutoSize = true;
            this.password_lbl.Location = new System.Drawing.Point(127, 214);
            this.password_lbl.Name = "password_lbl";
            this.password_lbl.Size = new System.Drawing.Size(53, 13);
            this.password_lbl.TabIndex = 3;
            this.password_lbl.Text = "Password";
            this.password_lbl.UseWaitCursor = true;
            // 
            // username_tb
            // 
            this.username_tb.AcceptsTab = true;
            this.username_tb.Location = new System.Drawing.Point(72, 174);
            this.username_tb.Name = "username_tb";
            this.username_tb.Size = new System.Drawing.Size(165, 20);
            this.username_tb.TabIndex = 1;
            this.username_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.username_tb.UseWaitCursor = true;
            // 
            // password_tb
            // 
            this.password_tb.Location = new System.Drawing.Point(72, 230);
            this.password_tb.Name = "password_tb";
            this.password_tb.PasswordChar = '*';
            this.password_tb.Size = new System.Drawing.Size(165, 20);
            this.password_tb.TabIndex = 2;
            this.password_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.password_tb.UseWaitCursor = true;
            // 
            // login_btn
            // 
            this.login_btn.Location = new System.Drawing.Point(118, 273);
            this.login_btn.Name = "login_btn";
            this.login_btn.Size = new System.Drawing.Size(75, 23);
            this.login_btn.TabIndex = 4;
            this.login_btn.Text = "Login";
            this.login_btn.UseVisualStyleBackColor = true;
            this.login_btn.UseWaitCursor = true;
            this.login_btn.Click += new System.EventHandler(this.login_btn_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(326, 321);
            this.Controls.Add(this.login_btn);
            this.Controls.Add(this.password_tb);
            this.Controls.Add(this.username_tb);
            this.Controls.Add(this.password_lbl);
            this.Controls.Add(this.username_lbl);
            this.Controls.Add(this.login_lbl);
            this.Controls.Add(this.app_name_lbl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Login";
            this.Text = "Welcome";
            this.UseWaitCursor = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label app_name_lbl;
        private System.Windows.Forms.Label login_lbl;
        private System.Windows.Forms.Label username_lbl;
        private System.Windows.Forms.Label password_lbl;
        private System.Windows.Forms.TextBox username_tb;
        private System.Windows.Forms.TextBox password_tb;
        private System.Windows.Forms.Button login_btn;
    }
}


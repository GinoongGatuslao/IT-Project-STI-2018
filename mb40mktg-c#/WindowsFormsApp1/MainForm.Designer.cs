namespace WindowsFormsApp1
{
    partial class MainForm
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
            this.accountsmgt_btn = new System.Windows.Forms.Button();
            this.inventorymgt_btn = new System.Windows.Forms.Button();
            this.loanmgt_btn = new System.Windows.Forms.Button();
            this.main_panel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.confirmClientInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dashboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addLoanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editLoanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.archiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.accountsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addStaffAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewCollectorAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewClientAccountsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inventoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addItemByBatchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewItemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addStaffAccount_btn = new System.Windows.Forms.Button();
            this.viewCollectors_btn = new System.Windows.Forms.Button();
            this.viewClients_btn = new System.Windows.Forms.Button();
            this.addstaff_panel = new System.Windows.Forms.Panel();
            this.firstname_lbl = new System.Windows.Forms.Label();
            this.firstname_tb = new System.Windows.Forms.TextBox();
            this.lastname_lbl = new System.Windows.Forms.Label();
            this.lastname_tb = new System.Windows.Forms.TextBox();
            this.middlename_lbl = new System.Windows.Forms.Label();
            this.middlename_tb = new System.Windows.Forms.TextBox();
            this.addStaffAccount_lbl = new System.Windows.Forms.Label();
            this.username_lbl = new System.Windows.Forms.Label();
            this.password_lbl = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.viewclients_panel = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.viewcollectors_panel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.main_panel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.addstaff_panel.SuspendLayout();
            this.viewclients_panel.SuspendLayout();
            this.viewcollectors_panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // accountsmgt_btn
            // 
            this.accountsmgt_btn.Font = new System.Drawing.Font("Book Antiqua", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accountsmgt_btn.Location = new System.Drawing.Point(271, 180);
            this.accountsmgt_btn.Name = "accountsmgt_btn";
            this.accountsmgt_btn.Size = new System.Drawing.Size(241, 65);
            this.accountsmgt_btn.TabIndex = 1;
            this.accountsmgt_btn.Text = "Accounts Management";
            this.accountsmgt_btn.UseVisualStyleBackColor = true;
            this.accountsmgt_btn.Click += new System.EventHandler(this.accountsmgt_btn_Click);
            // 
            // inventorymgt_btn
            // 
            this.inventorymgt_btn.Font = new System.Drawing.Font("Book Antiqua", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inventorymgt_btn.Location = new System.Drawing.Point(271, 263);
            this.inventorymgt_btn.Name = "inventorymgt_btn";
            this.inventorymgt_btn.Size = new System.Drawing.Size(241, 65);
            this.inventorymgt_btn.TabIndex = 2;
            this.inventorymgt_btn.Text = "Inventory Management";
            this.inventorymgt_btn.UseVisualStyleBackColor = true;
            this.inventorymgt_btn.Click += new System.EventHandler(this.inventorymgt_btn_Click);
            // 
            // loanmgt_btn
            // 
            this.loanmgt_btn.Font = new System.Drawing.Font("Book Antiqua", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loanmgt_btn.Location = new System.Drawing.Point(271, 98);
            this.loanmgt_btn.Name = "loanmgt_btn";
            this.loanmgt_btn.Size = new System.Drawing.Size(241, 65);
            this.loanmgt_btn.TabIndex = 0;
            this.loanmgt_btn.Text = "Loan Management";
            this.loanmgt_btn.UseVisualStyleBackColor = true;
            this.loanmgt_btn.Click += new System.EventHandler(this.loanmgt_btn_Click);
            // 
            // main_panel
            // 
            this.main_panel.Controls.Add(this.viewcollectors_panel);
            this.main_panel.Controls.Add(this.addstaff_panel);
            this.main_panel.Controls.Add(this.viewclients_panel);
            this.main_panel.Controls.Add(this.panel1);
            this.main_panel.Controls.Add(this.menuStrip1);
            this.main_panel.Location = new System.Drawing.Point(1, 1);
            this.main_panel.Name = "main_panel";
            this.main_panel.Size = new System.Drawing.Size(799, 450);
            this.main_panel.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkGray;
            this.panel1.Controls.Add(this.viewClients_btn);
            this.panel1.Controls.Add(this.viewCollectors_btn);
            this.panel1.Controls.Add(this.addStaffAccount_btn);
            this.panel1.Location = new System.Drawing.Point(3, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 420);
            this.panel1.TabIndex = 1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.loanToolStripMenuItem,
            this.accountsToolStripMenuItem,
            this.inventoryToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(799, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.confirmClientInfoToolStripMenuItem,
            this.dashboardToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // confirmClientInfoToolStripMenuItem
            // 
            this.confirmClientInfoToolStripMenuItem.Name = "confirmClientInfoToolStripMenuItem";
            this.confirmClientInfoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.confirmClientInfoToolStripMenuItem.Text = "Confirm Client Info";
            // 
            // dashboardToolStripMenuItem
            // 
            this.dashboardToolStripMenuItem.Name = "dashboardToolStripMenuItem";
            this.dashboardToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.dashboardToolStripMenuItem.Text = "Dashboard";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // loanToolStripMenuItem
            // 
            this.loanToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addLoanToolStripMenuItem,
            this.editLoanToolStripMenuItem,
            this.archiveToolStripMenuItem});
            this.loanToolStripMenuItem.Name = "loanToolStripMenuItem";
            this.loanToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.loanToolStripMenuItem.Text = "Loan";
            // 
            // addLoanToolStripMenuItem
            // 
            this.addLoanToolStripMenuItem.Name = "addLoanToolStripMenuItem";
            this.addLoanToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.addLoanToolStripMenuItem.Text = "Add Loan";
            // 
            // editLoanToolStripMenuItem
            // 
            this.editLoanToolStripMenuItem.Name = "editLoanToolStripMenuItem";
            this.editLoanToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.editLoanToolStripMenuItem.Text = "View Loan";
            this.editLoanToolStripMenuItem.Click += new System.EventHandler(this.editLoanToolStripMenuItem_Click);
            // 
            // archiveToolStripMenuItem
            // 
            this.archiveToolStripMenuItem.Name = "archiveToolStripMenuItem";
            this.archiveToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.archiveToolStripMenuItem.Text = "Archive";
            // 
            // accountsToolStripMenuItem
            // 
            this.accountsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addStaffAccountToolStripMenuItem,
            this.viewCollectorAccountToolStripMenuItem,
            this.viewClientAccountsToolStripMenuItem});
            this.accountsToolStripMenuItem.Name = "accountsToolStripMenuItem";
            this.accountsToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.accountsToolStripMenuItem.Text = "Accounts";
            // 
            // addStaffAccountToolStripMenuItem
            // 
            this.addStaffAccountToolStripMenuItem.Name = "addStaffAccountToolStripMenuItem";
            this.addStaffAccountToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.addStaffAccountToolStripMenuItem.Text = "Add Staff Account";
            // 
            // viewCollectorAccountToolStripMenuItem
            // 
            this.viewCollectorAccountToolStripMenuItem.Name = "viewCollectorAccountToolStripMenuItem";
            this.viewCollectorAccountToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.viewCollectorAccountToolStripMenuItem.Text = "View Collector Accounts";
            // 
            // viewClientAccountsToolStripMenuItem
            // 
            this.viewClientAccountsToolStripMenuItem.Name = "viewClientAccountsToolStripMenuItem";
            this.viewClientAccountsToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.viewClientAccountsToolStripMenuItem.Text = "View Client Accounts";
            // 
            // inventoryToolStripMenuItem
            // 
            this.inventoryToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addItemToolStripMenuItem,
            this.addItemByBatchToolStripMenuItem,
            this.viewItemsToolStripMenuItem});
            this.inventoryToolStripMenuItem.Name = "inventoryToolStripMenuItem";
            this.inventoryToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.inventoryToolStripMenuItem.Text = "Inventory";
            // 
            // addItemToolStripMenuItem
            // 
            this.addItemToolStripMenuItem.Name = "addItemToolStripMenuItem";
            this.addItemToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.addItemToolStripMenuItem.Text = "Add Item";
            // 
            // addItemByBatchToolStripMenuItem
            // 
            this.addItemByBatchToolStripMenuItem.Name = "addItemByBatchToolStripMenuItem";
            this.addItemByBatchToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.addItemByBatchToolStripMenuItem.Text = "Add Item by Batch";
            // 
            // viewItemsToolStripMenuItem
            // 
            this.viewItemsToolStripMenuItem.Name = "viewItemsToolStripMenuItem";
            this.viewItemsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.viewItemsToolStripMenuItem.Text = "View Items";
            // 
            // addStaffAccount_btn
            // 
            this.addStaffAccount_btn.Location = new System.Drawing.Point(3, 3);
            this.addStaffAccount_btn.Name = "addStaffAccount_btn";
            this.addStaffAccount_btn.Size = new System.Drawing.Size(194, 39);
            this.addStaffAccount_btn.TabIndex = 0;
            this.addStaffAccount_btn.Text = "Add Staff Account";
            this.addStaffAccount_btn.UseVisualStyleBackColor = true;
            this.addStaffAccount_btn.Click += new System.EventHandler(this.addStaffAccount_btn_Click);
            // 
            // viewCollectors_btn
            // 
            this.viewCollectors_btn.Location = new System.Drawing.Point(3, 46);
            this.viewCollectors_btn.Name = "viewCollectors_btn";
            this.viewCollectors_btn.Size = new System.Drawing.Size(194, 39);
            this.viewCollectors_btn.TabIndex = 1;
            this.viewCollectors_btn.Text = "View Collectors Account";
            this.viewCollectors_btn.UseVisualStyleBackColor = true;
            this.viewCollectors_btn.Click += new System.EventHandler(this.viewCollectors_btn_Click);
            // 
            // viewClients_btn
            // 
            this.viewClients_btn.Location = new System.Drawing.Point(3, 90);
            this.viewClients_btn.Name = "viewClients_btn";
            this.viewClients_btn.Size = new System.Drawing.Size(194, 39);
            this.viewClients_btn.TabIndex = 2;
            this.viewClients_btn.Text = "View Clients Account";
            this.viewClients_btn.UseVisualStyleBackColor = true;
            this.viewClients_btn.Click += new System.EventHandler(this.viewClients_btn_Click);
            // 
            // addstaff_panel
            // 
            this.addstaff_panel.BackColor = System.Drawing.Color.Gainsboro;
            this.addstaff_panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.addstaff_panel.Controls.Add(this.password_lbl);
            this.addstaff_panel.Controls.Add(this.username_lbl);
            this.addstaff_panel.Controls.Add(this.addStaffAccount_lbl);
            this.addstaff_panel.Controls.Add(this.middlename_lbl);
            this.addstaff_panel.Controls.Add(this.textBox2);
            this.addstaff_panel.Controls.Add(this.textBox1);
            this.addstaff_panel.Controls.Add(this.lastname_tb);
            this.addstaff_panel.Controls.Add(this.lastname_lbl);
            this.addstaff_panel.Controls.Add(this.middlename_tb);
            this.addstaff_panel.Controls.Add(this.firstname_tb);
            this.addstaff_panel.Controls.Add(this.firstname_lbl);
            this.addstaff_panel.Location = new System.Drawing.Point(209, 27);
            this.addstaff_panel.Name = "addstaff_panel";
            this.addstaff_panel.Size = new System.Drawing.Size(587, 420);
            this.addstaff_panel.TabIndex = 2;
            // 
            // firstname_lbl
            // 
            this.firstname_lbl.AutoSize = true;
            this.firstname_lbl.Location = new System.Drawing.Point(15, 66);
            this.firstname_lbl.Name = "firstname_lbl";
            this.firstname_lbl.Size = new System.Drawing.Size(60, 13);
            this.firstname_lbl.TabIndex = 0;
            this.firstname_lbl.Text = "First Name:";
            // 
            // firstname_tb
            // 
            this.firstname_tb.Location = new System.Drawing.Point(131, 63);
            this.firstname_tb.Name = "firstname_tb";
            this.firstname_tb.Size = new System.Drawing.Size(207, 20);
            this.firstname_tb.TabIndex = 1;
            // 
            // lastname_lbl
            // 
            this.lastname_lbl.AutoSize = true;
            this.lastname_lbl.Location = new System.Drawing.Point(15, 126);
            this.lastname_lbl.Name = "lastname_lbl";
            this.lastname_lbl.Size = new System.Drawing.Size(61, 13);
            this.lastname_lbl.TabIndex = 2;
            this.lastname_lbl.Text = "Last Name:";
            // 
            // lastname_tb
            // 
            this.lastname_tb.Location = new System.Drawing.Point(131, 123);
            this.lastname_tb.Name = "lastname_tb";
            this.lastname_tb.Size = new System.Drawing.Size(207, 20);
            this.lastname_tb.TabIndex = 3;
            // 
            // middlename_lbl
            // 
            this.middlename_lbl.AutoSize = true;
            this.middlename_lbl.Location = new System.Drawing.Point(15, 96);
            this.middlename_lbl.Name = "middlename_lbl";
            this.middlename_lbl.Size = new System.Drawing.Size(72, 13);
            this.middlename_lbl.TabIndex = 4;
            this.middlename_lbl.Text = "Middle Name:";
            // 
            // middlename_tb
            // 
            this.middlename_tb.Location = new System.Drawing.Point(131, 93);
            this.middlename_tb.Name = "middlename_tb";
            this.middlename_tb.Size = new System.Drawing.Size(207, 20);
            this.middlename_tb.TabIndex = 1;
            // 
            // addStaffAccount_lbl
            // 
            this.addStaffAccount_lbl.AutoSize = true;
            this.addStaffAccount_lbl.Font = new System.Drawing.Font("Bookman Old Style", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addStaffAccount_lbl.Location = new System.Drawing.Point(189, 9);
            this.addStaffAccount_lbl.Name = "addStaffAccount_lbl";
            this.addStaffAccount_lbl.Size = new System.Drawing.Size(230, 22);
            this.addStaffAccount_lbl.TabIndex = 5;
            this.addStaffAccount_lbl.Text = "ADD STAFF ACCOUNT";
            // 
            // username_lbl
            // 
            this.username_lbl.AutoSize = true;
            this.username_lbl.Location = new System.Drawing.Point(15, 155);
            this.username_lbl.Name = "username_lbl";
            this.username_lbl.Size = new System.Drawing.Size(58, 13);
            this.username_lbl.TabIndex = 6;
            this.username_lbl.Text = "Username:";
            // 
            // password_lbl
            // 
            this.password_lbl.AutoSize = true;
            this.password_lbl.Location = new System.Drawing.Point(15, 184);
            this.password_lbl.Name = "password_lbl";
            this.password_lbl.Size = new System.Drawing.Size(109, 13);
            this.password_lbl.TabIndex = 7;
            this.password_lbl.Text = "Generated Password:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(131, 152);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(207, 20);
            this.textBox1.TabIndex = 3;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(131, 181);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(207, 20);
            this.textBox2.TabIndex = 3;
            // 
            // viewclients_panel
            // 
            this.viewclients_panel.BackColor = System.Drawing.Color.Gainsboro;
            this.viewclients_panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.viewclients_panel.Controls.Add(this.label3);
            this.viewclients_panel.Location = new System.Drawing.Point(209, 27);
            this.viewclients_panel.Name = "viewclients_panel";
            this.viewclients_panel.Size = new System.Drawing.Size(587, 420);
            this.viewclients_panel.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Bookman Old Style", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(148, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(264, 22);
            this.label3.TabIndex = 5;
            this.label3.Text = "VIEW CLIENTS ACCOUNT";
            // 
            // viewcollectors_panel
            // 
            this.viewcollectors_panel.BackColor = System.Drawing.Color.Gainsboro;
            this.viewcollectors_panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.viewcollectors_panel.Controls.Add(this.label1);
            this.viewcollectors_panel.Location = new System.Drawing.Point(209, 27);
            this.viewcollectors_panel.Name = "viewcollectors_panel";
            this.viewcollectors_panel.Size = new System.Drawing.Size(587, 420);
            this.viewcollectors_panel.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(148, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(315, 22);
            this.label1.TabIndex = 5;
            this.label1.Text = "VIEW COLLECTORS ACCOUNT";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.main_panel);
            this.Controls.Add(this.inventorymgt_btn);
            this.Controls.Add(this.loanmgt_btn);
            this.Controls.Add(this.accountsmgt_btn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Dashboard";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.main_panel.ResumeLayout(false);
            this.main_panel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.addstaff_panel.ResumeLayout(false);
            this.addstaff_panel.PerformLayout();
            this.viewclients_panel.ResumeLayout(false);
            this.viewclients_panel.PerformLayout();
            this.viewcollectors_panel.ResumeLayout(false);
            this.viewcollectors_panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button accountsmgt_btn;
        private System.Windows.Forms.Button inventorymgt_btn;
        private System.Windows.Forms.Button loanmgt_btn;
        private System.Windows.Forms.Panel main_panel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem confirmClientInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dashboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem accountsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inventoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addLoanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editLoanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem archiveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addStaffAccountToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewCollectorAccountToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewClientAccountsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addItemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addItemByBatchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewItemsToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel addstaff_panel;
        private System.Windows.Forms.TextBox lastname_tb;
        private System.Windows.Forms.Label lastname_lbl;
        private System.Windows.Forms.TextBox firstname_tb;
        private System.Windows.Forms.Label firstname_lbl;
        private System.Windows.Forms.Button viewClients_btn;
        private System.Windows.Forms.Button viewCollectors_btn;
        private System.Windows.Forms.Button addStaffAccount_btn;
        private System.Windows.Forms.Label middlename_lbl;
        private System.Windows.Forms.Label addStaffAccount_lbl;
        private System.Windows.Forms.TextBox middlename_tb;
        private System.Windows.Forms.Label password_lbl;
        private System.Windows.Forms.Label username_lbl;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel viewclients_panel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel viewcollectors_panel;
        private System.Windows.Forms.Label label1;
    }
}
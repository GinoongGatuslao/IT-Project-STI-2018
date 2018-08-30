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
            this.main_panel.SuspendLayout();
            this.menuStrip1.SuspendLayout();
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
            this.main_panel.Controls.Add(this.menuStrip1);
            this.main_panel.Location = new System.Drawing.Point(1, 1);
            this.main_panel.Name = "main_panel";
            this.main_panel.Size = new System.Drawing.Size(799, 450);
            this.main_panel.TabIndex = 3;
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
            this.confirmClientInfoToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.confirmClientInfoToolStripMenuItem.Text = "Confirm Client Info";
            // 
            // dashboardToolStripMenuItem
            // 
            this.dashboardToolStripMenuItem.Name = "dashboardToolStripMenuItem";
            this.dashboardToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.dashboardToolStripMenuItem.Text = "Dashboard";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(173, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
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
            this.addLoanToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.addLoanToolStripMenuItem.Text = "Add Loan";
            // 
            // editLoanToolStripMenuItem
            // 
            this.editLoanToolStripMenuItem.Name = "editLoanToolStripMenuItem";
            this.editLoanToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.editLoanToolStripMenuItem.Text = "View Loan";
            this.editLoanToolStripMenuItem.Click += new System.EventHandler(this.editLoanToolStripMenuItem_Click);
            // 
            // archiveToolStripMenuItem
            // 
            this.archiveToolStripMenuItem.Name = "archiveToolStripMenuItem";
            this.archiveToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
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
            this.addItemToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.addItemToolStripMenuItem.Text = "Add Item";
            // 
            // addItemByBatchToolStripMenuItem
            // 
            this.addItemByBatchToolStripMenuItem.Name = "addItemByBatchToolStripMenuItem";
            this.addItemByBatchToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.addItemByBatchToolStripMenuItem.Text = "Add Item by Batch";
            // 
            // viewItemsToolStripMenuItem
            // 
            this.viewItemsToolStripMenuItem.Name = "viewItemsToolStripMenuItem";
            this.viewItemsToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.viewItemsToolStripMenuItem.Text = "View Items";
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
            this.main_panel.ResumeLayout(false);
            this.main_panel.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
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
    }
}
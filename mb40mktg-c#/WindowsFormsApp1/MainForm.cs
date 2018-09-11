using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class MainForm : Form
    {
        List<Panel> rightPanel = new List<Panel>();
        List<Panel> leftPanel = new List<Panel>();
        List<TextBox> profileTbs = new List<TextBox>();
       
        public MainForm()
        {
            InitializeComponent();
            main_panel.Visible = false;

        }

        private void loanmgt_btn_Click(object sender, EventArgs e)
        {
            this.Text = "Loan Management";
            this.Refresh();
            main_panel.Visible = true;
            addLoanToolStripMenuItem_Click(sender, e);
        }

        private void accountsmgt_btn_Click(object sender, EventArgs e)
        {
            this.Text = "Account Management";
            this.Refresh();
            main_panel.Visible = true;
        }

        private void inventorymgt_btn_Click(object sender, EventArgs e)
        {
            this.Text = "Transaction Management";
            this.Refresh();
            main_panel.Visible = true;
        }

        private void editLoanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rightPanel[5].BringToFront();
            leftPanel[1].BringToFront();
            loansfilter_gb.Enabled = true;
            search_gb.Enabled = true;
        }

        private void addStaffAccount_btn_Click(object sender, EventArgs e)
        {
            rightPanel[0].BringToFront();
        }

        private void viewCollectors_btn_Click(object sender, EventArgs e)
        {
            rightPanel[1].BringToFront();
        }

        private void viewClients_btn_Click(object sender, EventArgs e)
        {
            rightPanel[2].BringToFront();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            rightPanel.Add(addstaff_panel); //0
            rightPanel.Add(viewcollectors_panel); //1
            rightPanel.Add(viewclients_panel); //2
            rightPanel.Add(profile_panel); //3
            rightPanel.Add(addloan_panel); //4
            rightPanel.Add(viewloan_panel); //5
            rightPanel[0].BringToFront();

            leftPanel.Add(profile_sidepanel); 
            leftPanel.Add(loan_sidepanel);
            leftPanel[0].BringToFront();

            profileTbs.Add(prof_fn_tb);
            profileTbs.Add(prof_mn_tb);
            profileTbs.Add(prof_ln_tb);
            profileTbs.Add(prof_gender_tb);
            profileTbs.Add(prof_cn_tb);
            profileTbs.Add(prof_address_tb);
        }

        private void dashboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Text = "Dashboard";
            this.Refresh();
            main_panel.Visible = false;
        }

        private void profileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rightPanel[3].BringToFront();
            leftPanel[0].BringToFront();
        }

        private void edit_profile_btn_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < profileTbs.Count; i++)
            {
                profileTbs[i].Enabled = true;
                prof_bdate_picker.Enabled = true;
                prof_upload_btn.Enabled = true;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addLoanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rightPanel[4].BringToFront();
            leftPanel[1].BringToFront();
            loansfilter_gb.Enabled = false;
            search_gb.Enabled = false;
        }

        private void addloan_btn_Click(object sender, EventArgs e)
        {
            addLoanToolStripMenuItem_Click(sender, e);
        }

        private void viewloan_btn_Click(object sender, EventArgs e)
        {
            editLoanToolStripMenuItem_Click(sender, e);
        }
    }
}

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
    
        public static class Constants
        {
            public static Form mainForm = new MainForm();
        }
       
        public MainForm()
        {
            InitializeComponent();
            main_panel.Visible = false;
        }

        private void loanmgt_btn_Click(object sender, EventArgs e)
        {
            Constants.mainForm.Text = "Loan Management";
            main_panel.Visible = true;
        }

        private void accountsmgt_btn_Click(object sender, EventArgs e)
        {
            Constants.mainForm.Text = "Accounts Management";
            main_panel.Visible = true;
        }

        private void inventorymgt_btn_Click(object sender, EventArgs e)
        {
            Constants.mainForm.Text = "Inventory Management";
            main_panel.Visible = true;
        }

        private void editLoanToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}

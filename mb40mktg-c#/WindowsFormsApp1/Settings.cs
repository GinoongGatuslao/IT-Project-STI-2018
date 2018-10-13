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
    public partial class Settings : Form
    {
        public static string baseUrl = "http://localhost:8000";

        public Settings()
        {
            InitializeComponent();
        }

        private void ok_btn_Click(object sender, EventArgs e)
        {
            if (baseurl_lbl.Text != string.Empty)
            {
                baseUrl = baseurl_tb.Text;

                this.Hide();
                Login login = new Login();
                login.Closed += (s, args) => this.Close();
                login.Show();
            }
        }

        private void cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

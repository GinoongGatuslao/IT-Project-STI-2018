using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace WindowsFormsApp1
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void login_btn_Click(object sender, EventArgs e)
        {
            /**RestClient restClient = new RestClient();
            restClient.endPoint = "https://05bb7b95.ngrok.io/api/login?"
                + "username=" + username_tb.Text
                + "password=" + password_tb.Text;

            Debug.WriteLine("Rest Client Created.");

            string response = string.Empty;

            response = restClient.LoginRequest();

            Debug.WriteLine(response.ToString());**/

            if (username_tb.Text != string.Empty && password_tb.Text != string.Empty)
            {
                RestClient restClient = new RestClient();
                restClient.endPoint = "https://05bb7b95.ngrok.io/api/login?"
                    + "username=" + username_tb.Text
                    + "&password=" + password_tb.Text;

                Debug.WriteLine("Rest Client Created");

                string response = string.Empty;
                response = restClient.LoginRequest();
                string[] res = response.Split('|');

                if (res[0].Equals("OK"))
                {
                    this.Hide();
                    var dashboard = new MainForm();
                    dashboard.Closed += (s, args) => this.Close();
                    dashboard.Show();

                    Debug.WriteLine(res[0].ToString() + "\n" + res[1].ToString());
                } else
                {
                    //error message
                    Debug.WriteLine("error login");
                }
            } else
            {
                //add error dialog
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}

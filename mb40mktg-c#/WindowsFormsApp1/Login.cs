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
            //uncomment on final
            /**if (username_tb.Text != string.Empty && password_tb.Text != string.Empty)
            {
                RestClient restClient = new RestClient();
                restClient.endPoint = "https://4cd9a60f.ngrok.io/api/login?"
                    + "username=" + username_tb.Text
                    + "&password=" + password_tb.Text;

                Debug.WriteLine("Rest Client Created");

                string response = string.Empty;
                response = restClient.PostRequest();
                string[] res = response.Split('|');

                if (res[0].Equals("OK"))
                {**/
                    this.Hide();
                    var dashboard = new MainForm();
                    dashboard.Closed += (s, args) => this.Close();
                    dashboard.Show();
            /**
                    Debug.WriteLine(res[0].ToString() + "\n" + res[1].ToString());
                } else
                {
                    Debug.WriteLine("error login");
                    MessageBox.Show("Invalid username or password.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } else
            {
                MessageBox.Show("Username and password cannot be empty", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

            }**/
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}

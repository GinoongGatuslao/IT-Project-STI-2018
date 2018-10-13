using System;
using System.Windows.Forms;
using System.Diagnostics;
using Newtonsoft.Json;

namespace WindowsFormsApp1
{
    public partial class Login : Form
    {
        public static string api_token = string.Empty;
        public static int id = 0;
        public static int user_type = 0;

        public Login()
        {
            InitializeComponent();
        }

        private void login_btn_Click(object sender, EventArgs e)
        {
            if (username_tb.Text != string.Empty && password_tb.Text != string.Empty)
            {
                Cursor.Current = Cursors.WaitCursor;
                RestClient restClient = new RestClient();
                restClient.endPoint = Settings.baseUrl.ToString()
                    + "/api/login?";

                restClient.postJSON = "{\"password\": \"" + password_tb.Text
                    + "\",\"username\":\"" + username_tb.Text
                    + "\"}";

                restClient.login = true;
                    
                Debug.WriteLine("Rest Client Created\n" + restClient.postJSON + "\n" + restClient.endPoint);

                string response = string.Empty;
                response = restClient.PostRequest();
                Console.WriteLine("response : " + response);
                string[] res = response.Split('|');

                if (res[0].Equals("OK"))
                {
                    User user = JsonConvert.DeserializeObject<User>(res[1]);
                    api_token = user.api_token;
                    user_type = user.user_type;
                    id = user.id;
                    Console.WriteLine("api_token: " + user.api_token);

                    this.Hide();
                    MainForm dashboard = new MainForm();
                    dashboard.Closed += (s, args) => this.Close();
                    dashboard.Show();
            
                    Debug.WriteLine(res[0].ToString() + "\n" + res[1].ToString());
                } else
                {
                    Debug.WriteLine("error login");
                    MessageBox.Show("Invalid username or password.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Cursor.Current = Cursors.Default;
            } else
            {
                MessageBox.Show("Username and password cannot be empty", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

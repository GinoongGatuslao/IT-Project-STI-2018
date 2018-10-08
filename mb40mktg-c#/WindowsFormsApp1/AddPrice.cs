using Newtonsoft.Json;
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
    public partial class AddPrice : Form
    {
        public static bool cancel = false;

        public AddPrice()
        {
            InitializeComponent();
        }

        private void pcancel_btn_Click(object sender, EventArgs e)
        {
            cancel = true;
            this.Close();
        }

        private void padd_btn_Click(object sender, EventArgs e)
        {
            cancel = false;
            Cursor.Current = Cursors.WaitCursor;
            RestClient restClient = new RestClient();
            restClient.endPoint = Settings.baseUrl
                + "/api/price/addprice";

            Price price = new Price();
            price.price = Convert.ToInt32(addprice_tb.Text.ToString());

            string response = string.Empty;
            string jsonStr = string.Empty;

            jsonStr = JsonConvert.SerializeObject(price);
            restClient.postJSON = jsonStr;
            restClient.login = false;
            response = restClient.PostRequest();
            Console.WriteLine(response);
            Cursor.Current = Cursors.Default;
            this.Close();
        }
    }
}

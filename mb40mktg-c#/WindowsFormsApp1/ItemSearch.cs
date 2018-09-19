using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace WindowsFormsApp1
{
    public partial class ItemSearch : Form
    {
        public static int itemId = 0;
        public static string itemName = string.Empty;
        public static string itemDesc = string.Empty;

        public ItemSearch()
        {
            InitializeComponent();
        }

        private void ItemSearch_Load(object sender, EventArgs e)
        {
            RestClient restClient = new RestClient();
            restClient.endPoint = "https://6adc3c1b.ngrok.io"
                + "/api/product/getitems";

            string response = string.Empty;
            response = restClient.GetRequest();
            Console.WriteLine("response : " + response);
           
            var products = JsonConvert.DeserializeObject<List<Products>>(response);

            item_data.DataSource = products;
           
        }

        private void item_data_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            itemId = Convert.ToInt32(item_data.SelectedCells[0].Value);
            itemName = item_data.SelectedCells[1].Value.ToString();
            itemDesc = item_data.SelectedCells[7].Value.ToString();
        }
    }
}

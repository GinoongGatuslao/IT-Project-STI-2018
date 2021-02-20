using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace WindowsFormsApp1
{
    public partial class Search : Form
    {
        public string tag = string.Empty;
        public static int itemId = 0;
        public static string itemName = string.Empty;
        public static string itemDesc = string.Empty;
        public static double itemPrice = 0;
        public static int itemStockCount = 0;
        public static string prof_fullname = string.Empty;
        public static int prof_id = 0;
        public static string prof_add = string.Empty;
        public static string prof_cn = string.Empty;
        public static double prof_cred_limit = 0;
        public static string prof_stat = string.Empty;
        public static double prof_cred_rem = 0;
        public static int price_id = 0;
        public static double price = 0;
        public List<Products> products = new List<Products>();
        public List<Profile> profiles = new List<Profile>();
        public List<Price> prices = new List<Price>();

        public Search()
        {
            InitializeComponent();
        }

        private void ItemSearch_Load(object sender, EventArgs e)
        {
            RestClient restClient = new RestClient();

            Cursor.Current = Cursors.WaitCursor;
            switch (tag.ToString())
            {
                case "ITEM":
                case "ITEM_BATCH":
                    {
                        search_lbl.Text = "Search by item name: ";
                        this.Text = "Item Search";
                        addprice_btn.Visible = false;
                        restClient.endPoint = Settings.baseUrl.ToString()
                            + "/api/product/getitems";

                        string response = string.Empty;
                        response = restClient.GetRequest();

                        Console.WriteLine("response : " + response);

                        if (!response.Equals("[]"))
                        {
                            nodata_lbl.Visible = false;
                            products = JsonConvert.DeserializeObject<List<Products>>(response);

                            datagrid.DataSource = products;
                            format_itemdata();
                        } else
                        {
                            nodata_lbl.Visible = true;
                        }
                        break;
                    }
                case "CLIENT_ID":
                case "CLIENT":
                    {
                        search_lbl.Text = "Search by client's last name:";
                        this.Text = "Client Search";
                        addprice_btn.Visible = false;
                        restClient.endPoint = Settings.baseUrl.ToString()
                            + "/api/profile";

                        string response = string.Empty;
                        response = restClient.GetRequest();

                        Console.WriteLine("response : " + response);

                        if (!response.Equals("[]"))
                        {
                            nodata_lbl.Visible = false;
                            profiles = JsonConvert.DeserializeObject<List<Profile>>(response);
                            List<Profile> prof = new List<Profile>();

                            foreach (Profile p in profiles)
                            {
                                if (p.user_type == 3) //for clients only
                                {
                                    prof.Add(p);
                                }
                            }

                            datagrid.DataSource = prof;
                            format_clientdata();
                        }
                        else
                        {
                            nodata_lbl.Visible = true;
                        }

                        break;
                    }
                case "PRICE":
                    {
                        search_lbl.Text = "Search by price:";
                        this.Text = "Price Search";
                        addprice_btn.Visible = true;
                        restClient.endPoint = Settings.baseUrl
                            + "/api/price/getpricelist";

                        string response = string.Empty;
                        response = restClient.GetRequest();
                        Console.WriteLine(response);

                        if (!response.Equals("[]"))
                        {
                            nodata_lbl.Visible = false;
                            prices = JsonConvert.DeserializeObject<List<Price>>(response);

                            datagrid.DataSource = prices;
                            format_price();
                        } else
                        {
                            nodata_lbl.Visible = true;
                        }

                        break;
                    }
            }
            Cursor.Current = Cursors.Default;
        }

        private void datagrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (datagrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                switch (tag.ToString())
                {
                    case "ITEM":
                        if (Convert.ToInt32(datagrid.Rows[e.RowIndex].Cells["stock_count"].Value.ToString()) >= 1)
                        {
                            datagrid.CurrentRow.Selected = true;
                            itemId = Convert.ToInt32(datagrid.Rows[e.RowIndex].Cells["id"].Value.ToString());
                            itemName = datagrid.Rows[e.RowIndex].Cells["item_name"].Value.ToString();
                            itemDesc = datagrid.Rows[e.RowIndex].Cells["description"].Value.ToString();
                            itemPrice = Convert.ToDouble(datagrid.Rows[e.RowIndex].Cells["price"].Value.ToString());
                            itemStockCount = Convert.ToInt32(datagrid.Rows[e.RowIndex].Cells["stock_count"].Value.ToString());
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Item is out of stock.", "Adding Item",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }
                        break;
                    case "ITEM_BATCH":
                        datagrid.CurrentRow.Selected = true;
                        itemId = Convert.ToInt32(datagrid.Rows[e.RowIndex].Cells["id"].Value.ToString());
                        itemName = datagrid.Rows[e.RowIndex].Cells["item_name"].Value.ToString();
                        itemStockCount = Convert.ToInt32(datagrid.Rows[e.RowIndex].Cells["stock_count"].Value.ToString());
                        this.Close();
                        break;
                    case "CLIENT_ID":
                    case "CLIENT":
                        if (datagrid.Rows[e.RowIndex].Cells["verified"].Value.ToString().Equals("1"))
                        {
                            datagrid.CurrentRow.Selected = true;
                            prof_id = Convert.ToInt32(datagrid.Rows[e.RowIndex].Cells["id"].Value.ToString());
                            prof_fullname = datagrid.Rows[e.RowIndex].Cells["first_name"].Value.ToString()
                                + " " + datagrid.Rows[e.RowIndex].Cells["middle_name"].Value.ToString()
                                + " " + datagrid.Rows[e.RowIndex].Cells["last_name"].Value.ToString();
                            prof_cn = datagrid.Rows[e.RowIndex].Cells["contact_num"].Value.ToString();
                            prof_cred_limit = Convert.ToDouble(datagrid.Rows[e.RowIndex].Cells["credit_limit"].Value.ToString());
                            prof_add = datagrid.Rows[e.RowIndex].Cells["address"].Value.ToString();
                            prof_stat = datagrid.Rows[e.RowIndex].Cells["verified_str"].Value.ToString();
                            prof_cred_rem = Convert.ToDouble(datagrid.Rows[e.RowIndex].Cells["credit_rem"].Value.ToString());
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Account is not yet confirmed.", "Account Information",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }
                        break;
                    case "PRICE":
                        datagrid.CurrentRow.Selected = true;
                        price_id = Convert.ToInt32(datagrid.Rows[e.RowIndex].Cells["id"].Value.ToString());
                        price = Convert.ToDouble(datagrid.Rows[e.RowIndex].Cells["price"].Value.ToString());
                        this.Close();
                        break;
                }
            }
        }

        private void addprice_btn_Click(object sender, EventArgs e)
        {
            AddPrice addPrice = new AddPrice();
            addPrice.Show();

            addPrice.FormClosed += new FormClosedEventHandler(addPrice_FormClosed);
        }

        private void addPrice_FormClosed(object sender, EventArgs e)
        {
            if (!AddPrice.cancel)
            {
                tag = "PRICE";
                ItemSearch_Load(sender, e);
            }
        }

        private void format_itemdata()
        {
            datagrid.Columns[0].HeaderText = "Item ID";
            datagrid.Columns[1].HeaderText = "Item Name";
            datagrid.Columns[2].HeaderText = "Price";
            datagrid.Columns[3].HeaderText = "Stock Count";
            datagrid.Columns[4].HeaderText = "Repossessed";
            datagrid.Columns[5].HeaderText = "Damaged";
            datagrid.Columns[6].HeaderText = "Reorder Point";
            datagrid.Columns[7].HeaderText = "Description";

            datagrid.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            datagrid.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            datagrid.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            datagrid.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            datagrid.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            datagrid.Columns[2].DefaultCellStyle.Format = "N1";
        }

        private void format_clientdata()
        {
            datagrid.Columns[0].HeaderText = "Client ID";
            datagrid.Columns[2].HeaderText = "First Name";
            datagrid.Columns[3].HeaderText = "Middle Name";
            datagrid.Columns[4].HeaderText = "Last Name";
            datagrid.Columns[5].HeaderText = "Address";
            datagrid.Columns[6].HeaderText = "Contact No";
            datagrid.Columns[13].HeaderText = "Credit Limit";
            datagrid.Columns[16].HeaderText = "User Type";
            datagrid.Columns[19].HeaderText = "Status";

            datagrid.Columns[1].Visible = false;
            datagrid.Columns[7].Visible = false;
            datagrid.Columns[8].Visible = false;
            datagrid.Columns[9].Visible = false;
            datagrid.Columns[10].Visible = false;
            datagrid.Columns[11].Visible = false;
            datagrid.Columns[12].Visible = false;
            datagrid.Columns[14].Visible = false;
            datagrid.Columns[15].Visible = false;
            datagrid.Columns[16].Visible = false;
            datagrid.Columns[17].Visible = false;
            datagrid.Columns[18].Visible = false;
            datagrid.Columns[20].Visible = false;
            datagrid.Columns[13].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            datagrid.Columns[13].DefaultCellStyle.Format = "N1";
        }

        private void format_price()
        {
            datagrid.Columns[0].HeaderText = "ID";
            datagrid.Columns[1].HeaderText = "Price";
            datagrid.Columns[1].DefaultCellStyle.Format = "N1";
        }

        private void search_tb_KeyUp(object sender, KeyEventArgs e)
        {
            switch (tag)
            {
                case "ITEM":
                case "ITEM_BATCH":
                    datagrid.DataSource = null;
                    List<Products> prod = new List<Products>();
                    foreach (Products p in products)
                    {
                        CultureInfo culture = new CultureInfo("es-ES", false);
                        if (culture.CompareInfo.IndexOf(p.item_name, search_tb.Text.ToString(), CompareOptions.IgnoreCase) >= 0)
                        {
                            prod.Add(p);
                        }
                    }
                    datagrid.DataSource = prod;
                    format_itemdata();
                    break;
                case "CLIENT":
                    datagrid.DataSource = null;
                    List<Profile> prof = new List<Profile>();
                    foreach (Profile p in profiles)
                    {
                        CultureInfo culture = new CultureInfo("es-ES", false);
                        if (culture.CompareInfo.IndexOf(p.last_name, search_tb.Text.ToString(), CompareOptions.IgnoreCase) >= 0)
                        {
                            prof.Add(p);
                        }
                    }
                    datagrid.DataSource = prof;
                    format_clientdata();
                    break;
                case "PRICE":
                    datagrid.DataSource = null;
                    List<Price> price = new List<Price>();
                    foreach (Price p in prices)
                    {
                        CultureInfo culture = new CultureInfo("es-ES", false);
                        if (culture.CompareInfo.IndexOf(p.price.ToString(), search_tb.Text.ToString(), CompareOptions.IgnoreCase) >= 0)
                        {
                            price.Add(p);
                        }
                    }
                    datagrid.DataSource = price;
                    format_price();
                    break;
            }
        }
    }
}
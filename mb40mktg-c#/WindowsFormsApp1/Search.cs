using System;
using System.Collections.Generic;
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
        public static string itemPrice = string.Empty;
        public static string prof_fullname = string.Empty;
        public static int prof_id = 0;
        public static string prof_add = string.Empty;
        public static string prof_cn = string.Empty;
        public static double prof_cred_limit = 0;
        public static int prof_stat = 0;

        public Search()
        {
            InitializeComponent();
        }

        private void ItemSearch_Load(object sender, EventArgs e)
        {
            RestClient restClient = new RestClient();

            switch (tag.ToString())
            {
                case "ITEM":
                    {
                        this.Text = "Item Search";
                        restClient.endPoint = Settings.baseUrl.ToString()
                            + "/api/product/getitems";

                        string response = string.Empty;
                        response = restClient.GetRequest();

                        Console.WriteLine("response : " + response);

                        if (!response.Equals("[]"))
                        {
                            nodata_lbl.Visible = false;
                            var products = JsonConvert.DeserializeObject<List<Products>>(response);

                            datagrid.DataSource = products;

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
                        } else
                        {
                            nodata_lbl.Visible = true;
                        }
                        break;
                    }
                case "CLIENT":
                    {
                        this.Text = "Client Search";
                        restClient.endPoint = Settings.baseUrl.ToString()
                            + "/api/profile";

                        string response = string.Empty;
                        response = restClient.GetRequest();

                        Console.WriteLine("response : " + response);

                        if (!response.Equals("[]"))
                        {
                            nodata_lbl.Visible = false;
                            var profile = JsonConvert.DeserializeObject<List<Profile>>(response);
                            List<Profile> prof = new List<Profile>();

                            foreach (Profile p in profile)
                            {
                                if (p.user_type == 3) //for clients only
                                {
                                    prof.Add(p);
                                }
                            }

                            datagrid.DataSource = prof;

                            datagrid.Columns[0].HeaderText = "Client ID";
                            datagrid.Columns[1].HeaderText = "User ID";
                            datagrid.Columns[2].HeaderText = "First Name";
                            datagrid.Columns[3].HeaderText = "Middle Name";
                            datagrid.Columns[4].HeaderText = "Last Name";
                            datagrid.Columns[5].HeaderText = "Address";
                            datagrid.Columns[6].HeaderText = "Contact No";
                            datagrid.Columns[7].HeaderText = "Birthdate";
                            datagrid.Columns[8].HeaderText = "Occupation";
                            datagrid.Columns[9].HeaderText = "Montly Income";
                            datagrid.Columns[10].HeaderText = "Montly Expense";
                            datagrid.Columns[11].HeaderText = "ID Path";
                            datagrid.Columns[12].HeaderText = "Sketch Path";
                            datagrid.Columns[13].HeaderText = "Credit Limit";
                            datagrid.Columns[14].HeaderText = "Status";
                            datagrid.Columns[15].HeaderText = "Username";
                            datagrid.Columns[16].HeaderText = "User Type";

                            datagrid.Columns[1].Visible = false;
                            datagrid.Columns[7].Visible = false;
                            datagrid.Columns[8].Visible = false;
                            datagrid.Columns[9].Visible = false;
                            datagrid.Columns[10].Visible = false;
                            datagrid.Columns[11].Visible = false;
                            datagrid.Columns[12].Visible = false;
                            datagrid.Columns[15].Visible = false;
                            datagrid.Columns[16].Visible = false;
                            datagrid.Columns[13].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            datagrid.Columns[13].DefaultCellStyle.Format = "N1";
                        }
                        else
                        {
                            nodata_lbl.Visible = true;
                        }

                        break;
                    }
            }
        }

        private void datagrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (datagrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                switch (tag.ToString())
                {
                    case "ITEM":
                        {
                            datagrid.CurrentRow.Selected = true;
                            itemId = Convert.ToInt32(datagrid.Rows[e.RowIndex].Cells["id"].Value.ToString());
                            itemName = datagrid.Rows[e.RowIndex].Cells["item_name"].Value.ToString();
                            itemDesc = datagrid.Rows[e.RowIndex].Cells["description"].Value.ToString();
                            itemPrice = datagrid.Rows[e.RowIndex].Cells["price"].Value.ToString();

                            break;
                        }
                    case "CLIENT":
                        {
                            Console.WriteLine("hello world");

                            datagrid.CurrentRow.Selected = true;
                            prof_id = Convert.ToInt32(datagrid.Rows[e.RowIndex].Cells["id"].Value.ToString());
                            prof_fullname = datagrid.Rows[e.RowIndex].Cells["first_name"].Value.ToString()
                                + " " + datagrid.Rows[e.RowIndex].Cells["middle_name"].Value.ToString()
                                + " " + datagrid.Rows[e.RowIndex].Cells["last_name"].Value.ToString();
                            prof_cn = datagrid.Rows[e.RowIndex].Cells["contact_num"].Value.ToString();
                            prof_cred_limit = Convert.ToDouble(datagrid.Rows[e.RowIndex].Cells["credit_limit"].Value.ToString());
                            prof_add = datagrid.Rows[e.RowIndex].Cells["address"].Value.ToString();
                            prof_stat = Convert.ToInt32(datagrid.Rows[e.RowIndex].Cells["account_status"].Value.ToString());

                            break;
                        }
                }
            }

            this.Close();
        }
    }
}

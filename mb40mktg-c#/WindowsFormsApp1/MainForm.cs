using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WindowsFormsApp1;

namespace WindowsFormsApp1
{
    public partial class MainForm : Form
    {
        private List<Panel> rightPanel = new List<Panel>();
        private List<Panel> leftPanel = new List<Panel>();
        private List<TextBox> profileTbs = new List<TextBox>();
        private int item_index = 0;
        private int search_index = 0;
        private Loan loan = new Loan();
        private RestClient restClient = new RestClient();
        private string tag = String.Empty;
        private StatusList statuslist;
        private List<Loan> loans;

        public MainForm()
        {
            InitializeComponent();
            main_panel.Visible = false;

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            rightPanel.Add(addstaff_panel); //0
            rightPanel.Add(viewcollectors_panel); //1
            rightPanel.Add(viewclients_panel); //2
            rightPanel.Add(profile_panel); //3
            rightPanel.Add(addloan_panel); //4
            rightPanel.Add(viewloan_panel); //5
            rightPanel.Add(confirmclient_panel); //6
            rightPanel[0].BringToFront();

            leftPanel.Add(profile_sidepanel); //0
            leftPanel.Add(loan_sidepanel); //1
            leftPanel.Add(confirmclient_sidepanel); //2
            leftPanel[0].BringToFront();

            profileTbs.Add(prof_fn_tb);
            profileTbs.Add(prof_mn_tb);
            profileTbs.Add(prof_ln_tb);
            profileTbs.Add(prof_gender_tb);
            profileTbs.Add(prof_cn_tb);
            profileTbs.Add(prof_address_tb);

            restClient.endPoint = Settings.baseUrl.ToString()
                + "/api/loanstatus";

            string response = string.Empty;
            response = restClient.GetRequest();

            var loanTypes = JsonConvert.DeserializeObject<LoanType>(response);
            for (int i = 0; i < loanTypes.types.Length; i++)
            {
                loan_status_cb.Items.Add(loanTypes.types[i]);
            }

            restClient.endPoint = Settings.baseUrl.ToString()
                + "/api/statuslist";

            string res = string.Empty;
            res = restClient.GetRequest();

            statuslist = JsonConvert.DeserializeObject<StatusList>(res);
            for (int j = 0; j < statuslist.status.Length; j++)
            {
                item_stat_cb.Items.Add(statuslist.status[j]);
            }
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
            confirmClientAccountToolStripMenuItem_Click(sender, e);
        }

        private void inventorymgt_btn_Click(object sender, EventArgs e)
        {
            this.Text = "Transaction Management";
            this.Refresh();
            main_panel.Visible = true;
        }

        private void editLoanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            rightPanel[5].BringToFront();
            leftPanel[1].BringToFront();
            loansfilter_gb.Enabled = true;
            search_gb.Enabled = true;

            restClient.endPoint = Settings.baseUrl
                + "/api/loan/getloans";

            string response = string.Empty;
            response = restClient.GetRequest();
            Console.WriteLine("response : " + response);

            loans = JsonConvert.DeserializeObject<List<Loan>>(response);

            if (loans.Count != 0)
            {
                loan_data.DataSource = loans;
                no_data_lbl.Visible = false;
            }
            else
            {
                no_data_lbl.Visible = true;
            }
            Cursor.Current = Cursors.Default;
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
            for (int i = 0; i < profileTbs.Count; i++)
            {
                profileTbs[i].Clear();
                profileTbs[i].Enabled = false;
            }
            prof_bdate_picker.Enabled = false;
            prof_upload_btn.Enabled = false;
            save_btn.Enabled = false;
            cancel_btn.Enabled = false;
        }

        private void edit_profile_btn_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < profileTbs.Count; i++)
            {
                profileTbs[i].Enabled = true;
            }
            prof_bdate_picker.Enabled = true;
            prof_upload_btn.Enabled = true;
            save_btn.Enabled = true;
            cancel_btn.Enabled = true;
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

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.Show();
        }

        private void confirmClientAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rightPanel[6].BringToFront();
            leftPanel[2].BringToFront();

            accounts_lbl.Text = "CONFIRM CLIENT ACCOUNT";
            up_photo_btn.Visible = false;
            up_sketch_btn.Visible = false;
        }

        private void save_loan_btn_Click(object sender, EventArgs e)
        {
            //TODO: add error catching for empty fields and wrong input
            loan.account_id = Int32.Parse(account_id_tb.Text);
            loan.term_length = Int32.Parse(length_tb.Text);
            loan.status = loan_status_cb.SelectedIndex;
            loan.loan_value = Convert.ToDouble(total_amount_tb.Text.ToString());
            loan.amortization = Convert.ToDouble(amortization_tb.Text.ToString());
            loan.loan_items = new List<LoanItem>();

            int index = 0;
            foreach (Control c in loan_items_fp.Controls)
            {
                if (loan_items_fp.Controls.ContainsKey("loanitem" + Convert.ToString(index)))
                {
                    GroupBox gb = (GroupBox)loan_items_fp.Controls[index];

                    TextBox id_tb = (TextBox)gb.Controls["item_id_tb"];
                    ComboBox stat_cb = (ComboBox)gb.Controls["item_stat_cb"];
                    TextBox interest_tb = (TextBox)gb.Controls["item_int_tb"];

                    LoanItem loanItem = new LoanItem();
                    loanItem.item_id = Convert.ToInt32(id_tb.Text);
                    loanItem.item_status = item_stat_cb.SelectedIndex;
                    loanItem.interest = Convert.ToDouble(interest_tb.Text);
                    loan.loan_items.Add(loanItem);
                }
                index++;
            }

            restClient.endPoint = Settings.baseUrl.ToString()
                + "/api/loan/addloan";

            string jsonResult = JsonConvert.SerializeObject(loan);
            Console.WriteLine(jsonResult.ToString());

            restClient.postJSON = jsonResult;
            restClient.login = false;

            string response = string.Empty;
            response = restClient.PostRequest();
            Console.WriteLine("response : " + response);
        }

        private void add_item_btn_Click(object sender, EventArgs e)
        {
            GroupBox gb = new GroupBox();
            gb.Size = new Size(550, 114);
            gb.Name = "loanitem" + Convert.ToString(++item_index);
            gb.Text = "Loan Item " + (item_index + 1);

            Label item_id_lbl = new Label();
            item_id_lbl.Text = "Item ID:";
            item_id_lbl.Location = new Point(15, 19);
            item_id_lbl.Size = new Size(63, 13);
            gb.Controls.Add(item_id_lbl);

            Label item_name_lbl = new Label();
            item_name_lbl.Text = "Item Name:";
            item_name_lbl.Location = new Point(15, 46);
            item_name_lbl.Size = new Size(63, 13);
            gb.Controls.Add(item_name_lbl);

            Label item_desc_lbl = new Label();
            item_desc_lbl.Text = "Description:";
            item_desc_lbl.Location = new Point(15, 71);
            item_desc_lbl.Size = new Size(63, 13);
            gb.Controls.Add(item_desc_lbl);

            Label item_stat_lbl = new Label();
            item_stat_lbl.Text = "Status:";
            item_stat_lbl.Location = new Point(370, 19);
            item_stat_lbl.Size = new Size(45, 13);
            gb.Controls.Add(item_stat_lbl);

            Label item_price_lbl = new Label();
            item_price_lbl.Text = "Price:";
            item_price_lbl.Location = new Point(370, 46);
            item_price_lbl.Size = new Size(34, 13);
            gb.Controls.Add(item_price_lbl);

            Label item_interest_lbl = new Label();
            item_interest_lbl.Text = "Interest:";
            item_interest_lbl.Location = new Point(370, 71);
            item_interest_lbl.Size = new Size(45, 13);
            gb.Controls.Add(item_interest_lbl);

            Label p = new Label();
            p.Text = "P";
            p.Location = new Point(410, 46);
            p.Size = new Size(14, 13);
            gb.Controls.Add(p);

            Label percent = new Label();
            percent.Text = "%";
            percent.Location = new Point(517, 71);
            percent.Size = new Size(15, 13);
            gb.Controls.Add(percent);

            TextBox item_id_tb = new TextBox();
            item_id_tb.Size = new Size(247, 20);
            item_id_tb.Location = new Point(81, 16);
            item_id_tb.Name = "item_id_tb";
            gb.Controls.Add(item_id_tb);

            TextBox item_name_tb = new TextBox();
            item_name_tb.Size = new Size(247, 20);
            item_name_tb.Location = new Point(81, 42);
            item_name_tb.Name = "item_name_tb";
            gb.Controls.Add(item_name_tb);

            TextBox item_desc_tb = new TextBox();
            item_desc_tb.Size = new Size(247, 39);
            item_desc_tb.Location = new Point(81, 67);
            item_desc_tb.Multiline = true;
            item_desc_tb.Name = "item_desc_tb";
            gb.Controls.Add(item_desc_tb);

            ComboBox item_stat_cb = new ComboBox();
            item_stat_cb.Size = new Size(93, 21);
            item_stat_cb.Location = new Point(424, 16);
            item_stat_cb.Name = "item_stat_cb";
            for (int j = 0; j < statuslist.status.Length; j++)
            {
                item_stat_cb.Items.Add(statuslist.status[j]);
            }
            gb.Controls.Add(item_stat_cb);

            TextBox item_price_tb = new TextBox();
            item_price_tb.Size = new Size(93, 20);
            item_price_tb.Location = new Point(424, 43);
            item_price_tb.TextAlign = HorizontalAlignment.Right;
            item_price_tb.Enabled = false;
            item_price_tb.Name = "item_price_tb";
            item_price_tb.TextChanged += new EventHandler(item_price_tb_TextChanged);
            gb.Controls.Add(item_price_tb);

            TextBox item_int_tb = new TextBox();
            item_int_tb.Size = new Size(93, 20);
            item_int_tb.Location = new Point(424, 68);
            item_int_tb.TextAlign = HorizontalAlignment.Right;
            item_int_tb.Name = "item_int_tb";
            item_int_tb.TextChanged += new EventHandler(item_int_tb_TextChanged);
            gb.Controls.Add(item_int_tb);

            Button search_btn = new Button();
            search_btn.Size = new Size(25, 25);
            search_btn.Location = new Point(332, 14);
            search_btn.Name = "search_item" + item_index;
            search_btn.Click += new EventHandler(search_item0_Click);
            //search_btn.Image = new Bitmap("search(1).png");
            gb.Controls.Add(search_btn);

            loan_items_fp.Controls.Add(gb);
        }

        private void cancel_loan_btn_Click(object sender, EventArgs e)
        {
            addLoanToolStripMenuItem_Click(sender, e);
        }

        private void Search_FormClosed(object sender, FormClosedEventArgs e)
        {
            switch (tag.ToString())
            {
                case "ITEM":
                    {
                        foreach (Control c in loan_items_fp.Controls)
                        {
                            if (loan_items_fp.Controls.ContainsKey("loanitem" + Convert.ToString(search_index)))
                            {
                                GroupBox gb = (GroupBox)loan_items_fp.Controls[search_index];

                                TextBox id_tb = (TextBox)gb.Controls["item_id_tb"];
                                TextBox name_tb = (TextBox)gb.Controls["item_name_tb"];
                                TextBox desc_tb = (TextBox)gb.Controls["item_desc_tb"];
                                TextBox price_tb = (TextBox)gb.Controls["item_price_tb"];

                                id_tb.Text = Search.itemId.ToString();
                                name_tb.Text = Search.itemName.ToString();
                                desc_tb.Text = Search.itemDesc.ToString();
                                price_tb.Text = Search.itemPrice.ToString();
                            }
                        }
                        break;
                    }
                case "CLIENT":
                    {
                        Console.WriteLine("hello im here");

                        client_name_tb.Text = Search.prof_fullname;
                        address_tb.Text = Search.prof_add;
                        account_id_tb.Text = Search.prof_id.ToString();
                        contactnum_tb.Text = Search.prof_cn;
                        cred_lmt_tb.Text = Search.prof_cred_limit.ToString();

                        if (Search.prof_stat == 1)
                        {
                            account_stat_lbl.Text = "This account is confirmed.";
                            account_stat_lbl.ForeColor = Color.Green;
                            loan_items_fp.Enabled = true;
                            add_item_btn.Enabled = true;
                            save_loan_btn.Enabled = true;
                            cancel_loan_btn.Enabled = true;
                        } else
                        {
                            account_stat_lbl.Text = "This account is not yet confirmed.";
                            account_stat_lbl.ForeColor = Color.Red;
                            loan_items_fp.Enabled = false;
                            add_item_btn.Enabled = false;
                            save_loan_btn.Enabled = false;
                            cancel_loan_btn.Enabled = false;
                        }

                        break;
                    }
            }
        }

        private void search_item0_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            search_index = Convert.ToInt32(btn.Name.ToString().Substring((btn.Name.IndexOf('m') + 1),
                (btn.Name.Length - btn.Name.IndexOf('m') - 1)));

            Cursor.Current = Cursors.WaitCursor;
            Search search = new Search();
            search.tag = "ITEM";
            search.Show();
            tag = "ITEM";
            Cursor.Current = Cursors.Default;

            search.FormClosed += new FormClosedEventHandler(Search_FormClosed);
        }

        private void client_search_btn_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Search search = new Search();
            search.tag = "CLIENT";
            search.Show();
            tag = "CLIENT";
            Cursor.Current = Cursors.Default;

            search.FormClosed += new FormClosedEventHandler(Search_FormClosed);
        }

        private void create_acc_btn_Click(object sender, EventArgs e)
        {
            createClientAccountToolStripMenuItem_Click(sender, e);
        }

        private void confirm_btn_Click(object sender, EventArgs e)
        {
            confirmClientAccountToolStripMenuItem_Click(sender, e);
        }

        private void createClientAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rightPanel[6].BringToFront();
            leftPanel[2].BringToFront();

            accounts_lbl.Text = "CREATE CLIENT ACCOUNT";
            up_sketch_btn.Visible = true;
            up_photo_btn.Visible = true;
        }

        private void item_price_tb_TextChanged(object sender, EventArgs e)
        {
            double total_amount = 0;
            int index = 0;
            foreach (Control c in loan_items_fp.Controls)
            {
                if (loan_items_fp.Controls.ContainsKey("loanitem" + Convert.ToString(index)))
                {
                    GroupBox gb = (GroupBox)loan_items_fp.Controls[index];
                    TextBox price_tb = (TextBox)gb.Controls["item_price_tb"];
                    TextBox int_tb = (TextBox)gb.Controls["item_int_tb"];

                    total_amount += (Convert.ToDouble(price_tb.Text.ToString()) *
                        (1 + Convert.ToDouble(int_tb.Text.ToString())));
                }
                index++;
            }
            
            total_amount_tb.Text = total_amount.ToString();
        }

        private void total_amount_tb_TextChanged(object sender, EventArgs e)
        {
            amortization_tb.Text = (Convert.ToDouble(total_amount_tb.Text.ToString()) /
                Convert.ToInt32(length_tb.Text.ToString())).ToString("N1");
        }

        private void length_tb_TextChanged(object sender, EventArgs e)
        {
            amortization_tb.Text = (Convert.ToDouble(total_amount_tb.Text.ToString()) /
                Convert.ToInt32(length_tb.Text.ToString())).ToString("N1");
        }

        private void item_int_tb_TextChanged(object sender, EventArgs e)
        {
            item_price_tb_TextChanged(sender, e);
        }

        private void create_stf_btn_Click(object sender, EventArgs e)
        {
            rightPanel[6].BringToFront();
            leftPanel[2].BringToFront();

            accounts_lbl.Text = "CREATE STAFF ACCOUNT";
            up_sketch_btn.Visible = true;
            up_photo_btn.Visible = true;
        }

        private void active_rb_CheckedChanged(object sender, EventArgs e)
        {
            if (active_rb.Checked)
            {
                List<Loan> newList = new List<Loan>();
                foreach (Loan l in loans)
                {
                    if (l.status == 1)
                    {
                        newList.Add(l);
                    }
                }
                
                if (newList.Count != 0)
                {
                    loan_data.DataSource = newList;
                    no_data_lbl.Visible = false;
                } else
                {
                    no_data_lbl.Visible = true;
                }
                
            }
        }

        private void inactive_rb_CheckedChanged(object sender, EventArgs e)
        {
            if (inactive_rb.Checked)
            {
                List<Loan> newList = new List<Loan>();
                foreach (Loan l in loans)
                {
                    if (l.status == 0)
                    {
                        newList.Add(l);
                    }
                }

                if (newList.Count != 0)
                {
                    loan_data.DataSource = newList;
                    no_data_lbl.Visible = false;
                }
                else
                {
                    no_data_lbl.Visible = true;
                }
            }
        }

        private void completed_rb_CheckedChanged(object sender, EventArgs e)
        {
            if (completed_rb.Checked)
            {
                List<Loan> newList = new List<Loan>();
                foreach (Loan l in loans)
                {
                    if (l.status == 2)
                    {
                        newList.Add(l);
                    }
                }

                if (newList.Count != 0)
                {
                    loan_data.DataSource = newList;
                    no_data_lbl.Visible = false;
                }
                else
                {
                    no_data_lbl.Visible = true;
                }
            }
        }

        private void all_rb_CheckedChanged(object sender, EventArgs e)
        {
            if (loans.Count != 0)
            {
                loan_data.DataSource = loans;
                no_data_lbl.Visible = false;
            }
            else
            {
                no_data_lbl.Visible = true;
            }
        }
    }
}
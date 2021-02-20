using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsApp1;
using Microsoft.Office.Interop.Excel;
using GroupBox = System.Windows.Forms.GroupBox;
using TextBox = System.Windows.Forms.TextBox;
using Label = System.Windows.Forms.Label;
using Point = System.Drawing.Point;
using Button = System.Windows.Forms.Button;

namespace WindowsFormsApp1
{
    public partial class MainForm : Form
    {
        private List<Panel> rightPanel = new List<Panel>();
        private List<Panel> leftPanel = new List<Panel>();
        private int item_index = 0;
        private int search_index = 0;
        private int batch_index = 0;
        private Loan loan = new Loan();
        private LoanType loanTypes;
        private RestClient restClient = new RestClient();
        private string tag = String.Empty;
        private StatusList statuslist;
        private UserType userTypes;
        private List<Loan> loans;
        private List<Profile> profiles;
        private List<Item> items;
        private List<Transaction> transactions;
        private static Random random = new Random();
        private bool edit = false;
        private int selected_loanid = 0;
        private int selected_profileid = 0;
        private int selected_userId = 0;
        private int current_userId = 0;
        private int selected_itemId = 0;
        private int selected_priceId = 0;

        public MainForm()
        {
            InitializeComponent();
            main_panel.Visible = false;

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            dashboard_panel.Visible = true;
            main_panel.Visible = false;
            rightPanel.Add(create_staff_panel); //0
            rightPanel.Add(view_acc_panel); //1
            rightPanel.Add(confirmclient_panel); //2
            rightPanel.Add(profile_panel); //3
            rightPanel.Add(addloan_panel); //4
            rightPanel.Add(viewloan_panel); //5
            rightPanel.Add(additem_panel); //6
            rightPanel.Add(batch_panel); //7
            rightPanel.Add(view_item_panel); //8
            rightPanel.Add(trans_panel); //9
            rightPanel[0].BringToFront();

            leftPanel.Add(profile_sidepanel); //0
            leftPanel.Add(loan_sidepanel); //1
            leftPanel.Add(confirmclient_sidepanel); //2
            leftPanel.Add(inventory_sidepanel); //3
            leftPanel.Add(trans_sidepanel); //4
            leftPanel[0].BringToFront();

            //try
            //{
                string response = string.Empty;
            //    Cursor.Current = Cursors.WaitCursor;
            //    restClient.endPoint = Settings.baseUrl.ToString()
            //        + "/api/loanstatus";
            //    response = restClient.GetRequest();
            //
            //    loanTypes = JsonConvert.DeserializeObject<LoanType>(response);
            //    for (int i = 0; i < loanTypes.types.Length; i++)
            //    {
            //        loan_status_cb.Items.Add(loanTypes.types[i]);
            //    }
            //    loan_status_cb.SelectedIndex = 1;
            //
            //    restClient.endPoint = Settings.baseUrl.ToString()
            //        + "/api/statuslist";
            //    response = restClient.GetRequest();
            //
            //    statuslist = JsonConvert.DeserializeObject<StatusList>(response);
            //    for (int i = 0; i < statuslist.status.Length; i++)
            //    {
            //        item_stat_cb.Items.Add(statuslist.status[i]);
            //    }
            //    item_stat_cb.SelectedIndex = 0;
            //
            //    restClient.endPoint = Settings.baseUrl
            //        + "/api/usertypes";
            //    response = restClient.GetRequest();
            //
            //    userTypes = JsonConvert.DeserializeObject<UserType>(response);
            //    stype_tb.Items.Add(userTypes.types[1]);
            //    stype_tb.Items.Add(userTypes.types[2]);
            //    stype_tb.SelectedIndex = 0;
            //} catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message.ToString());
            //}
            //Cursor.Current = Cursors.Default;
            loan_status_cb.Items.Add("Inactive");
            loan_status_cb.Items.Add("Active");
            loan_status_cb.Items.Add("Completed");
            loan_status_cb.SelectedIndex = 1;

            item_stat_cb.Items.Add("In Stock");
            item_stat_cb.Items.Add("Repossessed");
            item_stat_cb.Items.Add("Damaged");
            item_stat_cb.SelectedIndex = 0;

            stype_tb.Items.Add("Staff");
            stype_tb.Items.Add("Collector");
            stype_tb.SelectedIndex = 0;

            mo_inc_cb.Items.Add("P1000.00 or Less");
            mo_inc_cb.Items.Add("P1001.00 - P3000.00");
            mo_inc_cb.Items.Add("P3001.00 - P6000.00");
            mo_inc_cb.Items.Add("P6001.00 - P10000.00");
            mo_inc_cb.Items.Add("P10001.00 - P15000.00");
            mo_inc_cb.Items.Add("P15001.00 - P25000.00");
            mo_inc_cb.Items.Add("P25001.00 and up");
            mo_inc_cb.SelectedIndex = 0;

            mo_exp_cb.Items.Add("P1000.00 or Less");
            mo_exp_cb.Items.Add("P1001.00 - P3000.00");
            mo_exp_cb.Items.Add("P3001.00 - P6000.00");
            mo_exp_cb.Items.Add("P6001.00 - P10000.00");
            mo_exp_cb.Items.Add("P10001.00 - P15000.00");
            mo_exp_cb.Items.Add("P15001.00 - P25000.00");
            mo_exp_cb.Items.Add("P25001.00 and up");
            mo_exp_cb.SelectedIndex = 0;
        }

        /**
         * DASHBOARD MENU
         **/
        private void loanmgt_btn_Click(object sender, EventArgs e)
        {
            this.Text = "Loan Management";
            this.Refresh();
            main_panel.Visible = true;
            dashboard_panel.Visible = false;
            addLoanToolStripMenuItem_Click(sender, e);
        }

        private void accountsmgt_btn_Click(object sender, EventArgs e)
        {
            this.Text = "Account Management";
            this.Refresh();
            main_panel.Visible = true;
            dashboard_panel.Visible = false;
            createClientAccountToolStripMenuItem_Click(sender, e);

            if (Login.user_type == 0)
            {
                create_stf_btn.Enabled = true;
            } else
            {
                create_stf_btn.Enabled = false;
            }
        }

        private void inventorymgt_btn_Click(object sender, EventArgs e)
        {
            this.Text = "Inventory Management";
            this.Refresh();
            main_panel.Visible = true;
            dashboard_panel.Visible = false;
            addItemToolStripMenuItem_Click(sender, e);
        }

        private void transaction_btn_Click(object sender, EventArgs e)
        {
            this.Text = "Transaction";
            this.Refresh();
            main_panel.Visible = true;
            dashboard_panel.Visible = false;
            collectionToolStripMenuItem_Click(sender, e);
        }

        private void reports_btn_Click(object sender, EventArgs e)
        {
            this.Text = "Reports";
            this.Refresh();
            main_panel.Visible = true;
            dashboard_panel.Visible = false;
            reports_panel.BringToFront();
        }

        /**
         * TOOLSTRIP MENU
         **/
        private void dashboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Text = "Dashboard";
            this.Refresh();
            main_panel.Visible = false;
            dashboard_panel.Visible = true;
        }

        private void profileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rightPanel[3].BringToFront();
            leftPanel[0].BringToFront();
            prof_upload_btn.Enabled = false;
            save_btn.Enabled = false;
            cancel_btn.Enabled = false;
            dashboard_panel.Visible = false;
            main_panel.Visible = true;

            Cursor.Current = Cursors.WaitCursor;
            restClient.endPoint = Settings.baseUrl
                + "/api/profile/get/" + Login.id;

            string response = string.Empty;
            response = restClient.GetRequest();
            Console.WriteLine(response);

            var profile = JsonConvert.DeserializeObject<Profile>(response);

            if (profile != null)
            {
                current_userId = profile.user_id;
                prof_fn_tb.Text = profile.first_name;
                prof_mn_tb.Text = profile.middle_name;
                prof_ln_tb.Text = profile.last_name;
                prof_bdate_picker.Value = Convert.ToDateTime(profile.bday);
                prof_address_tb.Text = profile.address;
                prof_cn_tb.Text = profile.contact_num;
                if (profile.gender.Equals("Male"))
                {
                    prof_male_rb.Checked = true;
                } else
                {
                    prof_female_rb.Checked = true;
                }
            }

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
            edit = false;
            add_item_btn.Visible = true;
            ClearTextBoxesInAddLoan(addloan_panel.Controls);
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.Show();
        }

        private void confirmClientAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rightPanel[2].BringToFront();
            leftPanel[2].BringToFront();

            accounts_lbl.Text = "CONFIRM CLIENT ACCOUNT";
            up_photo_btn.Visible = false;
            up_sketch_btn.Visible = false;
            save_acc_btn.Text = "Confirm";
            find_btn.Visible = true;
            filter_acc_gb.Enabled = false;
            filter_cacc_gb.Enabled = false;
            asearch_gb.Enabled = false;

            stat_cb.Items.Clear();
            stat_cb.Items.Add("Unconfirmed");
            stat_cb.Items.Add("Confirmed");
        }

        private void createClientAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rightPanel[2].BringToFront();
            leftPanel[2].BringToFront();

            accounts_lbl.Text = "CREATE CLIENT ACCOUNT";
            up_sketch_btn.Visible = true;
            up_photo_btn.Visible = true;
            save_acc_btn.Text = "Save";
            find_btn.Visible = false;
            filter_acc_gb.Enabled = false;
            filter_cacc_gb.Enabled = false;
            asearch_gb.Enabled = false;
            male_rb.Checked = true;
            clearTextBoxes(confirmclient_panel.Controls);

            stat_cb.Items.Clear();
            stat_cb.Items.Add("Unconfirmed");
            stat_cb.Items.Add("Confirmed");
            stat_cb.SelectedIndex = 0;
        }

        private void viewLoanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
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
                loan_data.DataSource = null;

                if (loans.Count != 0)
                {
                    loan_data.DataSource = loans;
                    loan_data.Visible = true;
                    no_data_lbl.Visible = false;
                    format_loanDataTable();
                }
                else
                {
                    no_data_lbl.Text = "No data.";
                    no_data_lbl.Visible = true;
                    loan_data.Visible = false;
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            Cursor.Current = Cursors.Default;
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to logout?", "Logout",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Cursor.Current = Cursors.WaitCursor;
                restClient.endPoint = Settings.baseUrl.ToString()
                    + "/api/logout";
                restClient.login = false;

                string response = string.Empty;
                response = restClient.PostRequest();
                Console.WriteLine("response : " + response);
                string[] res = response.Split('|');

                if (res[0].Equals("OK"))
                {
                    this.Hide();
                    Login login = new Login();
                    login.Closed += (s, args) => this.Close();
                    login.Show();
                    Console.WriteLine("logout successful");
                }
                else
                {
                    Console.WriteLine("error logout");
                }
                Cursor.Current = Cursors.Default;
            }
        }

        private void addStaffAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rightPanel[0].BringToFront();
            leftPanel[2].BringToFront();
            filter_acc_gb.Enabled = false;
            filter_cacc_gb.Enabled = false;
            asearch_gb.Enabled = false;
            genpass_tb.Text = generatePassword();
            smale_rb.Checked = true;
        }

        private void viewAccountsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            unCheckAllRadioButtons();
            Cursor.Current = Cursors.WaitCursor;
            rightPanel[1].BringToFront();
            leftPanel[2].BringToFront();
            filter_acc_gb.Enabled = true;
            filter_cacc_gb.Enabled = true;
            asearch_gb.Enabled = true;

            restClient.endPoint = Settings.baseUrl
                + "/api/profile";

            string response = string.Empty;
            response = restClient.GetRequest();
            Console.WriteLine(response);

            profiles = JsonConvert.DeserializeObject<List<Profile>>(response);
            accounts_data.DataSource = null;

            List<Profile> new_profs = new List<Profile>();
            foreach (Profile p in profiles)
            {
                if (p.user_type != 0)
                {
                    new_profs.Add(p);
                }
            }

            if (new_profs.Count != 0)
            {
                accounts_data.Visible = true;
                no_accounts.Visible = false;
                accounts_data.DataSource = new_profs;
                format_accountDataTable();
            } else
            {
                accounts_data.Visible = false;
                no_accounts.Visible = true;
            }
            Cursor.Current = Cursors.Default;
        }

        private void viewItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rightPanel[8].BringToFront();
            leftPanel[3].BringToFront();
            filter_items_gb.Enabled = true;
            isearch_gb.Enabled = true;
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                restClient.endPoint = Settings.baseUrl
                    + "/api/product/getitems";
                string response = string.Empty;
                response = restClient.GetRequest();
                Console.WriteLine(response);

                items = JsonConvert.DeserializeObject<List<Item>>(response);
                item_data.DataSource = null;

                if (items.Count != 0)
                {
                    no_item_lbl.Visible = false;
                    item_data.DataSource = items;
                    item_data.Visible = true;
                    format_ItemDataTable();
                } else
                {
                    no_item_lbl.Visible = true;
                    item_data.Visible = false;
                }

            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            Cursor.Current = Cursors.Default;
        }

        private void addItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clearTextBoxes(additem_panel.Controls);
            price_tb.Text = "0";
            reorder_tb.Text = "0";
            filter_items_gb.Enabled = false;
            isearch_gb.Enabled = false;
            rightPanel[6].BringToFront();
            leftPanel[3].BringToFront();
            if (edit)
            {
                stock_lbl.Visible = true;
                repo_lbl.Visible = true;
                dam_lbl.Visible = true;
                stock_tb.Visible = true;
                repo_tb.Visible = true;
                dam_tb.Visible = true;
            } else
            {
                stock_lbl.Visible = false;
                repo_lbl.Visible = false;
                dam_lbl.Visible = false;
                stock_tb.Visible = false;
                repo_tb.Visible = false;
                dam_tb.Visible = false;
            }
        }

        private void addItemByBatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rightPanel[7].BringToFront();
            leftPanel[3].BringToFront();
            filter_items_gb.Enabled = false;
            isearch_gb.Enabled = false;
        }

        private void collectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rightPanel[9].BringToFront();
            leftPanel[4].BringToFront();
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                restClient.endPoint = Settings.baseUrl
                    + "/api/transaction";
                string response = string.Empty;
                response = restClient.GetRequest();
                Console.WriteLine(response);

                transactions = JsonConvert.DeserializeObject<List<Transaction>>(response);
                trans_data.DataSource = null;

                if (transactions.Count != 0)
                {
                    trans_data.DataSource = transactions;
                    format_transDataTable();
                    no_trans.Visible = false;
                    trans_data.Visible = true;
                } else
                {
                    no_trans.Visible = true;
                    trans_data.Visible = false;
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            Cursor.Current = Cursors.Default;
        }

        private void reportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reports_panel.BringToFront();
        }

        /**
         * CLICK EVENTS
         **/
        private void edit_profile_btn_Click(object sender, EventArgs e)
        {
            prof_bdate_picker.Enabled = true;
            prof_upload_btn.Enabled = true;
            save_btn.Enabled = true;
            cancel_btn.Enabled = true;
        }

        private void addloan_btn_Click(object sender, EventArgs e)
        {
            addLoanToolStripMenuItem_Click(sender, e);
        }

        private void viewloan_btn_Click(object sender, EventArgs e)
        {
            viewLoanToolStripMenuItem_Click(sender, e);
        }

        private void save_loan_btn_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            loan.profile_id = Int32.Parse(account_id_tb.Text);
            loan.term_length = Int32.Parse(length_tb.Text);
            loan.status = loan_status_cb.SelectedIndex;
            loan.loan_value = Convert.ToDouble(total_amount_tb.Text.ToString());
            loan.amortization = Convert.ToDouble(amortization_tb.Text.ToString());

            string response = string.Empty;
            try
            {
                if (Convert.ToDouble(amortization_tb.Text) <= Convert.ToDouble(cred_lmt_tb.Text))
                {
                    if (!edit)
                    {
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

                        response = restClient.PostRequest();
                        MessageBox.Show("Loan added successfully.", "Save Loan",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        ClearTextBoxesInAddLoan(addloan_panel.Controls);
                    }
                    else //edit
                    {
                        loan.loan_value = Convert.ToDouble(total_amount_tb.Text.ToString());

                        restClient.endPoint = Settings.baseUrl.ToString()
                        + "/api/loan/updateloan/"
                        + selected_loanid;
                        edit = false;
                        add_item_btn.Visible = true;

                        string jsonResult = JsonConvert.SerializeObject(loan);
                        Console.WriteLine(jsonResult.ToString());

                        restClient.postJSON = jsonResult;
                        restClient.login = false;
                        response = restClient.PutRequest();

                        MessageBox.Show("Loan updated successfully.", "Save Loan",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    Console.WriteLine("response : " + response);
                    ClearTextBoxesInAddLoan(addloan_panel.Controls);
                }
                else
                {
                    MessageBox.Show("Loan amount exceeded the credit limit.", "Save Loan",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                Cursor.Current = Cursors.Default;
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Error saving loan.", "Save Loan",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

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
            item_id_tb.Enabled = false;
            gb.Controls.Add(item_id_tb);

            TextBox item_name_tb = new TextBox();
            item_name_tb.Size = new Size(247, 20);
            item_name_tb.Location = new Point(81, 42);
            item_name_tb.Name = "item_name_tb";
            item_name_tb.Enabled = false;
            gb.Controls.Add(item_name_tb);

            TextBox item_desc_tb = new TextBox();
            item_desc_tb.Size = new Size(247, 39);
            item_desc_tb.Location = new Point(81, 67);
            item_desc_tb.Multiline = true;
            item_desc_tb.Name = "item_desc_tb";
            item_desc_tb.Enabled = false;
            gb.Controls.Add(item_desc_tb);

            ComboBox item_stat_cb = new ComboBox();
            item_stat_cb.Size = new Size(93, 21);
            item_stat_cb.Location = new Point(424, 16);
            item_stat_cb.Name = "item_stat_cb";
            item_stat_cb.DropDownStyle = ComboBoxStyle.DropDownList;
            item_stat_cb.Items.Add("In Stock");
            item_stat_cb.Items.Add("Repossessed");
            item_stat_cb.Items.Add("Damaged");
            item_stat_cb.SelectedIndex = 0;
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
            item_int_tb.Text = "0.01";
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
            edit = false;
            add_item_btn.Visible = true;
            ClearTextBoxesInAddLoan(addloan_panel.Controls);
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
            search.tag = "CLIENT_ID";
            search.Show();
            tag = "CLIENT_ID";
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

        private void create_stf_btn_Click(object sender, EventArgs e)
        {
            addStaffAccountToolStripMenuItem_Click(sender, e);
        }

        private void slname_btn_Click(object sender, EventArgs e)
        {
            loan_data.DataSource = null;
            unCheckAllRadioButtons();
            List<Loan> newList = new List<Loan>();
            foreach (Loan l in loans)
            {
                if (!slname_tb.Text.ToString().Equals(string.Empty))
                {
                    if (l.last_name.ToString().Equals(slname_tb.Text.ToString(), StringComparison.InvariantCultureIgnoreCase))
                    {
                        newList.Add(l);
                    }
                } else
                {
                    newList.Add(l); //add all
                }
            }

            if (newList.Count != 0)
            {
                loan_data.DataSource = newList;
                loan_data.Visible = true;
                no_data_lbl.Visible = false;
                format_loanDataTable();
            }
            else
            {
                no_data_lbl.Text = "No loans from " + slname_tb.Text.ToString() + ".";
                no_data_lbl.Visible = true;
                loan_data.Visible = false;
            }
        }

        private void slamount_btn_Click(object sender, EventArgs e)
        {
            loan_data.DataSource = null;
            unCheckAllRadioButtons();
            List<Loan> newList = new List<Loan>();
            foreach (Loan l in loans)
            {
                if (!slamount1_tb.Text.ToString().Equals(string.Empty) && !slamount2_tb.Text.ToString().Equals(string.Empty))
                {
                    if (Convert.ToDouble(l.loan_value.ToString()) >= Convert.ToDouble(slamount1_tb.Text.ToString()) &&
                        Convert.ToDouble(l.loan_value.ToString()) <= Convert.ToDouble(slamount2_tb.Text.ToString()))
                    {
                        newList.Add(l);
                    }
                } else if (!slamount1_tb.Text.ToString().Equals(string.Empty) && slamount2_tb.Text.ToString().Equals(string.Empty))
                {
                    if (Convert.ToDouble(l.loan_value.ToString()) >= Convert.ToDouble(slamount1_tb.Text.ToString()))
                    {
                        newList.Add(l);
                    }
                } else if (slamount1_tb.Text.ToString().Equals(string.Empty) && !slamount2_tb.Text.ToString().Equals(string.Empty))
                {
                    if (Convert.ToDouble(l.loan_value.ToString()) <= Convert.ToDouble(slamount2_tb.Text.ToString()))
                    {
                        newList.Add(l);
                    }
                }
                else
                {
                    newList.Add(l); //add all
                }
            }

            if (newList.Count != 0)
            {
                loan_data.DataSource = newList;
                loan_data.Visible = true;
                no_data_lbl.Visible = false;
                format_loanDataTable();
            }
            else
            {
                no_data_lbl.Text = "No data.";
                no_data_lbl.Visible = true;
                loan_data.Visible = false;
            }
        }

        private void cancel_btn_Click(object sender, EventArgs e)
        {
            profileToolStripMenuItem_Click(sender, e);
        }

        private void ssave_btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkEmptyTextBoxes(create_staff_panel.Controls))
                {
                    if (scn_tb.Text.All(Char.IsDigit) && scn_tb.Text.Length == 10)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        restClient.endPoint = Settings.baseUrl
                            + "/api/register";
                        restClient.login = true;

                        Register register = new Register();
                        register.username = username_tb.Text.ToString();
                        register.user_type = stype_tb.SelectedIndex == 0 ? "1" : "2";
                        register.password = genpass_tb.Text.ToString();
                        register.password_confirmation = genpass_tb.Text.ToString();

                        string jsonStr = JsonConvert.SerializeObject(register);
                        Console.WriteLine(jsonStr);
                        restClient.postJSON = jsonStr;

                        string response = string.Empty;
                        response = restClient.PostRequest();
                        Console.WriteLine(response);

                        restClient.endPoint = Settings.baseUrl
                            + "/api/profile/createprofile";
                        restClient.login = false;

                        string[] res = response.Split('|');
                        var jo = JObject.Parse(res[1]);
                        var id = jo["id"].ToString();

                        Profile profile = new Profile();
                        profile.user_id = Convert.ToInt32(id.ToString());
                        profile.first_name = sfname_tb.Text.ToString();
                        profile.middle_name = smname_tb.Text.ToString();
                        profile.last_name = lname_tb.Text.ToString();
                        profile.gender = smale_rb.Checked ? "male" : "female";
                        profile.address = saddress_tb.Text.ToString();
                        profile.contact_num = scn_tb.Text.ToString();
                        profile.bday = sbday_tb.Value.ToString("MM/dd/yyyy");
                        profile.verified = 1; //since staff/collector siya
                                              //todo profile.path_id_pic = ;

                        jsonStr = JsonConvert.SerializeObject(profile);
                        Console.WriteLine(jsonStr);
                        restClient.postJSON = jsonStr;

                        response = restClient.PostRequest();
                        Console.WriteLine(response);

                        clearTextBoxes(create_staff_panel.Controls);
                        genpass_tb.Text = generatePassword();
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("Account created successfully.", "Save Account",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    } else
                    {
                        MessageBox.Show("Invalid contact number.", "Save Account",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                } else
                {
                    MessageBox.Show("Fields cannot be empty.", "Save Account",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch
            {
                Cursor.Current = Cursors.Default;
                Console.WriteLine("Registration failed.");
                MessageBox.Show("Error creating account.", "Save Account",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

        }

        private void scancel_btn_Click(object sender, EventArgs e)
        {
            clearTextBoxes(create_staff_panel.Controls);
            genpass_tb.Text = generatePassword();
        }

        private void view_acc_btn_Click(object sender, EventArgs e)
        {
            viewAccountsToolStripMenuItem_Click(sender, e);
        }

        private void find_btn_Click(object sender, EventArgs e)
        {
            viewAccountsToolStripMenuItem_Click(sender, e);
            unCheckAllRadioButtons();
            unconf_rb.Checked = true;
        }

        private void save_acc_btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkEmptyTextBoxes(confirmclient_panel.Controls))
                {
                    if (cn_tb.Text.All(Char.IsDigit) && cn_tb.Text.Length == 10)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        string response = string.Empty;
                        string jsonStr = string.Empty;
                        Profile profile = new Profile();

                        if (!edit)
                        {
                            restClient.endPoint = Settings.baseUrl
                            + "/api/register";
                            restClient.login = true;

                            Register register = new Register();
                            register.username = generatePassword(); //dummy
                            register.user_type = "3"; //client
                            register.password = "test123"; //not needed since client don't need login credentials
                                                           //but fields cannot be empty so we put dummy password
                            register.password_confirmation = "test123";

                            jsonStr = JsonConvert.SerializeObject(register);
                            restClient.postJSON = jsonStr;
                             response = restClient.PostRequest();
                            Console.WriteLine(response);

                            restClient.endPoint = Settings.baseUrl
                            + "/api/profile/createprofile";
                            restClient.login = false;

                            string[] res = response.Split('|');
                            var jo = JObject.Parse(res[1]);
                            var id = jo["id"].ToString();

                            profile.user_id = Convert.ToInt32(id.ToString());
                            profile.first_name = fn_tb.Text.ToString();
                            profile.middle_name = mn_tb.Text.ToString();
                            profile.last_name = ln_tb.Text.ToString();
                            profile.gender = male_rb.Checked ? "male" : "female";
                            profile.address = addr_tb.Text.ToString();
                            profile.contact_num = cn_tb.Text.ToString();
                            profile.bday = bday_picker.Value.ToString("MM/dd/yyyy");
                            profile.occupation = occu_tb.Text.ToString();
                            profile.mo_income = mo_inc_cb.SelectedItem.ToString();
                            profile.mo_expense = mo_exp_cb.SelectedItem.ToString();
                            profile.credit_limit = Convert.ToDouble(credit_limit_tb.Text.ToString());
                            profile.verified = stat_cb.SelectedIndex;
                            //todo profile.path_id_pic = ;
                            //todo profile.path_house_sketch_pic = ;

                            jsonStr = JsonConvert.SerializeObject(profile);
                            Console.WriteLine(jsonStr);
                            restClient.postJSON = jsonStr;

                            response = restClient.PostRequest();
                            Console.WriteLine(response);
                            MessageBox.Show("Account created successfully.", "Save Account",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                        }
                        else //edit
                        {
                            profile.user_id = selected_userId;
                            profile.first_name = fn_tb.Text.ToString();
                            profile.middle_name = mn_tb.Text.ToString();
                            profile.last_name = ln_tb.Text.ToString();
                            profile.gender = male_rb.Checked ? "male" : "female";
                            profile.address = addr_tb.Text.ToString();
                            profile.contact_num = cn_tb.Text.ToString();
                            profile.bday = bday_picker.Value.ToString("MM/dd/yyyy");
                            profile.occupation = occu_tb.Text.ToString();
                            profile.mo_income = mo_inc_cb.SelectedItem.ToString();
                            profile.mo_expense = mo_exp_cb.SelectedItem.ToString();
                            profile.credit_limit = Convert.ToDouble(credit_limit_tb.Text.ToString());
                            profile.verified = 1; //verified
                                                  //todo profile.path_id_pic = ;
                                                  //todo profile.path_house_sketch_pic = ;

                            restClient.endPoint = Settings.baseUrl
                                + "/api/profile/updateprofile/"
                                + selected_profileid;
                            restClient.login = false;
                            edit = false;
                            jsonStr = JsonConvert.SerializeObject(profile);
                            restClient.postJSON = jsonStr;

                            response = restClient.PutRequest();
                            Console.WriteLine(response);
                            MessageBox.Show("Account created successfully.", "Save Account",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                        }
                        clearTextBoxes(confirmclient_panel.Controls);
                        genpass_tb.Text = generatePassword();
                        Cursor.Current = Cursors.Default;
                    }
                    else
                    {
                        MessageBox.Show("Invalid contact number.", "Save Account",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Fields cannot be empty.", "Save Account",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch
            {
                Cursor.Current = Cursors.Default;
                Console.WriteLine("Registration failed.");
                MessageBox.Show("Error creating account.", "Save Account",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void save_btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkEmptyTextBoxes(profile_panel.Controls))
                {
                    if (prof_cn_tb.Text.All(Char.IsDigit) && prof_cn_tb.Text.Length == 10)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        restClient.endPoint = Settings.baseUrl
                            + "/api/profile/updateprofile/" + Login.id;

                        Profile prof = new Profile();
                        prof.user_id = current_userId;
                        prof.first_name = prof_fn_tb.Text;
                        prof.middle_name = prof_mn_tb.Text;
                        prof.last_name = prof_ln_tb.Text;
                        prof.address = prof_address_tb.Text;
                        prof.contact_num = prof_cn_tb.Text;
                        prof.gender = prof_male_rb.Checked ? "Male" : "Female";
                        prof.bday = prof_bdate_picker.Value.ToString("MM/dd/yyyy");
                        prof.verified = 1; //verified
                                           //prof id pic

                        var jsonStr = JsonConvert.SerializeObject(prof);
                        restClient.postJSON = jsonStr;

                        string response = string.Empty;
                        restClient.login = false;
                        response = restClient.PutRequest();
                        Console.WriteLine(response);
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("Successfully updated profile.", "Save Profile",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Invalid contact number.", "Save Profile",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Fields cannot be empty.", "Save Profile",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                MessageBox.Show("Error updating profile.", "Save Profile",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void additem_btn_Click(object sender, EventArgs e)
        {
            addItemToolStripMenuItem_Click(sender, e);
        }

        private void addbatch_btn_Click(object sender, EventArgs e)
        {
            addItemByBatchToolStripMenuItem_Click(sender, e);
        }

        private void viewitems_btn_Click(object sender, EventArgs e)
        {
            viewItemsToolStripMenuItem_Click(sender, e);
        }

        private void search_price_btn_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Search search = new Search();
            search.tag = "PRICE";
            search.Show();
            tag = "PRICE";
            Cursor.Current = Cursors.Default;

            search.FormClosed += new FormClosedEventHandler(Search_FormClosed);
        }

        private void icancel_btn_Click(object sender, EventArgs e)
        {
            itemname_tb.Text = string.Empty;
            description_tb.Text = string.Empty;
            price_tb.Text = "0";
            edit = false;
            reorder_tb.Text = string.Empty;
            stock_lbl.Visible = false;
            repo_lbl.Visible = false;
            dam_lbl.Visible = false;
            stock_tb.Visible = false;
            repo_tb.Visible = false;
            dam_tb.Visible = false;
        }

        private void isave_btn_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                string response = string.Empty;
                string jsonStr = string.Empty;
                
                if (!edit)
                {
                    if (!itemname_tb.Text.Equals(string.Empty) && !description_tb.Text.Equals(string.Empty)
                        && !price_tb.Text.Equals("0") && !reorder_tb.Text.Equals("0"))
                    {
                        if (reorder_tb.Text.All(Char.IsDigit))
                        {
                            Item item = new Item();
                            item.item_name = itemname_tb.Text;
                            item.price_id = Search.price_id;
                            item.description = description_tb.Text;
                            item.reorder_point = Convert.ToInt32(reorder_tb.Text);

                            restClient.endPoint = Settings.baseUrl
                                + "/api/product/item";
                            restClient.login = false;

                            jsonStr = JsonConvert.SerializeObject(item);
                            restClient.postJSON = jsonStr;
                            response = restClient.PostRequest();
                            Console.WriteLine(response);
                            
                            MessageBox.Show("Item added successfully.", "Save Item",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                            itemname_tb.Text = string.Empty;
                            price_tb.Text = string.Empty;
                            description_tb.Text = string.Empty;
                            reorder_tb.Text = string.Empty;
                        } else
                        {
                            MessageBox.Show("Invalid number.", "Save Item",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }
                    } else
                    {
                        MessageBox.Show("Fields cannot be empty.", "Save Item",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                    
                } else
                {
                    if (checkEmptyTextBoxes(additem_panel.Controls))
                    {
                        if (stock_tb.Text.All(Char.IsDigit) && reorder_tb.Text.All(Char.IsDigit)
                            && repo_tb.Text.All(Char.IsDigit) && dam_tb.Text.All(Char.IsDigit))
                        {
                            Item item = new Item();
                            item.item_name = itemname_tb.Text;
                            item.price_id = selected_priceId;
                            item.stock_count = Convert.ToInt32(stock_tb.Text.ToString());
                            item.reorder_point = Convert.ToInt32(reorder_tb.Text.ToString());
                            item.repossessed = Convert.ToInt32(repo_tb.Text.ToString());
                            item.damaged = Convert.ToInt32(dam_tb.Text.ToString());
                            item.description = description_tb.Text;

                            restClient.endPoint = Settings.baseUrl
                                + "/api/product/updateitem/"
                                + selected_itemId;
                            restClient.login = false;

                            jsonStr = JsonConvert.SerializeObject(item);
                            restClient.postJSON = jsonStr;
                            response = restClient.PutRequest();
                            Console.WriteLine(response);
                            
                            MessageBox.Show("Item updated successfully.", "Save Item",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                            icancel_btn_Click(sender, e);
                        } else
                        {
                            MessageBox.Show("Invalid number.", "Save Item",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }
                    } else
                    {
                        MessageBox.Show("Fields cannot be empty.", "Save Item",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                MessageBox.Show("Error adding item.", "Save Item",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            Cursor.Current = Cursors.Default;
        }

        private void logout_btn_Click(object sender, EventArgs e)
        {
            logoutToolStripMenuItem_Click(sender, e);
        }

        private void profile_btn_Click(object sender, EventArgs e)
        {
            profileToolStripMenuItem_Click(sender, e);
        }

        private void iadditem_tb_Click(object sender, EventArgs e)
        {
            GroupBox gb = new GroupBox();
            gb.Size = new Size(551, 40);
            gb.Name = "item_gb" + Convert.ToString(++batch_index);
            gb.Text = "Item " + (batch_index + 1);

            Label iname_lbl = new Label();
            iname_lbl.Text = "Item Name:";
            iname_lbl.Location = new Point(25, 16);
            iname_lbl.Size = new Size(61, 13);
            gb.Controls.Add(iname_lbl);

            Label quantity_lbl = new Label();
            quantity_lbl.Text = "Quantity:";
            quantity_lbl.Location = new Point(368, 16);
            quantity_lbl.Size = new Size(49, 13);
            gb.Controls.Add(quantity_lbl);

            TextBox iname_tb = new TextBox();
            iname_tb.Location = new Point(87, 13);
            iname_tb.Size = new Size(206, 20);
            iname_tb.Name = "iname_tb";
            iname_tb.Enabled = false;
            gb.Controls.Add(iname_tb);

            TextBox quantity_tb = new TextBox();
            quantity_tb.Location = new Point(419, 13);
            quantity_tb.Size = new Size(100, 20);
            quantity_tb.Name = "iqty_tb";
            quantity_tb.Text = "0";
            quantity_tb.TextAlign = HorizontalAlignment.Right;
            gb.Controls.Add(quantity_tb);

            TextBox item_id = new TextBox();
            item_id.Location = new Point(328, 12);
            item_id.Size = new Size(30, 20);
            item_id.Name = "iid_tb";
            item_id.Visible = false;
            gb.Controls.Add(item_id);

            Button search_btn = new Button();
            search_btn.Size = new Size(25, 25);
            search_btn.Location = new Point(297, 10);
            search_btn.Name = "search_btn" + batch_index;
            search_btn.Click += new EventHandler(search_btn_Click);
            gb.Controls.Add(search_btn);

            items_fp.Controls.Add(gb);
        }

        private void search_btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            search_index = Convert.ToInt32(btn.Name.ToString().Substring((btn.Name.IndexOf('n') + 1),
                (btn.Name.Length - btn.Name.IndexOf('n') - 1)));
            Cursor.Current = Cursors.WaitCursor;
            Search search = new Search();
            search.tag = "ITEM_BATCH";
            search.Show();
            tag = "ITEM_BATCH";
            Cursor.Current = Cursors.Default;

            search.FormClosed += new FormClosedEventHandler(Search_FormClosed);
        }

        private void bcancel_btn_Click(object sender, EventArgs e)
        {
            bname_tb.Text = string.Empty;
            bnum_tb.Text = string.Empty;
            clearFlowPanelInBatch();
        }

        private void bsave_tb_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                restClient.endPoint = Settings.baseUrl
                    + "/api/product/batch";
                restClient.login = false;

                ItemBatch ibatch = new ItemBatch();
                ibatch.batch_name = bname_tb.Text;
                ibatch.batch_number = bnum_tb.Text;
                ibatch.date_rcv = Convert.ToDateTime(batch_picker.Value).ToString("MM/dd/yyyy");
                ibatch.items_rcv = new List<ItemBatchList>();

                int index = 0;
                foreach (Control c in items_fp.Controls)
                {
                    if (items_fp.Controls.ContainsKey("item_gb" + Convert.ToString(index)))
                    {
                        GroupBox gb = (GroupBox)items_fp.Controls[index];
                        TextBox qty_tb = (TextBox)gb.Controls["iqty_tb"];
                        TextBox id_tb = (TextBox)gb.Controls["iid_tb"];

                        ItemBatchList item = new ItemBatchList();
                        item.item_id = Convert.ToInt32(id_tb.Text.ToString());
                        item.quantity = Convert.ToInt32(qty_tb.Text.ToString());
                        ibatch.items_rcv.Add(item);
                    }
                    index++;
                }

                string response = string.Empty;
                string jsonStr = string.Empty;
                jsonStr = JsonConvert.SerializeObject(ibatch);
                restClient.postJSON = jsonStr;
                response = restClient.PostRequest();
                Console.WriteLine(response);

                Cursor.Current = Cursors.Default;
                MessageBox.Show("Save successful.", "Save Items",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                bcancel_btn_Click(sender, e);
            } catch (Exception ex)
            {
                MessageBox.Show("Error saving changes.", "Save Items",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Console.WriteLine(ex.Message.ToString());
            }
        }

        private void ccancel_btn_Click(object sender, EventArgs e)
        {
            prof_upload_btn.Visible = true;
            up_sketch_btn.Visible = true;
            find_btn.Visible = false;
            clearTextBoxes(confirmclient_panel.Controls);
        }

        private void scname_btn_Click(object sender, EventArgs e)
        {
            trans_data.DataSource = null;
            if (transactions.Count != 0)
            {
                List<Transaction> new_trans = new List<Transaction>();
                foreach (Transaction i in transactions)
                {
                    if (!scname_tb.Text.ToString().Equals(string.Empty))
                    {
                        if (i.last_name.ToString().Equals(scname_tb.Text.ToString(), StringComparison.InvariantCultureIgnoreCase))
                        {
                            new_trans.Add(i);
                        }
                    } else
                    {
                        new_trans.Add(i);
                    }
                    
                }

                if (new_trans.Count != 0)
                {
                    trans_data.DataSource = new_trans;
                    trans_data.Visible = true;
                    no_trans.Visible = false;
                    format_transDataTable();
                }
                else
                {
                    trans_data.Visible = false;
                    no_trans.Visible = true;
                }
            }
        }

        private void sclient_btn_Click(object sender, EventArgs e)
        {
            trans_data.DataSource = null;
            if (transactions.Count != 0)
            {
                List<Transaction> new_trans = new List<Transaction>();
                foreach (Transaction i in transactions)
                {
                    if (!sclient_tb.Text.ToString().Equals(string.Empty))
                    {
                        if (i.c_lname.ToString().Equals(sclient_tb.Text.ToString(), StringComparison.InvariantCultureIgnoreCase))
                        {
                            new_trans.Add(i);
                        }
                    }
                    else
                    {
                        new_trans.Add(i);
                    }

                }

                if (new_trans.Count != 0)
                {
                    trans_data.DataSource = new_trans;
                    trans_data.Visible = true;
                    no_trans.Visible = false;
                    format_transDataTable();
                }
                else
                {
                    trans_data.Visible = false;
                    no_trans.Visible = true;
                }
            }
        }

        private void asearch_btn_Click(object sender, EventArgs e)
        {
            accounts_data.DataSource = null;
            if (profiles.Count != 0)
            {
                List<Profile> new_profs = new List<Profile>();
                foreach (Profile p in profiles)
                {
                    if (!asearch_tb.Text.ToString().Equals(string.Empty))
                    {
                        if (p.last_name.ToString().Equals(asearch_tb.Text.ToString(), StringComparison.InvariantCultureIgnoreCase))
                        {
                            new_profs.Add(p);
                        }
                    } else
                    {
                        new_profs.Add(p);
                    }
                }

                if (new_profs.Count != 0)
                {
                    accounts_data.DataSource = new_profs;
                    accounts_data.Visible = true;
                    no_accounts.Visible = false;
                    format_accountDataTable();
                }
                else
                {
                    accounts_data.Visible = false;
                    no_accounts.Visible = true;
                }
            }
        }

        private void isearch_btn_Click(object sender, EventArgs e)
        {
            item_data.DataSource = null;
            if (items.Count != 0)
            {
                List<Item> new_profs = new List<Item>();
                foreach (Item i in items)
                {
                    if (!isearch_tb.Text.ToString().Equals(string.Empty))
                    {
                        CultureInfo culture = new CultureInfo("es-ES", false);
                        if (culture.CompareInfo.IndexOf(i.item_name, isearch_tb.Text.ToString(), CompareOptions.IgnoreCase) >= 0)
                        {
                            new_profs.Add(i);
                        }
                    } else
                    {
                        new_profs.Add(i);
                    }
                    
                }

                if (new_profs.Count != 0)
                {
                    item_data.DataSource = new_profs;
                    item_data.Visible = true;
                    no_item_lbl.Visible = false;
                    format_ItemDataTable();
                }
                else
                {
                    item_data.Visible = false;
                    no_item_lbl.Visible = true;
                }
            }
        }

        private void prof_upload_btn_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
            if (open.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(open.FileName);
                Console.WriteLine(open.FileName);
            }
        }

        private void stock_btn_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                restClient.endPoint = Settings.baseUrl
                    + "/api/product/getitems";
                string response = string.Empty;
                response = restClient.GetRequest();
                Console.WriteLine(response);

                var item_rep = JsonConvert.DeserializeObject<List<Item_InStock>>(response);
                item_data.DataSource = null;
                if (item_rep.Count != 0)
                {
                    List<Item_InStock> new_item = new List<Item_InStock>();
                    foreach (Item_InStock i in item_rep)
                    {
                        if (i.stock_count != 0)
                        {
                            new_item.Add(i);
                        }
                    }
                    if (new_item.Count != 0)
                    {
                        item_data.DataSource = new_item;
                        ImportDataGridViewDataToExcelSheet(item_data, "Item Inventory - In Stock", Item_InStock.headers);
                    }
                    else
                    {
                        MessageBox.Show("No data.", "Report",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                    }
                }
                Cursor.Current = Cursors.Default;
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }

        private void repossessed_btn_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                restClient.endPoint = Settings.baseUrl
                    + "/api/product/getitems";
                string response = string.Empty;
                response = restClient.GetRequest();
                Console.WriteLine(response);

                var item_rep = JsonConvert.DeserializeObject<List<Item_Repo>>(response);
                item_data.DataSource = null;
                if (item_rep.Count != 0)
                {
                    List<Item_Repo> new_item = new List<Item_Repo>();
                    foreach (Item_Repo i in item_rep)
                    {
                        if (i.repossessed != 0)
                        {
                            new_item.Add(i);
                        }
                    }
                    if (new_item.Count != 0)
                    {
                        item_data.DataSource = new_item;
                        ImportDataGridViewDataToExcelSheet(item_data, "Item Inventory - Repossessed", Item_Repo.headers);
                    } else
                    {
                        MessageBox.Show("No data.", "Report",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                    }
                }
                Cursor.Current = Cursors.Default;
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }

        private void damaged_btn_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                restClient.endPoint = Settings.baseUrl
                    + "/api/product/getitems";
                string response = string.Empty;
                response = restClient.GetRequest();
                Console.WriteLine(response);

                var item_rep = JsonConvert.DeserializeObject<List<Item_Damaged>>(response);
                item_data.DataSource = null;
                if (item_rep.Count != 0)
                {
                    List<Item_Damaged> new_item = new List<Item_Damaged>();
                    foreach (Item_Damaged i in item_rep)
                    {
                        if (i.damaged != 0)
                        {
                            new_item.Add(i);
                        }
                    }
                    if (new_item.Count != 0)
                    {
                        item_data.DataSource = new_item;
                        ImportDataGridViewDataToExcelSheet(item_data, "Item Inventory - Damaged", Item_Damaged.headers);
                    }
                    else
                    {
                        MessageBox.Show("No data.", "Report",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                    }
                }
                Cursor.Current = Cursors.Default;
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }

        private void application_btn_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                restClient.endPoint = Settings.baseUrl
                    + "/api/loan/getloans";

                string response = string.Empty;
                response = restClient.GetRequest();
                Console.WriteLine("response : " + response);

                var loans = JsonConvert.DeserializeObject<List<Loan_App>>(response);
                loan_data.DataSource = null;

                if (loans.Count != 0)
                {
                    loan_data.DataSource = loans;
                    ImportDataGridViewDataToExcelSheet(loan_data, "Loan Application", Loan_App.headers);
                }
                else
                {
                    MessageBox.Show("No data.", "Report",
                                   MessageBoxButtons.OK,
                                   MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            Cursor.Current = Cursors.Default;
        }

        private void daily_btn_Click(object sender, EventArgs e)
        {
            SummaryPrompt prompt = new SummaryPrompt();
            prompt.tag = "DAILY";
            prompt.Show();
        }

        private void weekly_btn_Click(object sender, EventArgs e)
        {
            SummaryPrompt prompt = new SummaryPrompt();
            prompt.tag = "WEEKLY";
            prompt.Show();
        }

        private void monthly_btn_Click(object sender, EventArgs e)
        {
            SummaryPrompt prompt = new SummaryPrompt();
            prompt.tag = "MONTHLY";
            prompt.Show();
        }

        private void payment_btn_Click(object sender, EventArgs e)
        {
            SummaryPrompt prompt = new SummaryPrompt();
            prompt.tag = "CUSTOMER";
            prompt.Show();
        }

        private void sales_btn_Click(object sender, EventArgs e)
        {
            //all completed loans
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                restClient.endPoint = Settings.baseUrl
                    + "/api/loan/getloans";

                string response = string.Empty;
                response = restClient.GetRequest();
                Console.WriteLine("response : " + response);

                var loans = JsonConvert.DeserializeObject<List<LoanReport>>(response);
                loan_data.DataSource = null;

                if (loans.Count != 0)
                {
                    loan_data.DataSource = transactions;
                    List<LoanReport> rep = new List<LoanReport>();
                    foreach (LoanReport p in loans)
                    {
                        if (p.status_str.ToString().Equals("Completed"))
                        {
                            rep.Add(p);
                        }
                    }
                    if (rep.Count != 0)
                    {
                        loan_data.DataSource = rep;
                        ImportDataGridViewDataToExcelSheet(loan_data, "Summary of Sales", LoanReport.headers);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("No data.", "Report",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            Cursor.Current = Cursors.Default;
        }

        /**
         * DOUBLE CLICK EVENTS
         **/
        private void accounts_data_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            edit = true;
            try
            {
                if (accounts_data.Rows[e.RowIndex].Cells["id"].Value != null)
                {
                    confirmClientAccountToolStripMenuItem_Click(sender, e);

                    selected_profileid = Convert.ToInt32(accounts_data.Rows[e.RowIndex].Cells["id"].Value.ToString());
                    selected_userId = Convert.ToInt32(accounts_data.Rows[e.RowIndex].Cells["user_id"].Value.ToString());
                    fn_tb.Text = accounts_data.Rows[e.RowIndex].Cells["first_name"].Value.ToString();
                    mn_tb.Text = accounts_data.Rows[e.RowIndex].Cells["middle_name"].Value.ToString();
                    ln_tb.Text = accounts_data.Rows[e.RowIndex].Cells["last_name"].Value.ToString();
                    addr_tb.Text = accounts_data.Rows[e.RowIndex].Cells["address"].Value.ToString();
                    cn_tb.Text = accounts_data.Rows[e.RowIndex].Cells["contact_num"].Value.ToString();
                    bday_picker.Text = accounts_data.Rows[e.RowIndex].Cells["bday"].Value.ToString();
                    occu_tb.Text = accounts_data.Rows[e.RowIndex].Cells["occupation"].Value.ToString();
                    mo_inc_cb.SelectedIndex = mo_inc_cb.Items.IndexOf(accounts_data.Rows[e.RowIndex].Cells["mo_income"].Value.ToString());
                    mo_exp_cb.SelectedIndex = mo_exp_cb.Items.IndexOf(accounts_data.Rows[e.RowIndex].Cells["mo_expense"].Value.ToString());
                    credit_limit_tb.Text = Convert.ToDouble(accounts_data.Rows[e.RowIndex].Cells["credit_limit"].Value).ToString("N1");
                    stat_cb.SelectedIndex = Convert.ToInt32(accounts_data.Rows[e.RowIndex].Cells["verified"].Value.ToString());
                    if (accounts_data.Rows[e.RowIndex].Cells["gender"].Value.ToString().Equals("Male"))
                    {
                        male_rb.Checked = true;
                    }
                    else
                    {
                        female_rb.Checked = true;
                    }
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.Message.ToString());
            }
        }

        private void loan_data_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            edit = true;
            add_item_btn.Visible = false;
            try
            {
                if (loan_data.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    addLoanToolStripMenuItem_Click(sender, e);

                    selected_loanid = Convert.ToInt32(loan_data.Rows[e.RowIndex].Cells["id"].Value.ToString());
                    client_name_tb.Text = loan_data.Rows[e.RowIndex].Cells["first_name"].Value.ToString() + " "
                        + loan_data.Rows[e.RowIndex].Cells["middle_name"].Value.ToString() + " "
                        + loan_data.Rows[e.RowIndex].Cells["last_name"].Value.ToString();
                    address_tb.Text = loan_data.Rows[e.RowIndex].Cells["address"].Value.ToString();
                    account_id_tb.Text = loan_data.Rows[e.RowIndex].Cells["profile_id"].Value.ToString();
                    contactnum_tb.Text = loan_data.Rows[e.RowIndex].Cells["contact_num"].Value.ToString();
                    cred_lmt_tb.Text = Convert.ToDouble(loan_data.Rows[e.RowIndex].Cells["credit_limit"].Value).ToString("N1");
                    total_amount_tb.Text = Convert.ToDouble(loan_data.Rows[e.RowIndex].Cells["loan_value"].Value).ToString("N1");
                    balance_tb.Text = Convert.ToDouble(loan_data.Rows[e.RowIndex].Cells["remaining_balance"].Value).ToString("N1");
                    length_tb.Text = Convert.ToInt32(loan_data.Rows[e.RowIndex].Cells["term_length"].Value.ToString()).ToString();
                    amortization_tb.Text = Convert.ToDouble(loan_data.Rows[e.RowIndex].Cells["amortization"].Value).ToString("N1");
                    ldate_picker.Text = Convert.ToDateTime(loan_data.Rows[e.RowIndex].Cells["created_at"].Value).ToShortDateString();
                    loan_status_cb.SelectedIndex = Convert.ToInt32(loan_data.Rows[e.RowIndex].Cells["status"].Value);

                    restClient.endPoint = Settings.baseUrl
                        + "/api/loan/getloanitems/"
                        + loan_data.Rows[e.RowIndex].Cells["id"].Value.ToString();
                    string response = string.Empty;
                    response = restClient.GetRequest();
                    Console.WriteLine(response);

                    var loanlist = JsonConvert.DeserializeObject<List<LoanItem>>(response);

                    if (loanlist != null && loanlist.Count != 0)
                    {
                        for (int i = 0; i < loanlist.Count - 1; i++)
                        {
                            add_item_btn_Click(sender, e);
                        }

                        int index = 0;
                        foreach (Control c in loan_items_fp.Controls)
                        {
                            if (loan_items_fp.Controls.ContainsKey("loanitem" + Convert.ToString(index)))
                            {
                                GroupBox gb = (GroupBox)loan_items_fp.Controls[index];

                                TextBox id_tb = (TextBox)gb.Controls["item_id_tb"];
                                TextBox name_tb = (TextBox)gb.Controls["item_name_tb"];
                                TextBox desc_tb = (TextBox)gb.Controls["item_desc_tb"];
                                ComboBox stat_cb = (ComboBox)gb.Controls["item_stat_cb"];
                                TextBox price_tb = (TextBox)gb.Controls["item_price_tb"];
                                TextBox interest_tb = (TextBox)gb.Controls["item_int_tb"];

                                id_tb.Text = loanlist[index].item_id.ToString();
                                name_tb.Text = loanlist[index].item_name.ToString();
                                desc_tb.Text = loanlist[index].description.ToString();
                                price_tb.Text = Convert.ToDouble(loanlist[index].price).ToString("N1");
                                interest_tb.Text = loanlist[index].interest.ToString();
                                stat_cb.SelectedIndex = loanlist[index].item_status;
                            }
                            index++;
                        }
                    }
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }

        private void item_data_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            edit = true;
            try
            {
                if (item_data.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    addItemToolStripMenuItem_Click(sender, e);

                    selected_itemId = Convert.ToInt32(item_data.Rows[e.RowIndex].Cells["id"].Value.ToString());
                    selected_priceId = Convert.ToInt32(item_data.Rows[e.RowIndex].Cells["price_id"].Value.ToString());
                    itemname_tb.Text = item_data.Rows[e.RowIndex].Cells["item_name"].Value.ToString();
                    description_tb.Text = item_data.Rows[e.RowIndex].Cells["description"].Value.ToString();
                    price_tb.Text = Convert.ToDouble(item_data.Rows[e.RowIndex].Cells["price"].Value).ToString("N1");
                    reorder_tb.Text = item_data.Rows[e.RowIndex].Cells["reorder_point"].Value.ToString();
                    stock_tb.Text = item_data.Rows[e.RowIndex].Cells["stock_count"].Value.ToString();
                    repo_tb.Text = item_data.Rows[e.RowIndex].Cells["repossessed"].Value.ToString();
                    dam_tb.Text = item_data.Rows[e.RowIndex].Cells["damaged"].Value.ToString();
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }

        /**
         * TEXTCHANGED EVENTS
         **/
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

                    try
                    {
                        total_amount += (Convert.ToDouble(price_tb.Text.ToString()) *
                            (1 + Convert.ToDouble(int_tb.Text.ToString())));
                    } catch (FormatException ex)
                    {
                        Console.WriteLine(ex.Message.ToString());
                    }
                }
                index++;
            }
            
            total_amount_tb.Text = total_amount.ToString("N1");
        }

        private void total_amount_tb_TextChanged(object sender, EventArgs e)
        {
            if (!total_amount_tb.Text.Equals(string.Empty) && !length_tb.Text.Equals(string.Empty))
            {
                if(!edit)
                {
                    amortization_tb.Text = ((Convert.ToDouble(total_amount_tb.Text)) /
                        (Convert.ToInt32(length_tb.Text))).ToString("N1");
                    balance_tb.Text = Convert.ToDouble(total_amount_tb.Text).ToString("N1");
                }
            }
        }

        private void length_tb_TextChanged(object sender, EventArgs e)
        {
            if (!total_amount_tb.Text.Equals(string.Empty) && !length_tb.Text.Equals(string.Empty))
            {
                amortization_tb.Text = (Convert.ToDouble(total_amount_tb.Text.ToString()) /
                Convert.ToInt32(length_tb.Text.ToString())).ToString("N1");
            }
        }

        private void item_int_tb_TextChanged(object sender, EventArgs e)
        {
            item_price_tb_TextChanged(sender, e);
        }
        
        /**
         * CHECKEDCHANGED EVENTS
         **/
        private void active_rb_CheckedChanged(object sender, EventArgs e)
        {
            loan_data.DataSource = null;
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
                    loan_data.Visible = true;
                    no_data_lbl.Visible = false;
                    format_loanDataTable();
                }
                else
                {
                    no_data_lbl.Text = "No active loans.";
                    no_data_lbl.Visible = true;
                    loan_data.Visible = false;
                }

            }
        }

        private void inactive_rb_CheckedChanged(object sender, EventArgs e)
        {
            loan_data.DataSource = null;
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
                    loan_data.Visible = true;
                    no_data_lbl.Visible = false;
                    format_loanDataTable();
                }
                else
                {
                    no_data_lbl.Text = "No inactive loans.";
                    no_data_lbl.Visible = true;
                    loan_data.Visible = false;
                }
            }
        }

        private void completed_rb_CheckedChanged(object sender, EventArgs e)
        {
            loan_data.DataSource = null;
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
                    loan_data.Visible = true;
                    no_data_lbl.Visible = false;
                    format_loanDataTable();
                }
                else
                {
                    no_data_lbl.Text = "No completed loans.";
                    no_data_lbl.Visible = true;
                    loan_data.Visible = false;
                }
            }
        }

        private void all_rb_CheckedChanged(object sender, EventArgs e)
        {
            loan_data.DataSource = null;
            if (loans.Count != 0)
            {
                loan_data.DataSource = loans;
                loan_data.Visible = true;
                no_data_lbl.Visible = false;
                format_loanDataTable();
            }
            else
            {
                no_data_lbl.Text = "No data.";
                no_data_lbl.Visible = true;
                loan_data.Visible = false;
            }
        }

        private void client_rb_CheckedChanged(object sender, EventArgs e)
        {
            accounts_data.DataSource = null;
            if (profiles.Count != 0)
            {
                List<Profile> new_profs = new List<Profile>();
                foreach(Profile p in profiles)
                {
                    if (p.user_type == 3)
                    {
                        new_profs.Add(p);
                    }
                }
                
                if (new_profs.Count != 0)
                {
                    accounts_data.DataSource = new_profs;
                    accounts_data.Visible = true;
                    no_accounts.Visible = false;
                    format_accountDataTable();
                } else
                {
                    accounts_data.Visible = false;
                    no_accounts.Visible = true;
                }
            }
        }

        private void staff_rb_CheckedChanged(object sender, EventArgs e)
        {
            accounts_data.DataSource = null;
            if (profiles.Count != 0)
            {
                List<Profile> new_profs = new List<Profile>();
                foreach (Profile p in profiles)
                {
                    if (p.user_type == 1)
                    {
                        new_profs.Add(p);
                    }
                }

                if (new_profs.Count != 0)
                {
                    accounts_data.DataSource = new_profs;
                    accounts_data.Visible = true;
                    no_accounts.Visible = false;
                    format_accountDataTable();
                }
                else
                {
                    accounts_data.Visible = false;
                    no_accounts.Visible = true;
                }
            }
        }

        private void collector_rb_CheckedChanged(object sender, EventArgs e)
        {
            accounts_data.DataSource = null;
            if (profiles.Count != 0)
            {
                List<Profile> new_profs = new List<Profile>();
                foreach (Profile p in profiles)
                {
                    if (p.user_type == 2)
                    {
                        new_profs.Add(p);
                    }
                }

                if (new_profs.Count != 0)
                {
                    accounts_data.DataSource = new_profs;
                    accounts_data.Visible = true;
                    no_accounts.Visible = false;
                    format_accountDataTable();
                }
                else
                {
                    accounts_data.Visible = false;
                    no_accounts.Visible = true;
                }
            }
        }

        private void aall_rb_CheckedChanged(object sender, EventArgs e)
        {
            accounts_data.DataSource = null;

            if (profiles.Count != 0)
            {
                List<Profile> new_profs = new List<Profile>();
                foreach (Profile p in profiles)
                {
                    if (p.user_type != 0)
                    {
                        new_profs.Add(p);
                    }
                }

                if (new_profs.Count != 0)
                {
                    accounts_data.Visible = true;
                    no_accounts.Visible = false;
                    accounts_data.DataSource = new_profs;
                    format_accountDataTable();
                }
                else
                {
                    accounts_data.Visible = false;
                    no_accounts.Visible = true;
                }
            }
        }

        private void conf_rb_CheckedChanged(object sender, EventArgs e)
        {
            accounts_data.DataSource = null;
            if (profiles.Count != 0)
            {
                List<Profile> new_profs = new List<Profile>();
                foreach (Profile p in profiles)
                {
                    if (p.verified == 1 && p.user_id != 0)
                    {
                        new_profs.Add(p);
                    }
                }

                if (new_profs.Count != 0)
                {
                    accounts_data.DataSource = new_profs;
                    accounts_data.Visible = true;
                    no_accounts.Visible = false;
                    format_accountDataTable();
                }
                else
                {
                    accounts_data.Visible = false;
                    no_accounts.Visible = true;
                }
            }
        }

        private void unconf_rb_CheckedChanged(object sender, EventArgs e)
        {
            accounts_data.DataSource = null;
            if (profiles.Count != 0)
            {
                List<Profile> new_profs = new List<Profile>();
                foreach (Profile p in profiles)
                {
                    if (p.verified == 0)
                    {
                        new_profs.Add(p);
                    }
                }

                if (new_profs.Count != 0)
                {
                    accounts_data.DataSource = new_profs;
                    accounts_data.Visible = true;
                    no_accounts.Visible = false;
                    format_accountDataTable();
                }
                else
                {
                    accounts_data.Visible = false;
                    no_accounts.Visible = true;
                }
            }
        }

        private void repo_rb_CheckedChanged(object sender, EventArgs e)
        {
            item_data.DataSource = null;
            if (items.Count != 0)
            {
                List<Item> new_profs = new List<Item>();
                foreach (Item i in items)
                {
                    if (i.repossessed != 0)
                    {
                        new_profs.Add(i);
                    }
                }

                if (new_profs.Count != 0)
                {
                    item_data.DataSource = new_profs;
                    item_data.Visible = true;
                    no_item_lbl.Visible = false;
                    format_ItemDataTable();
                }
                else
                {
                    item_data.Visible = false;
                    no_item_lbl.Visible = true;
                }
            }
        }

        private void dam_rb_CheckedChanged(object sender, EventArgs e)
        {
            item_data.DataSource = null;
            if (items.Count != 0)
            {
                List<Item> new_profs = new List<Item>();
                foreach (Item i in items)
                {
                    if (i.damaged != 0)
                    {
                        new_profs.Add(i);
                    }
                }

                if (new_profs.Count != 0)
                {
                    item_data.DataSource = new_profs;
                    item_data.Visible = true;
                    no_item_lbl.Visible = false;
                    format_ItemDataTable();
                }
                else
                {
                    item_data.Visible = false;
                    no_item_lbl.Visible = true;
                }
            }
        }

        private void instock_rb_CheckedChanged(object sender, EventArgs e)
        {
            item_data.DataSource = null;
            if (items.Count != 0)
            {
                List<Item> new_profs = new List<Item>();
                foreach (Item i in items)
                {
                    if (i.stock_count != 0)
                    {
                        new_profs.Add(i);
                    }
                }

                if (new_profs.Count != 0)
                {
                    item_data.DataSource = new_profs;
                    item_data.Visible = true;
                    no_item_lbl.Visible = false;
                    format_ItemDataTable();
                }
                else
                {
                    item_data.Visible = false;
                    no_item_lbl.Visible = true;
                }
            }
        }

        private void iall_rb_CheckedChanged(object sender, EventArgs e)
        {
            item_data.DataSource = null;
            if (items.Count != 0)
            {
                item_data.DataSource = items;
                item_data.Visible = true;
                no_item_lbl.Visible = false;
                format_ItemDataTable();
            } else
            {
                item_data.Visible = false;
                no_item_lbl.Visible = true;

            }
        }

        /**
         * SELECTED INDEX CHANGED EVENTS
         **/
        private void mo_inc_cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(mo_inc_cb.SelectedIndex)
            {
                case 0:
                    credit_limit_tb.Text = "50.00";
                    break;
                case 1:
                    credit_limit_tb.Text = "150.00";
                    break;
                case 2:
                    credit_limit_tb.Text = "250.00";
                    break;
                case 3:
                    credit_limit_tb.Text = "400.00";
                    break;
                case 4:
                    credit_limit_tb.Text = "500.00";
                    break;
                case 5:
                    credit_limit_tb.Text = "600.00";
                    break;
                case 6:
                    credit_limit_tb.Text = "1000.00";
                    break;
            }
        }

        /**
         * FORMATTING METHODS
         **/
        private void format_loanDataTable()
        {
            loan_data.Columns[0].HeaderText = "ID";
            loan_data.Columns[2].HeaderText = "First Name";
            loan_data.Columns[4].HeaderText = "Last Name";
            loan_data.Columns[6].HeaderText = "Amount";
            loan_data.Columns[8].HeaderText = "Created";
            loan_data.Columns[9].HeaderText = "Updated";
            loan_data.Columns[11].HeaderText = "Status";

            loan_data.Columns[1].Visible = false;
            loan_data.Columns[3].Visible = false;
            loan_data.Columns[5].Visible = false;
            loan_data.Columns[7].Visible = false;
            loan_data.Columns[10].Visible = false;
            loan_data.Columns[12].Visible = false;
            loan_data.Columns[13].Visible = false;
            loan_data.Columns[14].Visible = false;
            loan_data.Columns[15].Visible = false;
            loan_data.Columns[16].Visible = false;
            loan_data.Columns[0].Width = 35;
            loan_data.Columns[2].Width = 85;
            loan_data.Columns[4].Width = 85;
            loan_data.Columns[6].Width = 85;
            loan_data.Columns[8].Width = 110;
            loan_data.Columns[9].Width = 110;
            loan_data.Columns[11].Width = 60;
            loan_data.Columns[8].DefaultCellStyle.Format = "MM/dd/yy hh:mm tt";
            loan_data.Columns[9].DefaultCellStyle.Format = "MM/dd/yy hh:mm tt";
            loan_data.Columns[6].DefaultCellStyle.Format = "N1";
            loan_data.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void format_accountDataTable()
        {
            accounts_data.Columns[0].HeaderText = "ID";
            accounts_data.Columns[2].HeaderText = "First Name";
            accounts_data.Columns[3].HeaderText = "Middle Name";
            accounts_data.Columns[4].HeaderText = "Last Name";
            accounts_data.Columns[18].HeaderText = "User Type";
            accounts_data.Columns[19].HeaderText = "Status";

            accounts_data.Columns[1].Visible = false;
            accounts_data.Columns[5].Visible = false;
            accounts_data.Columns[6].Visible = false;
            accounts_data.Columns[7].Visible = false;
            accounts_data.Columns[8].Visible = false;
            accounts_data.Columns[9].Visible = false;
            accounts_data.Columns[10].Visible = false;
            accounts_data.Columns[11].Visible = false;
            accounts_data.Columns[12].Visible = false;
            accounts_data.Columns[13].Visible = false;
            accounts_data.Columns[14].Visible = false;
            accounts_data.Columns[15].Visible = false;
            accounts_data.Columns[16].Visible = false;
            accounts_data.Columns[17].Visible = false;
            accounts_data.Columns[20].Visible = false;
            accounts_data.Columns[0].Width = 50;
        }

        private void format_ItemDataTable()
        {
            item_data.Columns[0].HeaderText = "ID";
            item_data.Columns[1].HeaderText = "Item Name";
            item_data.Columns[3].HeaderText = "Price";
            item_data.Columns[4].HeaderText = "Stock Count";
            item_data.Columns[5].HeaderText = "Reorder Point";
            item_data.Columns[6].HeaderText = "Repossessed";
            item_data.Columns[7].HeaderText = "Damaged";

            item_data.Columns[2].Visible = false;
            item_data.Columns[8].Visible = false;

            item_data.Columns[0].Width = 35;
            item_data.Columns[1].Width = 120;
            item_data.Columns[3].Width = 65;
            item_data.Columns[4].Width = 100;
            item_data.Columns[5].Width = 100;
            item_data.Columns[6].Width = 75;
            item_data.Columns[7].Width = 75;

            item_data.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            item_data.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            item_data.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            item_data.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            item_data.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            item_data.Columns[3].DefaultCellStyle.Format = "N1";
        }

        private void format_transDataTable()
        {
            trans_data.Columns[0].HeaderText = "ID";
            trans_data.Columns[2].HeaderText = "Loan ID";
            trans_data.Columns[4].HeaderText = "Payment";
            trans_data.Columns[5].HeaderText = "Balance";
            trans_data.Columns[10].HeaderText = "Client";
            trans_data.Columns[13].HeaderText = "Collected By";

            trans_data.Columns[1].Visible = false;
            trans_data.Columns[3].Visible = false;
            trans_data.Columns[6].Visible = false;
            trans_data.Columns[7].Visible = false;
            trans_data.Columns[8].Visible = false;
            trans_data.Columns[9].Visible = false;
            trans_data.Columns[11].Visible = false;
            trans_data.Columns[12].Visible = false;

            trans_data.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            trans_data.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            trans_data.Columns[4].DefaultCellStyle.Format = "N1";
            trans_data.Columns[5].DefaultCellStyle.Format = "N1";
            trans_data.Columns[0].Width = 85;
            trans_data.Columns[2].Width = 85;
        }

        public void ClearTextBoxesInAddLoan(Control.ControlCollection ctrlCollection)
        {
            foreach (Control ctrl in ctrlCollection)
            {
                if (ctrl is TextBoxBase)
                {
                    if (ctrl.Name.Equals("total_amount_tb") || ctrl.Name.Equals("amortization_tb") ||
                        ctrl.Name.Equals("length_tb"))
                    {
                        total_amount_tb.Text = "0";
                        amortization_tb.Text = "0";
                        length_tb.Text = "90";
                    }
                    else
                    {
                        ctrl.Text = String.Empty;
                    }
                }
                else
                {
                    ClearTextBoxesInAddLoan(ctrl.Controls);
                }
            }


            for (int i = loan_items_fp.Controls.Count - 1; i >= 0; i--)
            {
                if (i != 0)
                {
                    loan_items_fp.Controls[i].Dispose();
                }
                else
                {
                    GroupBox gb = (GroupBox)loan_items_fp.Controls[i];
                    TextBox price_tb = (TextBox)gb.Controls["item_price_tb"];
                    TextBox int_tb = (TextBox)gb.Controls["item_int_tb"];

                    price_tb.Text = string.Empty;
                    int_tb.Text = "0.01";
                }
            }
        }

        private void clearTextBoxes(Control.ControlCollection ctrlCollection)
        {
            foreach (Control ctrl in ctrlCollection)
            {
                if (ctrl is TextBoxBase)
                {
                    ctrl.Text = String.Empty;
                }
                else
                {
                    clearTextBoxes(ctrl.Controls);
                }
            }
        }

        private void clearFlowPanelInBatch()
        {
            for (int i = items_fp.Controls.Count - 1; i >= 0; i--)
            {
                if (i != 0)
                {
                    items_fp.Controls[i].Dispose();
                } else
                {
                    GroupBox gb = (GroupBox)items_fp.Controls[0];
                    TextBox name_tb = (TextBox)gb.Controls["iname_tb"];
                    TextBox quantity_tb = (TextBox)gb.Controls["iqty_tb"];

                    name_tb.Text = string.Empty;
                    quantity_tb.Text = "0";
                }
            }
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
                                price_tb.Text = Search.itemPrice.ToString("N1");
                            }
                        }
                        break;
                    }
                case "ITEM_BATCH":
                    {
                        foreach (Control c in items_fp.Controls)
                        {
                            if (items_fp.Controls.ContainsKey("item_gb" + Convert.ToString(search_index)))
                            {
                                GroupBox gb = (GroupBox)items_fp.Controls[search_index];

                                TextBox name_tb = (TextBox)gb.Controls["iname_tb"];
                                TextBox id_tb = (TextBox)gb.Controls["iid_tb"];

                                name_tb.Text = Search.itemName.ToString();
                                id_tb.Text = Search.itemId.ToString();
                            }
                        }
                        break;
                    }
                case "CLIENT_ID":
                    {
                        client_name_tb.Text = Search.prof_fullname;
                        address_tb.Text = Search.prof_add;
                        account_id_tb.Text = Search.prof_id.ToString();
                        contactnum_tb.Text = Search.prof_cn;
                        cred_lmt_tb.Text = Search.prof_cred_rem.ToString("N1");
                        break;
                    }
                case "CLIENT":
                    {
                        client_name_tb.Text = Search.prof_fullname;
                        address_tb.Text = Search.prof_add;
                        account_id_tb.Text = Search.prof_id.ToString();
                        contactnum_tb.Text = Search.prof_cn;
                        cred_lmt_tb.Text = Search.prof_cred_limit.ToString("N1");
                        break;
                    }
                case "PRICE":
                    {
                        price_tb.Text = Convert.ToDouble(Search.price).ToString("N1");
                        break;
                    }
            }
        }

        private void unCheckAllRadioButtons()
        {
            all_rb.Checked = false;
            active_rb.Checked = false;
            inactive_rb.Checked = false;
            completed_rb.Checked = false;
            client_rb.Checked = false;
            staff_rb.Checked = false;
            collector_rb.Checked = false;
            aall_rb.Checked = false;
            conf_rb.Checked = false;
            unconf_rb.Checked = false;
        }

        public static string generatePassword()
        {
            const string charsAlpha = "abcdefghijklmnopqrstuvwxyz";
            string alpha = new string(Enumerable.Repeat(charsAlpha, 4)
              .Select(s => s[random.Next(s.Length)]).ToArray());

            const string charsNumeric = "0123456789";
            string numeric = new string(Enumerable.Repeat(charsNumeric, 4)
              .Select(s => s[random.Next(s.Length)]).ToArray());

            return alpha + numeric;
        }

        private bool checkEmptyTextBoxes(Control.ControlCollection ctrlCollection)
        {
            foreach (Control ctrl in ctrlCollection)
            {
                if (ctrl is TextBoxBase)
                {
                    if (ctrl.Text.ToString().Equals(String.Empty))
                    {
                        return false;
                    }
                }
                else
                {
                    checkEmptyTextBoxes(ctrl.Controls);
                }
            }
            return true;
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        private void ImportDataGridViewDataToExcelSheet(DataGridView datagrid, string filename, string[] headers)
        {
            Microsoft.Office.Interop.Excel.Application xlApp;
            Workbook xlWorkBook;
            Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlApp = new Microsoft.Office.Interop.Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);

            for (int x = 1; x < datagrid.Columns.Count + 1; x++)
            {
                xlWorkSheet.Cells[1, x] = headers[x];
            }

            for (int i = 0; i < datagrid.Rows.Count; i++)
            {
                for (int j = 0; j < datagrid.Columns.Count; j++)
                {
                    xlWorkSheet.Cells[i + 2, j + 1] = datagrid.Rows[i].Cells[j].Value.ToString();
                }
            }

            var saveFileDialoge = new SaveFileDialog();
            saveFileDialoge.FileName = filename;
            saveFileDialoge.DefaultExt = ".xlsx";
            if (saveFileDialoge.ShowDialog() == DialogResult.OK)
            {
                xlWorkBook.SaveAs(saveFileDialoge.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            }

            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);
        }
    }
}
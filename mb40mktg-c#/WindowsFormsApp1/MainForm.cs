using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
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
        private LoanType loanTypes;
        private RestClient restClient = new RestClient();
        private string tag = String.Empty;
        private StatusList statuslist;
        private UserType userTypes;
        private List<Loan> loans;
        private List<Profile> profiles;
        private static Random random = new Random();

        public MainForm()
        {
            InitializeComponent();
            main_panel.Visible = false;

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            rightPanel.Add(create_staff_panel); //0
            rightPanel.Add(view_acc_panel); //1
            rightPanel.Add(confirmclient_panel); //2
            rightPanel.Add(profile_panel); //3
            rightPanel.Add(addloan_panel); //4
            rightPanel.Add(viewloan_panel); //5
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

            string response = string.Empty;
            Cursor.Current = Cursors.WaitCursor;
            restClient.endPoint = Settings.baseUrl.ToString()
                + "/api/loanstatus";
            response = restClient.GetRequest();

            loanTypes = JsonConvert.DeserializeObject<LoanType>(response);
            for (int i = 0; i < loanTypes.types.Length; i++)
            {
                loan_status_cb.Items.Add(loanTypes.types[i]);
            }
            loan_status_cb.SelectedIndex = 1;

            restClient.endPoint = Settings.baseUrl.ToString()
                + "/api/statuslist";
            response = restClient.GetRequest();

            statuslist = JsonConvert.DeserializeObject<StatusList>(response);
            for (int i = 0; i < statuslist.status.Length; i++)
            {
                item_stat_cb.Items.Add(statuslist.status[i]);
            }
            item_stat_cb.SelectedIndex = 0;
            Cursor.Current = Cursors.Default;
        }

        /**
         * DASHBOARD MENU
         **/

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
            createClientAccountToolStripMenuItem_Click(sender, e);

            if (Login.user_type == 0)
            {
                create_stf_btn.Enabled = true;
            } else
            {
                create_stf_btn.Enabled = false;
            }

            genpass_tb.Text = generatePassword();
        }

        private void inventorymgt_btn_Click(object sender, EventArgs e)
        {
            this.Text = "Transaction Management";
            this.Refresh();
            main_panel.Visible = true;
        }

        /**
         * TOOLSTRIP MENU
         **/

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
        }

        private void viewLoanToolStripMenuItem_Click(object sender, EventArgs e)
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
            Cursor.Current = Cursors.Default;
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void addStaffAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rightPanel[0].BringToFront();
            leftPanel[2].BringToFront();
            filter_acc_gb.Enabled = false;
            filter_cacc_gb.Enabled = false;
        }

        private void viewAccountsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            rightPanel[1].BringToFront();
            leftPanel[2].BringToFront();
            filter_acc_gb.Enabled = true;
            filter_cacc_gb.Enabled = true;

            restClient.endPoint = Settings.baseUrl
                + "/api/profile";

            string response = string.Empty;
            response = restClient.GetRequest();
            Console.WriteLine(response);

            profiles = JsonConvert.DeserializeObject<List<Profile>>(response);
            accounts_data.DataSource = null;

            if (profiles.Count != 0)
            {
                accounts_data.Visible = true;
                no_accounts.Visible = false;
                accounts_data.DataSource = profiles;
                format_accountDataTable();
            } else
            {
                accounts_data.Visible = false;
                no_accounts.Visible = true;
            }
            Cursor.Current = Cursors.Default;
        }

        /**
         * CLICK EVENTS
         **/

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
            //TODO: add error catching for empty fields and wrong input
            loan.profile_id = Int32.Parse(account_id_tb.Text);
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
            Cursor.Current = Cursors.Default;

            ClearTextBoxesInAddLoan(this.Controls);
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
            item_stat_cb.DropDownStyle = ComboBoxStyle.DropDownList;
            for (int j = 0; j < statuslist.status.Length; j++)
            {
                item_stat_cb.Items.Add(statuslist.status[j]);
            }
            gb.Controls.Add(item_stat_cb);
            item_stat_cb.SelectedIndex = 0;

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
            ClearTextBoxesInAddLoan(this.Controls);
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
           
        }

        private void ssave_btn_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            restClient.endPoint = Settings.baseUrl
                + "/api/register";
            restClient.login = true;

            Register register = new Register();
            register.username = username_tb.Text.ToString();
            register.user_type = "1"; //staff
            register.password = genpass_tb.Text.ToString();
            register.password_confirmation = genpass_tb.Text.ToString();

            string jsonStr = JsonConvert.SerializeObject(register);
            Console.WriteLine(jsonStr);
            restClient.postJSON = jsonStr;

            string response = string.Empty;
            response = restClient.PostRequest();
            Console.WriteLine(response);

            clearTextBoxes(this.Controls);
            genpass_tb.Text = generatePassword();
            Cursor.Current = Cursors.Default;
        }

        private void scancel_btn_Click(object sender, EventArgs e)
        {
            clearTextBoxes(this.Controls);
            genpass_tb.Text = generatePassword();
        }

        private void view_acc_btn_Click(object sender, EventArgs e)
        {
            viewAccountsToolStripMenuItem_Click(sender, e);
        }

        private void find_btn_Click(object sender, EventArgs e)
        {
            unconf_rb.Checked = true;
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

        private void conf_rb_CheckedChanged(object sender, EventArgs e)
        {
            accounts_data.DataSource = null;
            if (profiles.Count != 0)
            {
                List<Profile> new_profs = new List<Profile>();
                foreach (Profile p in profiles)
                {
                    if (p.verified == 1)
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
            accounts_data.Columns[16].HeaderText = "User Type";
            //accounts_data.Columns[0].HeaderText = "Status";

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
                        length_tb.Text = "12";
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

            int index = 0;
            foreach (Control c in loan_items_fp.Controls)
            {
                if (loan_items_fp.Controls.ContainsKey("loanitem" + Convert.ToString(index)))
                {
                    if (index != 0)
                    {
                        GroupBox gb = (GroupBox)loan_items_fp.Controls[index];
                        gb.Dispose();
                    }
                    else
                    {
                        GroupBox gb = (GroupBox)loan_items_fp.Controls[index];
                        TextBox price_tb = (TextBox)gb.Controls["item_price_tb"];
                        TextBox int_tb = (TextBox)gb.Controls["item_int_tb"];

                        price_tb.Text = string.Empty;
                        int_tb.Text = "0.01";
                    }
                }
                index++;
            }
            account_stat_lbl.Text = "Status";
            account_stat_lbl.ForeColor = Color.Black;
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
                    ClearTextBoxesInAddLoan(ctrl.Controls);
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
                case "CLIENT":
                    {
                        Console.WriteLine("hello im here");

                        client_name_tb.Text = Search.prof_fullname;
                        address_tb.Text = Search.prof_add;
                        account_id_tb.Text = Search.prof_id.ToString();
                        contactnum_tb.Text = Search.prof_cn;
                        cred_lmt_tb.Text = Search.prof_cred_limit.ToString("N1");

                        if (Search.prof_stat == 1)
                        {
                            account_stat_lbl.Text = "This account is confirmed.";
                            account_stat_lbl.ForeColor = Color.Green;
                            loan_items_fp.Enabled = true;
                            add_item_btn.Enabled = true;
                            save_loan_btn.Enabled = true;
                            cancel_loan_btn.Enabled = true;
                        }
                        else
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

        private void unCheckAllRadioButtons()
        {
            all_rb.Checked = false;
            active_rb.Checked = false;
            inactive_rb.Checked = false;
            completed_rb.Checked = false;
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
    }
}
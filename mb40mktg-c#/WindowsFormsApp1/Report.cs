using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Item_InStock
    {
        public static string[] headers = {"", "ID", "ITEM NAME", "DESCRIPTION"
                , "PRICE", "STOCK COUNT", "REORDER POINT"};

        public int id { get; set; }
        public string item_name { get; set; }
        public string description { get; set; }
        public double price { get; set; }
        public int stock_count { get; set; }
        public int reorder_point { get; set; }
    }

    class Item_Repo
    {
        public static string[] headers = {"", "ID", "ITEM NAME", "DESCRIPTION"
                , "PRICE", "REORDER POINT", "REPOSSESSED"};

        public int id { get; set; }
        public string item_name { get; set; }
        public string description { get; set; }
        public double price { get; set; }
        public int reorder_point { get; set; }
        public int repossessed { get; set; }
    }

    class Item_Damaged
    {
        public static string[] headers = {"", "ID", "ITEM NAME", "DESCRIPTION"
                , "PRICE", "REORDER POINT", "DAMAGED"};

        public int id { get; set; }
        public string item_name { get; set; }
        public string description { get; set; }
        public double price { get; set; }
        public int reorder_point { get; set; }
        public int damaged { get; set; }
    }

    class Loan_App
    {
        public static string[] headers = { "", "ID", "FIRST NAME", "LAST NAME"
                , "TERM (DAYS)", "LOAN VALUE", "AMORTIZATION", "DATE LOANED"
                , "STATUS", "CREDIT LIMIT", "BALANCE"};

        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public int term_length { get; set; }
        public double loan_value { get; set; }
        public double amortization { get; set; }
        public DateTime created_at { get; set; }
        public string status_str { get; set; }
        public double credit_limit { get; set; }
        public double remaining_balance { get; set; }
    }

    class Summary
    {
        public static string[] headers = {"", "ID", "LOAN ID", "PAYMENT", "BALANCE"
                , "DATE", "COLLECTOR"};
        public int id { get; set; }
        public int loan_id { get; set; }
        public double payment { get; set; }
        public double remaining_balance { get; set; }
        public string created_at { get; set; }
        public string last_name { get; set; }
    }

    class Payment
    {
        public static string[] headers = {"", "ID", "LOAN ID", "PAYMENT", "BALANCE"
                , "LOAN VALUE", "CLIENT LAST NAME", "COLLECTED BY"};
        public int id { get; set; }
        public int loan_id { get; set; }
        public double payment { get; set; }
        public double remaining_balance { get; set; }
        public double loan_value { get; set; }
        public string last_name { get; set; }
        public string c_lname { get; set; }
    }

    class LoanReport
    {
        public static string[] headers = {"", "ID", "FIRST NAME", "MIDDLE NAME", "LAST NAME", "TERM"
                , "LOAN VALUE", "AMORTIZATION", "LOAN DATE", "CREDIT LIMIT", "STATUS"};

        public int id { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }
        public int term_length { get; set; }
        public double loan_value { get; set; }
        public double amortization { get; set; }
        public DateTime created_at { get; set; }
        public double credit_limit { get; set; }
        public string status_str { get; set; }
    }
}

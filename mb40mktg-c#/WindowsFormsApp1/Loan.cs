using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Loan
    {
        public int id { get; set; }
        public int profile_id { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }
        public int term_length { get; set; }
        public double loan_value { get; set; }
        public double amortization { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public int status { get; set; }
        public string status_str { get; set; }
        public string address { get; set; }
        public string contact_num { get; set; }
        public double credit_limit { get; set; }
        public double remaining_balance { get; set; }
        public double loan_rem { get; set; }
        public List<LoanItem> loan_items { get; set; }
    }
}
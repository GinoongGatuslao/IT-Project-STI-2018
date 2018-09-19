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
        public int account_id { get; set; }
        public int term_length { get; set; }
        public double loan_value { get; set; }
        public double amortization { get; set; }
        public int status { get; set; }
        public List<LoanItem> loan_items { get; set; }
    }
}
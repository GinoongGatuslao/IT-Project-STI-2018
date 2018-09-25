using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Profile
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }
        public string address { get; set; }
        public string contact_num { get; set; }
        public string bday { get; set; }
        public string occupation { get; set; }
        public double mo_income { get; set; }
        public double mo_expense { get; set; }
        public string path_id_pic { get; set; }
        public string path_house_sketch_pic { get; set; }
        public double credit_limit { get; set; }
        public int account_status { get; set; }
        public string username { get; set; }
        public int user_type { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Products
    {
        public int id { get; set; }
        public string item_name { get; set; }
        public int price { get; set; }
        public int stock_count { get; set; }
        public int repossessed { get; set; }
        public int damaged { get; set; }
        public int reorder_point { get; set; }
        public string description { get; set; }
    }
}

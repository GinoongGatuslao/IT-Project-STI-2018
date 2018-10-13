using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Item
    {
        public int id { get; set; }
        public string item_name { get; set; }
        public double price_id { get; set; }
        public double price { get; set; }
        public int stock_count { get; set; }
        public int reorder_point { get; set; }
        public int repossessed { get; set; }
        public int damaged { get; set; }
        public string description { get; set; }
    }
}

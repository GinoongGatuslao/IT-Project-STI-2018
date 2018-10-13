using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class ItemBatch
    {
        public string batch_name { get; set; }
        public string batch_number { get; set; }
        public string date_rcv { get; set; }
        public List<ItemBatchList> items_rcv { get; set; }
    }

    public class ItemBatchList
    {
        public int item_id { get; set; }
        public int quantity { get; set; } 
    }
}

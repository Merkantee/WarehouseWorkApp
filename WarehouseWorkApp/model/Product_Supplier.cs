using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseWorkApp.model
{
    public class Product_Supplier
    {
        public int Supply_id { get; set; }
        public int Product_id { get; set; }
        public int Supplier_id { get; set; }
        public int Supply_quantity { get; set; }
        public DateTime Supply_date { get; set; }
        public Product_Supplier(int supply_id, int product_id, int supplier_id, int supply_quantity, DateTime supply_date)
        {
            Supply_id = supply_id;
            Product_id = product_id;
            Supplier_id = supplier_id;
            Supply_quantity = supply_quantity;
            Supply_date = supply_date;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseWorkApp.model
{
    public class Product_categories
    {
        public int Category_id { get; set; }
        public string Category_name { get; set; }

        public Product_categories(int category_id, string category_name) 
        {
            Category_id = category_id;
            Category_name = category_name;
        }
    }
}

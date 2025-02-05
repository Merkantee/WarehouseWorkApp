using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseWorkApp.model
{
    public class Products
    {
        public int Product_id { get; set; }
        public string Product_name { get; set; }
        public string Product_description { get; set;}
        public int Quantity { get; set; }
        public decimal Product_price { get; set;}
        public int Product_category { get; set;}
        public Products(int product_id, string product_name, string product_description, int quantity, decimal product_price, int product_category)
        {
            Product_id = product_id;
            Product_name = product_name;
            Product_description = product_description;
            Quantity = quantity;
            Product_price = product_price;
            Product_category = product_category;

        }
    }
}

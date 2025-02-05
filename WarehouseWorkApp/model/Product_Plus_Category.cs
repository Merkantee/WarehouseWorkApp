using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseWorkApp.model
{
    public class Product_Plus_Category
    {
        public string Category_name { get; set; }
        public string Product_name { get; set; }
        public string Product_description { get; set; }
        public int Quantity { get; set; }
        public decimal Product_price { get; set; }

        public Product_Plus_Category(string Category_name, string Product_name, string Product_description, int Quantity, decimal Product_price)
        {
            this.Category_name = Category_name;
            this.Product_name = Product_name;
            this.Product_description = Product_description;
            this.Quantity = Quantity;
            this.Product_price = Product_price;
        }
    }
}

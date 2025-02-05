using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseWorkApp.model
{
    public class Inventory_results
    {
        public int Result_id { get; set; }
        public int User_id { get; set; }
        public int Product_id { get; set; }
        public int Actual_quantity { get; set; }
        public DateTime Check_time { get; set; }
        public string RoomName { get; set; }
        public string ProductName { get; set; }

        public Inventory_results(int result_id, int user_id, int product_id, int actual_quantity, DateTime check_time)
        {
            Result_id = result_id;
            User_id = user_id;
            Product_id = product_id;
            Actual_quantity = actual_quantity;
            Check_time = check_time;
        }

        public Inventory_results ()
        {
        
        }

        public Inventory_results(string roomName, string productName, int actual_quantity)
        {
            RoomName = roomName;
            ProductName = productName;
            Actual_quantity = actual_quantity;
        }
    }
}

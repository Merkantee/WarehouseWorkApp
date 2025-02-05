using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseWorkApp.model
{
    public class Product_location
    {
        public int Id_location;
        public int Id_product;
        public string ProductName { get; set; }
        public int Id_room;
        public string RoomName { get; set; }
        public int Count { get; set; }
        public Product_location(int id_location, int id_product, int id_room, int count)
        {
            Id_location = id_location;
            Id_product = id_product;
            Id_room = id_room;
            Count = count;
        }
        public Product_location(string productName, int count, string roomName)
        {
            ProductName = productName;
            Count = count;
            RoomName = roomName;
        }
    }
}

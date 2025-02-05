using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseWorkApp.model
{
    public class Storage_rooms
    {
        public int Room_id { get; set; } 
        public string Room_name { get; set; }
        public string Adress_room {  get; set; }

        public Storage_rooms(int room_id, string room_name, string adress_room)
        {
            Room_id = room_id;
            Room_name = room_name;
            Adress_room = adress_room;
        }
    }
}

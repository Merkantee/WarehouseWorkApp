using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseWorkApp.model
{
    public class Session
    {
        public int Session_id { get; set; }
        public int User_id { get; set; }
        public DateTime Login_time { get; set; }
        public DateTime Logout_time { get; set; }

        

        public Session(int session_id, int user_id, DateTime login_time) //На вход
        {
            Session_id = session_id;
            User_id = user_id;
            Login_time = login_time;
        }
        public Session (DateTime logout_time) //На выход
        {
            Logout_time = logout_time;
        }
        public Session (int session_id, int user_id, DateTime login_time, DateTime logout_time) //Для вывода
        {
            Session_id = session_id;
            User_id = user_id;
            Login_time = login_time;
            Logout_time = logout_time;
        }
    }
}

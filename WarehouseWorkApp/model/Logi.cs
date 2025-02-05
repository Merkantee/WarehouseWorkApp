using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseWorkApp.model
{
    public class Logi
    {
        public int Log_id { get; set; }
        public int User_id { get; set; }
        public DateTime Action_time { get; set; }
        public string Action_description { get; set; }

        public Logi(int log_id, int user_id, DateTime action_time, string action_description)
        {
            Log_id = log_id;
            User_id = user_id;
            Action_time = action_time;
            Action_description = action_description;
        }
    }
}

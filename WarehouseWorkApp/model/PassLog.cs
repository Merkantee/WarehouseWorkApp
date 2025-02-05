using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseWorkApp.model
{
    public class PassLog
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int Roles_fk { get; set; }
        public int Users_fk { get; set; }
        public PassLog(int id, string login, string password, int roles_fk, int users_fk)
        {
            Id = id;
            Login = login;
            Password = password;
            Roles_fk = roles_fk;
            Users_fk = users_fk;
        }
    }
}

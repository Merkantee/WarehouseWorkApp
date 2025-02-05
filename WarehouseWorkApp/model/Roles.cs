using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseWorkApp.model
{
    public class Roles
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Roles(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}

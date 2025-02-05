using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseWorkApp.model
{
    public class Suppliers
    {
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
        public string Contact_info { get; set; }

        public Suppliers(int supplierID, string supplierName, string contact_info)
        {
            SupplierID = supplierID;
            SupplierName = supplierName;
            Contact_info = contact_info;
        }
    }
}

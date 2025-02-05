using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WarehouseWorkApp
{
    public class Users
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Midelname { get; set; }
        public int SeriaPasport {  get; set; }
        public int NumberPasport { get; set; }
        public string SNILS { get; set; }
        public string numberPhone { get; set; }
        public string AddresLive { get; set; }
        public Users(int id, string name, string surname, string midelname)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Midelname = midelname;
        }
        public Users(int id, string name, string surname, string midelname, int seriaPasport, int numberPasport, string sNILS, string numberPhone, string addresLive)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Midelname = midelname;
            SeriaPasport = seriaPasport;
            NumberPasport = numberPasport;
            SNILS = sNILS;
            this.numberPhone = numberPhone;
            AddresLive = addresLive;
        }
    }
}

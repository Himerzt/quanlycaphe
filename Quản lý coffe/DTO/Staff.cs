using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DTO
{
    public class Staff
    {
        int iDStaff;
        string name;
        string phone;
        DateTime birthDate;
        DateTime startDate;
        private DataRow row;

        public Staff()
        {
        }
        public Staff(DataRow row)
        {
            IDStaff = Convert.ToInt32(row["IDStaff"]);
            Name = Convert.ToString(row["Name"]);
            Phone = Convert.ToString(row["Phone"]);
            BirthDate = Convert.ToDateTime(row["BirthDate"]);
            StartDate = Convert.ToDateTime(row["StartDate"]);
        }
        public int IDStaff { get => iDStaff; set => iDStaff = value; }
        public string Name { get => name; set => name = value; }
        public string Phone { get => phone; set => phone = value; }
        public DateTime BirthDate { get => birthDate; set => birthDate = value; }
        public DateTime StartDate { get => startDate; set => startDate = value; }

    }
}

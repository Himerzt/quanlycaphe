using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class TableDrink
    {
        int iDTableDrink;
        string name;
        string iDstatus;
        private DataRow row;

        public TableDrink()
        {
        }

        public TableDrink(DataRow row)
        {
            this.Row = row;
            IDTableDrink = (int)row["IDTableDrink"];
            Name = row["Name"].ToString();
            IDstatus = row["IDstatus"].ToString();

        }
        public TableDrink(int id, string name, string idstatus)
        {
            this.IDTableDrink = id;
            this.Name = name;
            this.IDstatus = idstatus;
        }

        public int IDTableDrink { get => iDTableDrink; set => iDTableDrink = value; }
        public string Name { get => name; set => name = value; }
        public string IDstatus { get => iDstatus; set => iDstatus = value; }

        public DataRow Row { get => row; set => row = value; }
    }
}

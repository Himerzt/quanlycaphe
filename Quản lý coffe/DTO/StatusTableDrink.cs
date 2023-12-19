using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class StatusTableDrink
    {
        int iDstatus;
        string status;
        private DataRow row;

        public int IDstatus { get => iDstatus; set => iDstatus = value; }
        public string Status { get => status; set => status = value; }
        public DataRow Row { get => row; set => row = value; }

        public StatusTableDrink(DataRow row)
        {
            this.Row = row;
            this.IDstatus= Convert.ToInt32(row["IDStatus"]);
            this.Status = row["Status"].ToString();
        }
        public StatusTableDrink(int id, string status)
        {
            this.IDstatus = id;
            this.Status = status;
        }

        public StatusTableDrink()
        {
        }
    }
}

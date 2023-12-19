using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Bill
    {
        int iDBill;
        DateTime? dateCheckIn;
        DateTime? dateCheckOut;
        int iDTable;
        int status;
        int iDStaff;
        float total;
        float? discount;
        private DataRow dataRow;

        public Bill(DataRow dataRow)
        {
            this.DataRow = dataRow;
            IDBill = Convert.ToInt32(dataRow["IDBill"]);
            DateCheckIn = Convert.ToDateTime(dataRow["DateCheckIn"]);
            DateCheckOut = Convert.ToDateTime(dataRow["DateCheckIn"]);
            IDTable = Convert.ToInt32(dataRow["IDTableDrink"]);
            IDStaff = Convert.ToInt32(dataRow["IDStaff"]);
            Status = Convert.ToInt32(dataRow["Status"]);
            Total = (float)Convert.ToDouble(dataRow["Total"]);
            Discount = (float)Convert.ToDouble(dataRow["Discount"]);
        }

        public Bill(int iDBill, DateTime? checkIn, DateTime? checkOut, int iDTable, int status, int iDStaff, float total, float? discount)
        {
            this.IDBill = iDBill;
            this.DateCheckIn = checkIn;
            this.DateCheckOut = checkOut;
            this.IDTable = iDTable;
            this.Status = status;
            this.IDStaff = iDStaff;
            this.Total = total;
            this.Discount = discount;           
        }

        public int IDBill { get => iDBill; set => iDBill = value; }
        public DateTime? DateCheckIn { get => dateCheckIn; set => dateCheckIn = value; }
        public DateTime? DateCheckOut { get => dateCheckOut; set => dateCheckOut = value; }
        public int IDTable { get => iDTable; set => iDTable = value; }
        public int Status { get => status; set => status = value; }
        public int IDStaff { get => iDStaff; set => iDStaff = value; }
        public float Total { get => total; set => total = value; }
        public float? Discount { get => discount; set => discount = value; }
        public DataRow DataRow { get => dataRow; set => dataRow = value; }
    }
}

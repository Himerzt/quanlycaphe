using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BUS
{
    public class BUS_Bill
    {
        DAL_Bill BillDAL = new DAL_Bill();
        private static BUS_Bill instance;

        public static BUS_Bill Instance
        {
            get { if (instance == null) instance = new BUS_Bill(); return BUS_Bill.instance; }
            private set { BUS_Bill.instance = value; }
        }

        public BUS_Bill() { }

        public DataTable ThongKeDoanhThu_BUS(DateTime checkIn, DateTime checkOut)
        {
            return BillDAL.ThongKeDoanhThu_DAL(checkIn, checkOut);
        }
        public int GetUncheckBillIDByTableID(int id)
        {
            return BillDAL.GetUncheckBillIDByTableID(id);
        }

        public void InsertBill_BUS(int idBill, int idTable, int idStaff)
        {
             BillDAL.InsertBill_DAL( idBill, idTable, idStaff);
        }
        public void CheckOut_BUS(int id, int discount, float totalPrice)
        {
            BillDAL.CheckOut_DAL(id,discount,totalPrice);
        }
    }
}

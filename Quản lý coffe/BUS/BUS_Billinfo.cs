using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class BUS_Billinfo
    {
        DAL_Billinfo BillinfoDAL = new DAL_Billinfo();
        private static BUS_Billinfo instance;

        public static BUS_Billinfo Instance
        {
            get { if (instance == null) instance = new BUS_Billinfo(); return BUS_Billinfo.instance; }
            private set { BUS_Billinfo.instance = value; }
        }
        public BUS_Billinfo() { }
        public void InsertBillInfo_BUS(int idBill, int idDrink, int count)
        {
             BillinfoDAL.InsertBillInfo_DAL(idBill, idDrink, count);
        }
    }
}

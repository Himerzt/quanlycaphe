using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class BUS_Status
    {
        DAL_Status Status = new DAL_Status();
        //Load cbb Status
        public List<StatusTableDrink> LoadStatusBUS()
        {
            List<StatusTableDrink> listtable = new List<StatusTableDrink>();
            DataTable dt = Status.LoadStatusDAL();

            foreach (DataRow row in dt.Rows)
            {
                StatusTableDrink tb = new StatusTableDrink(row);
                listtable.Add(tb);
            }
            return listtable;
        }

        public List<StatusTableDrink> BUS_GetStatusByID()
        {
            List<StatusTableDrink> listtable = new List<StatusTableDrink>();
            DataTable dt = Status.LoadStatusDAL();

            foreach (DataRow row in dt.Rows)
            {
                StatusTableDrink st = new StatusTableDrink(row);
                listtable.Add(st);

            }
            return listtable;
        }
    }
}

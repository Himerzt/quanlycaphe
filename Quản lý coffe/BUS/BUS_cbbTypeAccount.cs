using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class BUS_cbbTypeAccount
    {
        private DAL_cbbTypeAccount dal;
        public BUS_cbbTypeAccount()
        {
            dal = new DAL_cbbTypeAccount();
        }

        public DataTable GetAccountTypes()
        {
            // Call a method in TaikhoanDAL to get Account_Type data
            return dal.GetAccountTypes();
        }
    }
}

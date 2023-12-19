using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class BUS_Login
    {
        private DAL_Login dal;

        public BUS_Login()
        {
            dal = new DAL_Login();
        }

        public Login KiemTraLogin(Login login)
        {
            return dal.KiemTraLogin(login);
        }
    }
}

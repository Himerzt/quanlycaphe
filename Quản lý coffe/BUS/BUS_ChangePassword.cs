using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class BUS_ChangePassword
    {
        private DAL_ChangePassword dal;

        public BUS_ChangePassword()
        {
            dal = new DAL_ChangePassword();
        }

        public bool CapNhatMatKhau(ChangePassword changePasswordDTO)
        {
            if (!dal.KiemTraMatKhauCu(changePasswordDTO))
            {
                return false;
            }

            return dal.CapNhatMatKhau(changePasswordDTO);
        }
    }
}

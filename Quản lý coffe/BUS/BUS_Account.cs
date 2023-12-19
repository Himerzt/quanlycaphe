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
    public class BUS_Account
    {
        private DAL_Account dal;
        public BUS_Account()
        {
            dal = new DAL_Account();
        }

        public List<Account> GetTaikhoanDTO()
        {
            return dal.GetTaikhoanDTO();
        }


        //Sửa tài khoản
        public bool EditAccount(Account editAccountDTO)
        {
            return dal.EditAccount(editAccountDTO);
        }

        //Thêm tài khoản
        public bool AddAccount(Account themTaikhoanBUll)
        {
            return dal.AddAccount(themTaikhoanBUll);
        }
        // Xóa tài khoản
        public bool DeleteAccount(Account deleteaccountBll)
        {
            return dal.DeleteAccount(deleteaccountBll);
        }
        // Cài lại mật khẩu
        public bool ResetPassWord(string userName)
        {
            string newPassword = "999"; // Thay đổi thành mật khẩu mặc định thực tế

            // Gọi phương thức ResetPassword từ TaikhoanDAL
            return dal.ResetPassWord(userName, newPassword);
        }

        // Tìm kiếm tài khoản
        public DataTable SearchAccountByNameBULL(string searchText)
        {
            // Gọi phương thức tìm kiếm từ Data Access Layer
            return dal.SearchAccountByNameDAL(searchText);
        }
    }
}

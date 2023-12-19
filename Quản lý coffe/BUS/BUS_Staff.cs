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
    public class BUS_Staff
    {
        DAL_Staff Staff = new DAL_Staff();

        //xem danh sách staff
        public DataTable LoadStaffBUS()
        {
            return Staff.LoadStaffDAL();
        }

        //Add Staff
        public bool AddStaffBUS(Staff bst)
        {
            return Staff.AddStaffDAL(bst);
        }

        //Update Staff
        public bool UpdateStaffBUS(Staff st)
        {
            return Staff.UpdateStaffDAL(st);
        }

        //Delete Staff
        public bool DeleteStaffBUS(int IDStaff)
        {
            return Staff.DeleteTableDAL(IDStaff);
        }

        //Search
        public DataTable SearchStaffByNameBUS(string searchText)
        {
            // Gọi phương thức tìm kiếm từ Data Access Layer
            return Staff.SearchStaffByNameDAL(searchText);
        }
    }
}

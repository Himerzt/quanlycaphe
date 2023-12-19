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
    public class BUS_Payroll
    {
        DAL_Payroll PayrollDAL = new DAL_Payroll();
        public DataTable LoadPayroll_BUS()
        {
            return PayrollDAL.LoadPayroll_DAL();
        }

        public List<Staff> GetListStaff()
        {
            List<Staff> listStaff = new List<Staff>();
            DataTable dt = PayrollDAL.GetStaffData();

            foreach (DataRow row in dt.Rows)
            {
                Staff staff = new Staff(row);
                listStaff.Add(staff);
            }
            return listStaff;
        }

       /* public List<Payroll> GetPayrollList()
        {
            List<Payroll> listPayroll = new List<Payroll>();
            DataTable dt = PayrollDAL.GetPayrollData();

            foreach (DataRow row in dt.Rows)
            {
                Payroll payroll = new Payroll(row);
                listPayroll.Add(payroll);
            }
            return listPayroll;
        }*/

        public bool AddPayroll_BUS(Payroll p)
        {
            return PayrollDAL.AddPayroll_DAL(p);
        }

        public bool UpdatePayroll_BUS(/*int payrollID, int idStaff, int pay, int incentive, DateTime payrollDate*/ Payroll payroll)
        {
            return PayrollDAL.UpdatePayroll_DAL(payroll);
        }

        public bool DeletePayroll_BUS(int payrollID)
        {
            return PayrollDAL.DeletePayroll_DAL(payrollID);
        }

        public DataTable SearchPayroll_BUS(string searchKeyword)
        {
            return PayrollDAL.SearchPayroll_DAL(searchKeyword);
        }
    }
}

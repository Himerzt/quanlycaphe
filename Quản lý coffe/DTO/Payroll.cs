using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Payroll
    {
        int iDPayroll;
        DateTime? payrollDate;
        int iDStaff;
        SqlMoney pay;
        SqlMoney incentive;

        public int IDPayroll { get => iDPayroll; set => iDPayroll = value; }
        public DateTime? PayrollDate { get => payrollDate; set => payrollDate = value; }
        public int IDStaff { get => iDStaff; set => iDStaff = value; }
        public SqlMoney Pay { get => pay; set => pay = value; }
        public SqlMoney Incentive { get => incentive; set => incentive = value; }

        public Payroll()
        {
        }

        public Payroll(DataRow row)
        {
            IDPayroll = Convert.ToInt32(row["IDPayroll"]);
            Pay = Convert.ToDecimal(row["Pay"]);
            Incentive = Convert.ToDecimal(row["Incentive"]);
            PayrollDate = Convert.ToDateTime(row["PayrollDate"]);
            IDStaff = Convert.ToInt32(row["IDStaff"]);
        }

        
    }
}

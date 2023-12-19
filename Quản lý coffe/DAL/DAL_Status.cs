using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class DAL_Status: DataProvider
    {
        public DataTable LoadStatusDAL()
        {
            DataTable dt = new DataTable();
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter("Select * from StatusTableDrink", conn);
            da.Fill(dt);
            conn.Close();
            return dt;
        }

        public StatusTableDrink DAL_GetStatusByID(int IDstatus)
        {
            StatusTableDrink status = null;
            DataTable dt = new DataTable();
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter("Select * from StatusTableDrink where IDstatus = " + IDstatus, conn);
            da.Fill(dt);
            conn.Close();
            if (dt.Rows.Count > 0)
            {
                // Gán giá trị cho DTO_Status từ dòng đầu tiên của DataTable
                status = new StatusTableDrink(dt.Rows[0]);
            }
            return status;
        }
    }
}
